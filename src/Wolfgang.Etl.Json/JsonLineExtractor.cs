using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    private static readonly KeyValuePair<string, object?> _operationTag = new("etl.operation", "extract");
    private static readonly KeyValuePair<string, object?> _componentTag = new("etl.component", "JsonLine");
    private static readonly KeyValuePair<string, object?> _recordTypeTag = new("etl.record_type", typeof(TRecord).Name);
    private readonly Stream _stream;
    private readonly bool _ownsStream;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private readonly List<JsonDeserializationError> _errors = new();
    private int _progressTimerWired;
    private long _currentLineNumber;
    private long _currentByteOffset;



    /// <summary>
    /// Gets or sets the character encoding to use when reading the JSONL stream.
    /// When <see langword="null"/> (the default), the encoding is inferred from the
    /// stream's byte-order mark (BOM), falling back to UTF-8.
    /// </summary>
    public System.Text.Encoding? Encoding { get; set; }



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
    public bool EnableCheckpointing { get; set; }



    /// <summary>
    /// Gets or sets the byte offset within the stream at which extraction begins.
    /// Set this before calling <see cref="ExtractorBase{TSource,TProgress}.ExtractAsync(System.Threading.CancellationToken)"/> to resume from a saved checkpoint.
    /// The stream must be seekable when this value is greater than zero.
    /// Default is <c>0</c> (start of stream).
    /// </summary>
    public long StartByteOffset { get; set; }



    /// <summary>
    /// Gets the byte offset of the start of the next unread line in the stream.
    /// Capture this value after each <see langword="yield"/> to create a resumable checkpoint.
    /// </summary>
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
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
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
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
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
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonLineExtractor
    (
        Stream stream,
        ILogger<JsonLineExtractor<TRecord>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="options"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonLineExtractor
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    internal JsonLineExtractor
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
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
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonLineExtractor
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
    }



    /// <summary>
    /// Gets or sets how deserialization errors are handled during extraction.
    /// Default is <see cref="ErrorHandling.Throw"/>.
    /// </summary>
    public ErrorHandling ErrorHandling { get; init; } = ErrorHandling.Throw;



    /// <summary>
    /// Gets the collection of deserialization errors captured during the most recent extraction.
    /// Only populated when <see cref="ErrorHandling"/> is <see cref="ErrorHandling.CaptureAndContinue"/>.
    /// </summary>
    public IReadOnlyList<JsonDeserializationError> Errors => _errors.AsReadOnly();



    private StreamReader CreateStreamReader()
    {
#if NETSTANDARD2_0 || NET462 || NET481
        return Encoding is null
            ? new StreamReader(_stream, System.Text.Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: !_ownsStream)
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
        _errors.Clear();
        JsonLogMessages.StartingOperation(_logger, OperationName, null);
        var track = EnableCheckpointing;
        var (newlineSize, lineEncoding) = await PrepareForExtractionAsync(token).ConfigureAwait(false);
        var skipBudget = SkipItemCount;
        var sw = Stopwatch.StartNew();
        using var reader = CreateStreamReader();
        string? line;
#if NETSTANDARD2_0 || NET462 || NET481
        while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) is not null)
#else
        while ((line = await reader.ReadLineAsync(token).ConfigureAwait(false)) is not null)
#endif
        {
            token.ThrowIfCancellationRequested();
            var lineBytes = track ? lineEncoding.GetByteCount(line) + newlineSize : 0;
            var lineNum = Interlocked.Increment(ref _currentLineNumber);
            if (string.IsNullOrWhiteSpace(line))
            {
                Interlocked.Add(ref _currentByteOffset, lineBytes);
                JsonLogMessages.SkippingBlankLine(_logger, lineNum, null);
                continue;
            }
            if (!TryDeserializeLine(line, lineNum, out var item))
            {
                Interlocked.Add(ref _currentByteOffset, lineBytes);
                continue;
            }

            if (item is null)
            {
                Interlocked.Add(ref _currentByteOffset, lineBytes);
                JsonLogMessages.LineDeserializedToNull(_logger, lineNum, null);
                continue;
            }
            if (skipBudget > 0)
            {
                skipBudget--;
                Interlocked.Add(ref _currentByteOffset, lineBytes);
                IncrementCurrentSkippedItemCount();
                JsonMetrics.AddSkipped(_operationTag, _componentTag, _recordTypeTag);
                JsonLogMessages.SkippedItemAtLine(_logger, CurrentSkippedItemCount, SkipItemCount, lineNum, null);
                continue;
            }
            if (CurrentItemCount >= MaximumItemCount)
            {
                JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                break;
            }
            Interlocked.Add(ref _currentByteOffset, lineBytes);
            IncrementCurrentItemCount();
            JsonMetrics.AddExtracted(_operationTag, _componentTag, _recordTypeTag);
            JsonLogMessages.ExtractedItemFromLine(_logger, CurrentItemCount, lineNum, null);
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
        JsonMetrics.RecordDuration(sw.Elapsed.TotalMilliseconds, _operationTag, _componentTag, _recordTypeTag);
    }



    private async Task<(int NewlineSize, Encoding LineEncoding)> PrepareForExtractionAsync(CancellationToken token)
    {
        Interlocked.Exchange(ref _currentLineNumber, 0);
        Interlocked.Exchange(ref _currentByteOffset, StartByteOffset);
        var lineEncoding = Encoding ?? System.Text.Encoding.UTF8;
        var newlineSize = EnableCheckpointing
            ? await DetectNewlineSizeAsync(token).ConfigureAwait(false)
            : 0;
        if (StartByteOffset > 0)
        {
            if (!_stream.CanSeek)
            {
                throw new InvalidOperationException
                (
                    "Stream must be seekable when StartByteOffset is greater than zero."
                );
            }

            _stream.Seek(StartByteOffset, SeekOrigin.Begin);
        }

        return (newlineSize, lineEncoding);
    }



    private async Task<int> DetectNewlineSizeAsync(CancellationToken token)
    {
        if (!_stream.CanSeek) { return 1; }

        var savedPosition = _stream.Position;
        _stream.Seek(0, SeekOrigin.Begin);
        var buffer = new byte[256];
#if NETSTANDARD2_0 || NET462 || NET481
#pragma warning disable CA2016, MA0040 // old TFM overload has no CancellationToken parameter
        var bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
#pragma warning restore CA2016, MA0040
        _ = token;
#else
        var bytesRead = await _stream.ReadAsync(buffer.AsMemory(0, buffer.Length), token).ConfigureAwait(false);
#endif
        _stream.Seek(savedPosition, SeekOrigin.Begin);

        for (var i = 0; i < bytesRead - 1; i++)
        {
            if (buffer[i] == '\r' && buffer[i + 1] == '\n') { return 2; }
            if (buffer[i] == '\n') { return 1; }
        }

        return 1;
    }



    private bool TryDeserializeLine(string line, long lineNum, out TRecord? item)
    {
        try
        {
            item = _typeInfo is not null
                ? JsonSerializer.Deserialize(line, _typeInfo)
                : JsonSerializer.Deserialize<TRecord>(line, _options);
            return true;
        }
#pragma warning disable CA1031 // catch JsonException to implement error-handling policy
        catch (JsonException ex)
#pragma warning restore CA1031
        {
            if (ErrorHandling == ErrorHandling.Throw) { throw; }
            var error = new JsonDeserializationError(
                itemIndex: _errors.Count + CurrentItemCount + CurrentSkippedItemCount,
                lineNumber: lineNum,
                rawContent: line,
                exception: ex);
            if (ErrorHandling == ErrorHandling.CaptureAndContinue) { _errors.Add(error); }
            JsonLogMessages.DeserializationErrorAtLine(_logger, lineNum, ex);
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
