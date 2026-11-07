using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Extracts items of type <typeparamref name="TRecord"/> from a JSONL (JSON Lines / NDJSON) stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to extract. Must be <c>notnull</c>.</typeparam>
/// <remarks>
/// Reads a stream, line by line, deserializing each non-empty line as a single JSON object.
/// Blank lines are skipped with a warning logged.
/// Compatible with both JSONL and NDJSON formats.
/// </remarks>
/// <example>
/// <code>
/// using var stream = File.OpenRead("data.jsonl");
/// var extractor = new JsonLineExtractor&lt;Person&gt;(stream);
/// await foreach (var person in extractor.ExtractAsync(cancellationToken))
/// {
///     Console.WriteLine(person.Name);
/// }
/// </code>
/// </example>
public sealed class JsonLineExtractor<TRecord> : ExtractorBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private static readonly string OperationName = $"JSONL extraction of {typeof(TRecord).Name}";
    private static readonly KeyValuePair<string, object?> RecordTypeTag = new("etl.record_type", typeof(TRecord).Name);
    private readonly Stream _stream;
    private readonly bool _ownsStream;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private int _progressTimerWired;
    private long _currentLineNumber;
    private long _currentByteOffset;

    // How far ahead the checkpoint probe looks for the first line terminator.
    private const int ProbeByteLimit = 64 * 1024;

    // Encodings the StreamReader detects from a byte-order mark, longest preamble first
    // (the UTF-32 LE mark starts with the UTF-16 LE mark).
    private static readonly Encoding[] BomEncodings =
    {
        new UTF32Encoding(bigEndian: false, byteOrderMark: true),
        new UTF32Encoding(bigEndian: true, byteOrderMark: true),
        Encoding.UTF8,
        Encoding.Unicode,
        Encoding.BigEndianUnicode,
    };



    private Encoding? _encoding;

    /// <summary>
    /// Gets or sets the character encoding to use when reading the JSONL stream.
    /// When <see langword="null"/> (the default), the encoding is inferred from the
    /// stream's byte-order mark (BOM), falling back to UTF-8.
    /// </summary>
    public Encoding? Encoding { get => _encoding; [Obsolete("Configure Encoding through JsonLineExtractorOptions passed to the constructor instead. The setter will be removed in a later release.")] set => _encoding = value; }



    private bool _enableCheckpointing;

    /// <summary>
    /// Gets or sets a value indicating whether the extractor tracks the byte offset of each line
    /// so that <see cref="CurrentByteOffset"/> can be captured as a resumable checkpoint.
    /// Default is <see langword="false"/>.
    /// </summary>
    /// <remarks>
    /// Tracking requires computing the byte length of every line, which adds measurable per-line
    /// overhead on the extraction hot path. It is therefore opt-in: leave this <see langword="false"/>
    /// unless you intend to read <see cref="CurrentByteOffset"/> to create checkpoints. Resuming a
    /// prior run via <see cref="StartByteOffset"/> does not by itself require this flag — set it only
    /// when you also need to capture new checkpoints during the resumed run.
    /// </remarks>
    public bool EnableCheckpointing { get => _enableCheckpointing; [Obsolete("Configure EnableCheckpointing through JsonLineExtractorOptions passed to the constructor instead. The setter will be removed in a later release.")] set => _enableCheckpointing = value; }



    private long _startByteOffset;

    /// <summary>
    /// Gets or sets the byte offset within the stream at which extraction begins.
    /// Set this before calling <see cref="ExtractorBase{TSource,TProgress}.ExtractAsync(CancellationToken)"/> to resume from a saved checkpoint.
    /// The stream must be seekable when this value is greater than zero.
    /// Default is <c>0</c> (start of stream).
    /// </summary>
    public long StartByteOffset { get => _startByteOffset; [Obsolete("Configure StartByteOffset through JsonLineExtractorOptions passed to the constructor instead. The setter will be removed in a later release.")] set => _startByteOffset = value; }



    /// <summary>
    /// Gets the byte offset of the start of the next unread line in the stream.
    /// Capture this value after each <see langword="yield"/> to create a resumable checkpoint.
    /// </summary>
    /// <remarks>
    /// On a seekable stream the offset counts the byte-order mark, uses the encoding the reader
    /// detects from it (or <see cref="Encoding"/> when set), and sizes the line terminator from the
    /// first line found in the first 64 KiB. On a non-seekable stream it assumes UTF-8 (or
    /// <see cref="Encoding"/>) without a byte-order mark and a one-byte terminator.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <see cref="EnableCheckpointing"/> is <see langword="false"/>. Byte offsets are
    /// only tracked when checkpointing is enabled.
    /// </exception>
    public long CurrentByteOffset =>
        EnableCheckpointing
            ? Interlocked.Read(ref _currentByteOffset)
            : throw new InvalidOperationException("Set EnableCheckpointing to true before reading CurrentByteOffset.");



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Retained for binary compatibility with assemblies compiled before the optional-logger overload
    /// existed: <c>new JsonLineExtractor&lt;T&gt;(stream)</c> in such an assembly is bound to this exact signature,
    /// and removing it would fail at runtime with <see cref="MissingMethodException"/> with no compile-time
    /// signal. Hidden from IntelliSense; source code binds here too, so nothing changes for callers. New
    /// code has no reason to name this overload.
    /// </remarks>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    [EditorBrowsable(EditorBrowsableState.Never)]
    public JsonLineExtractor
    (
        Stream stream
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class that opens
    /// and owns the JSONL file at <paramref name="path"/>. The file is closed when extraction
    /// completes or the extractor is disposed.
    /// </summary>
    /// <param name="path">The path of the JSONL file to read.</param>
    /// <param name="options">The JSON serializer options to use, or <c>null</c> for the default.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="path"/> is <c>null</c>.</exception>
    /// <remarks>
    /// Superseded by the overload that takes a <see cref="JsonLineExtractorOptions"/> record before the serializer options.
    /// Retained for binary compatibility with assemblies compiled against 0.8.x, which are bound to this exact
    /// signature; removing it would fail them at runtime with <see cref="MissingMethodException"/> with no
    /// compile-time signal. Hidden from IntelliSense; source code binds here too, so nothing changes for callers.
    /// New code passes the record.
    /// </remarks>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    [EditorBrowsable(EditorBrowsableState.Never)]
    public JsonLineExtractor
    (
        string path,
        JsonSerializerOptions? options = null,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
    {
        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        _stream = File.OpenRead(path);
        _ownsStream = true;
        _options = options;
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with diagnostic logging.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="logger">
    /// An optional logger instance for diagnostic output. When <c>null</c> — or omitted —
    /// <see cref="NullLogger.Instance"/> is used and logging is disabled.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    public JsonLineExtractor
    (
        Stream stream,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The JSON serializer options to use for deserialization, or <c>null</c> for the serializer default.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Superseded by the overload that takes a <see cref="JsonLineExtractorOptions"/> record before the serializer options.
    /// Retained for binary compatibility with assemblies compiled against 0.8.x, which are bound to this exact
    /// signature; removing it would fail them at runtime with <see cref="MissingMethodException"/> with no
    /// compile-time signal. Hidden from IntelliSense; source code binds here too, so nothing changes for callers.
    /// New code passes the record.
    /// </remarks>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    [EditorBrowsable(EditorBrowsableState.Never)]
    public JsonLineExtractor
    (
        Stream stream,
        JsonSerializerOptions? options = null,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options;
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class configured through an options record.
    /// </summary>
    /// <param name="stream">The stream to read JSON Lines from.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="ExtractorOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    public JsonLineExtractor
    (
        Stream stream,
        JsonLineExtractorOptions options,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
        : base(options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = (options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions;
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class configured through an options record and a source-generated <see cref="JsonTypeInfo{TRecord}"/>.
    /// </summary>
    /// <param name="stream">The stream to read JSON Lines from.</param>
    /// <param name="typeInfo">The source-generated type information used to deserialize <typeparamref name="TRecord"/>; it carries its own serializer options.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="ExtractorOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="options"/> sets <see cref="JsonLineExtractorOptions.SerializerOptions"/>, which cannot be combined with a <paramref name="typeInfo"/>; the type info carries its own serializer options.</exception>
    public JsonLineExtractor
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        JsonLineExtractorOptions options,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
        : base(options)
    {
        RejectSerializerOptions(options);
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class configured through an options record.
    /// </summary>
    /// <param name="path">The path of the file to read; it is opened for reading and closed with the extractor.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="ExtractorOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    public JsonLineExtractor
    (
        string path,
        JsonLineExtractorOptions options,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
        : base(options)
    {
        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        _stream = File.OpenRead(path);
        _ownsStream = true;
        _options = (options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions;
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }



    /// <summary>
    /// Rejects a record that sets <see cref="JsonLineExtractorOptions.SerializerOptions"/> when a source-generated type info is
    /// supplied: the type info carries its own serializer options, so the record's would be ignored, and an ignored
    /// setting is worse than an error.
    /// </summary>
    /// <param name="options">The record to check.</param>
    /// <exception cref="ArgumentException">The record sets <see cref="JsonLineExtractorOptions.SerializerOptions"/>.</exception>
    private static void RejectSerializerOptions(JsonLineExtractorOptions? options)
    {
        if (options?.SerializerOptions is not null)
        {
            throw new ArgumentException
            (
                "SerializerOptions cannot be combined with a JsonTypeInfo; the type info carries its own serializer options.",
                nameof(options)
            );
        }
    }



    /// <summary>
    /// Copies the stage-specific settings from <paramref name="options"/> onto this instance; the inherited
    /// settings were applied by the <see cref="ExtractorBase{TSource, TProgress}"/> constructor.
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <see langword="null"/>.</exception>
    private void ApplyOptions(JsonLineExtractorOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _encoding = options.Encoding;
        _enableCheckpointing = options.EnableCheckpointing;
        _startByteOffset = options.StartByteOffset;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The construction-time configuration, including <see cref="JsonLineExtractorOptions.SerializerOptions"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    internal JsonLineExtractor
    (
        Stream stream,
        JsonLineExtractorOptions options,
        IProgressTimer timer,
        ILogger? logger = null
    )
        : base(options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = (options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions;
        _logger = logger ?? NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
        ApplyOptions(options);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with source-generated type metadata for AOT-friendly, reflection-free deserialization.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonLineExtractor
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with source-generated type metadata and an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    internal JsonLineExtractor
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        IProgressTimer timer,
        ILogger? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
    }



    /// <param name="resumeEncoding">
    /// The encoding detected from the head of the stream when resuming mid-stream; there is no
    /// byte-order mark at the resume offset, so the reader cannot detect it there itself.
    /// </param>
    private StreamReader CreateStreamReader(Encoding? resumeEncoding)
    {
        if (Encoding is null && resumeEncoding is not null)
        {
            return new StreamReader(_stream, resumeEncoding, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: !_ownsStream);
        }

#if NETSTANDARD2_0 || NET462 || NET481
        return Encoding is null
            ? new StreamReader(_stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: !_ownsStream)
            : new StreamReader(_stream, Encoding, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: !_ownsStream);
#else
        return Encoding is null
            ? new StreamReader(_stream, leaveOpen: !_ownsStream)
            : new StreamReader(_stream, Encoding, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: !_ownsStream);
#endif
    }



    /// <inheritdoc />
    protected override async IAsyncEnumerable<TRecord> ExtractWorkerAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        JsonLogMessages.StartingOperation(_logger, OperationName, null);
        var track = EnableCheckpointing;
        var probe = await PrepareForExtractionAsync(token).ConfigureAwait(false);
        var lineEncoding = probe.Encoding;
        var newlineSize = probe.NewlineSize;
        var skipBudget = SkipItemCount;
        var sw = Stopwatch.StartNew();
        using var reader = CreateStreamReader(StartByteOffset > 0 ? probe.Encoding : null);
        string? lineText;
#if NET7_0_OR_GREATER
        while ((lineText = await reader.ReadLineAsync(token).ConfigureAwait(false)) is not null)
#else
        while ((lineText = await reader.ReadLineAsync().ConfigureAwait(false)) is not null)
#endif
        {
            token.ThrowIfCancellationRequested();
            var lineBytes = track ? lineEncoding.GetByteCount(lineText) + newlineSize : 0;
            var lineNumber = Interlocked.Increment(ref _currentLineNumber);
            if (string.IsNullOrWhiteSpace(lineText))
            {
                Interlocked.Add(ref _currentByteOffset, lineBytes);
                JsonLogMessages.SkippingBlankLine(_logger, lineNumber, null);
                continue;
            }
            if (!TryDeserializeLine(lineText, out var item))
            {
                Interlocked.Add(ref _currentByteOffset, lineBytes);
                continue;
            }

            if (item is null)
            {
                Interlocked.Add(ref _currentByteOffset, lineBytes);
                JsonLogMessages.LineDeserializedToNull(_logger, lineNumber, null);
                continue;
            }
            if (skipBudget > 0)
            {
                skipBudget--;
                Interlocked.Add(ref _currentByteOffset, lineBytes);
                IncrementCurrentSkippedItemCount();
                JsonMetrics.AddSkipped(JsonMetrics.ExtractOperationTag, JsonMetrics.JsonLineComponentTag, RecordTypeTag);
                JsonLogMessages.SkippedItemAtLine(_logger, CurrentSkippedItemCount, SkipItemCount, lineNumber, null);
                continue;
            }
            if (CurrentItemCount >= MaximumItemCount)
            {
                JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                break;
            }
            Interlocked.Add(ref _currentByteOffset, lineBytes);
            IncrementCurrentItemCount();
            JsonMetrics.AddExtracted(JsonMetrics.ExtractOperationTag, JsonMetrics.JsonLineComponentTag, RecordTypeTag);
            JsonLogMessages.ExtractedItemFromLine(_logger, CurrentItemCount, lineNumber, null);
            yield return item;
        }

        CompleteExtraction(sw);
    }



    private void CompleteExtraction(Stopwatch sw)
    {
        if (_stream.CanSeek && Interlocked.Read(ref _currentByteOffset) > _stream.Length)
        {
            Interlocked.Exchange(ref _currentByteOffset, _stream.Length);
        }

        JsonLogMessages.JsonlExtractionCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, Interlocked.Read(ref _currentLineNumber), null);
        JsonMetrics.RecordDuration(sw.Elapsed.TotalMilliseconds, JsonMetrics.ExtractOperationTag, JsonMetrics.JsonLineComponentTag, RecordTypeTag);
    }



    private async Task<StreamProbe> PrepareForExtractionAsync(CancellationToken token)
    {
        Interlocked.Exchange(ref _currentLineNumber, 0);
        Interlocked.Exchange(ref _currentByteOffset, StartByteOffset);
        if (StartByteOffset > 0 && !_stream.CanSeek)
        {
            throw new InvalidOperationException
            (
                "Stream must be seekable when StartByteOffset is greater than zero."
            );
        }

        var probe = _stream.CanSeek && (EnableCheckpointing || StartByteOffset > 0)
            ? await ProbeStreamAsync(token).ConfigureAwait(false)
            : new StreamProbe(Encoding ?? Encoding.UTF8, PreambleLength: 0, NewlineSize: 1);

        if (StartByteOffset > 0)
        {
            _stream.Seek(StartByteOffset, SeekOrigin.Begin);
        }
        else
        {
            // The StreamReader swallows the byte-order mark without reporting it, so count it here
            // once; otherwise every checkpoint taken from a BOM-prefixed stream is short by its length.
            Interlocked.Add(ref _currentByteOffset, probe.PreambleLength);
        }

        return probe;
    }



    /// <summary>
    /// Reads the head of the stream (and restores the position) to find the encoding the
    /// <see cref="StreamReader"/> will use, the byte-order mark it will swallow and the byte size
    /// of the line terminator, so that checkpoint offsets match the bytes actually consumed.
    /// </summary>
    private async Task<StreamProbe> ProbeStreamAsync(CancellationToken token)
    {
        var savedPosition = _stream.Position;
        // Resuming: the head of the stream carries the byte-order mark that identifies the encoding.
        // Fresh run: probe where the reader will start.
        var probeStart = StartByteOffset > 0 ? 0 : savedPosition;
        _stream.Seek(probeStart, SeekOrigin.Begin);
        var buffer = new byte[ProbeByteLimit];
        var bytesRead = await ReadProbeAsync(buffer, token).ConfigureAwait(false);
        _stream.Seek(savedPosition, SeekOrigin.Begin);

        var (encoding, preambleLength) = DetectEncoding(buffer, bytesRead);
        var text = encoding.GetString(buffer, preambleLength, bytesRead - preambleLength);
        var newlineIndex = text.IndexOf('\n');
        var isCrlf = newlineIndex > 0 && text[newlineIndex - 1] == '\r';
        var newlineSize = encoding.GetByteCount(isCrlf ? "\r\n" : "\n");

        return new StreamProbe(encoding, preambleLength, newlineSize);
    }



    private (Encoding Encoding, int PreambleLength) DetectEncoding(byte[] buffer, int bytesRead)
    {
        if (Encoding is not null)
        {
            // An explicit encoding: the reader skips that encoding's own preamble when present.
            var preamble = Encoding.GetPreamble();
            return (Encoding, StartsWith(buffer, bytesRead, preamble) ? preamble.Length : 0);
        }

        // No explicit encoding: the reader detects the encoding from the byte-order mark.
        foreach (var candidate in BomEncodings)
        {
            var preamble = candidate.GetPreamble();
            if (StartsWith(buffer, bytesRead, preamble))
            {
                return (candidate, preamble.Length);
            }
        }

        return (Encoding.UTF8, 0);
    }



    private static bool StartsWith(byte[] buffer, int bytesRead, byte[] preamble)
    {
        if (preamble.Length == 0 || bytesRead < preamble.Length)
        {
            return false;
        }

        for (var i = 0; i < preamble.Length; i++)
        {
            if (buffer[i] != preamble[i])
            {
                return false;
            }
        }

        return true;
    }



    private async Task<int> ReadProbeAsync(byte[] buffer, CancellationToken token)
    {
        var total = 0;
        while (total < buffer.Length)
        {
#if NETSTANDARD2_0 || NET462 || NET481
            token.ThrowIfCancellationRequested();
#pragma warning disable CA2016, MA0040, S8949 // old TFM overload has no CancellationToken parameter
            var read = await _stream.ReadAsync(buffer, total, buffer.Length - total).ConfigureAwait(false);
#pragma warning restore CA2016, MA0040, S8949
#else
            var read = await _stream.ReadAsync(buffer.AsMemory(total, buffer.Length - total), token).ConfigureAwait(false);
#endif
            if (read == 0)
            {
                break;
            }

            total += read;
        }

        return total;
    }



    private readonly record struct StreamProbe(Encoding Encoding, int PreambleLength, int NewlineSize);



#if NET5_0_OR_GREATER
    [UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Reflection-based serialization runs only when _typeInfo is null, and every constructor that leaves it null carries [RequiresUnreferencedCode].")]
    [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "Reflection-based serialization runs only when _typeInfo is null, and every constructor that leaves it null carries [RequiresDynamicCode].")]
#endif
    private bool TryDeserializeLine(string lineText, out TRecord? item)
    {
        try
        {
            item = _typeInfo is not null
                ? JsonSerializer.Deserialize(lineText, _typeInfo)
                : JsonSerializer.Deserialize<TRecord>(lineText, _options);
            return true;
        }
        catch (JsonException ex)
        {
            var context = new ItemErrorContext
            (
                CurrentItemCount + CurrentSkippedItemCount + CurrentErrorItemCount + 1,
                ex,
                () => lineText
            );
            if (HandleItemError(context) == ItemErrorAction.Abort)
            {
                throw;
            }

            item = default;
            return false;
        }
    }



    /// <inheritdoc />
    protected override JsonReport CreateProgressReport() =>
        new
        (
            CurrentItemCount,
            CurrentSkippedItemCount
        );



    /// <inheritdoc />
    protected override IProgressTimer CreateProgressTimer(IProgress<JsonReport> progress)
    {
        if (_progressTimer is not null)
        {
            if (Interlocked.CompareExchange(ref _progressTimerWired, 1, 0) == 0)
            {
                _progressTimer.Elapsed += () => progress.Report(CreateProgressReport());
            }

            return _progressTimer;
        }

        return base.CreateProgressTimer(progress);
    }



    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && _ownsStream)
        {
            _stream.Dispose();
        }

        base.Dispose(disposing);
    }
}
