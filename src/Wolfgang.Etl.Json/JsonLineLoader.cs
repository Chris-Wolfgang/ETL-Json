using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Loads items of type <typeparamref name="TRecord"/> into a JSONL (JSON Lines / NDJSON) stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to load. Must be <c>notnull</c>.</typeparam>
/// <remarks>
/// Writes each item as a single line of JSON to the output stream,
/// with each line separated by a newline character.
/// Compatible with both JSONL and NDJSON consumers.
/// </remarks>
/// <example>
/// <code>
/// using var stream = File.Create("output.jsonl");
/// var loader = new JsonLineLoader&lt;Person&gt;(stream);
/// await loader.LoadAsync(items, cancellationToken);
/// </code>
/// </example>
public sealed class JsonLineLoader<TRecord> : LoaderBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private static readonly string OperationName = $"JSONL loading of {typeof(TRecord).Name}";
    private static readonly KeyValuePair<string, object?> _recordTypeTag = new("etl.record_type", typeof(TRecord).Name);
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private int _progressTimerWired;
    private long _currentLineNumber;



    private bool _isDryRun;

    /// <inheritdoc />
    /// <remarks>
    /// When <see langword="true"/>, the loader enumerates the source and increments
    /// progress counters as usual but does not write any JSON to the output stream.
    /// </remarks>
    public bool IsDryRun { get => _isDryRun; [Obsolete("Configure IsDryRun through JsonLineLoaderOptions passed to the constructor instead. The setter will be removed in a later release.")] set => _isDryRun = value; }



    private System.Text.Encoding? _encoding;

    /// <summary>
    /// Gets or sets the character encoding to use when writing the JSONL stream.
    /// When <see langword="null"/> (the default), UTF-8 is used.
    /// </summary>
    public System.Text.Encoding? Encoding { get => _encoding; [Obsolete("Configure Encoding through JsonLineLoaderOptions passed to the constructor instead. The setter will be removed in a later release.")] set => _encoding = value; }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Retained for binary compatibility with assemblies compiled before the optional-logger overload
    /// existed: <c>new JsonLineLoader&lt;T&gt;(stream)</c> in such an assembly is bound to this exact signature,
    /// and removing it would fail at runtime with <see cref="MissingMethodException"/> with no compile-time
    /// signal. Hidden from IntelliSense; source code binds here too, so nothing changes for callers. New
    /// code has no reason to name this overload.
    /// </remarks>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    [EditorBrowsable(EditorBrowsableState.Never)]
    public JsonLineLoader
    (
        Stream stream
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class
    /// with diagnostic logging.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <param name="logger">
    /// An optional logger instance for diagnostic output. When <c>null</c> — or omitted —
    /// <see cref="NullLogger.Instance"/> is used and logging is disabled.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    public JsonLineLoader
    (
        Stream stream,
        ILogger<JsonLineLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <param name="options">The JSON serializer options to use for serialization, or <c>null</c> for the serializer default.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Superseded by the overload that takes a <see cref="JsonLineLoaderOptions"/> record before the serializer options.
    /// Retained for binary compatibility with assemblies compiled against 0.8.x, which are bound to this exact
    /// signature; removing it would fail them at runtime with <see cref="MissingMethodException"/> with no
    /// compile-time signal. Hidden from IntelliSense; source code binds here too, so nothing changes for callers.
    /// New code passes the record.
    /// </remarks>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    [EditorBrowsable(EditorBrowsableState.Never)]
    public JsonLineLoader
    (
        Stream stream,
        JsonSerializerOptions? options = null,
        ILogger<JsonLineLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options;
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class configured through an options record.
    /// </summary>
    /// <param name="stream">The stream to write JSON Lines to.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="LoaderOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    public JsonLineLoader
    (
        Stream stream,
        JsonLineLoaderOptions options,
        ILogger<JsonLineLoader<TRecord>>? logger = null
    )
        : base(options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = (options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions;
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class configured through an options record and a source-generated <see cref="JsonTypeInfo{TRecord}"/>.
    /// </summary>
    /// <param name="stream">The stream to write JSON Lines to.</param>
    /// <param name="typeInfo">The source-generated type information used to serialize <typeparamref name="TRecord"/>; it carries its own serializer options.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="LoaderOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="options"/> sets <see cref="JsonLineLoaderOptions.SerializerOptions"/>, which cannot be combined with a <paramref name="typeInfo"/>; the type info carries its own serializer options.</exception>
    public JsonLineLoader
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        JsonLineLoaderOptions options,
        ILogger<JsonLineLoader<TRecord>>? logger = null
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
    /// Rejects a record that sets <see cref="JsonLineLoaderOptions.SerializerOptions"/> when a source-generated type info is
    /// supplied: the type info carries its own serializer options, so the record's would be ignored, and an ignored
    /// setting is worse than an error.
    /// </summary>
    /// <param name="options">The record to check.</param>
    /// <exception cref="ArgumentException">The record sets <see cref="JsonLineLoaderOptions.SerializerOptions"/>.</exception>
    private static void RejectSerializerOptions(JsonLineLoaderOptions options)
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
    /// settings were applied by the <see cref="LoaderBase{TDestination, TProgress}"/> constructor.
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <see langword="null"/>.</exception>
    private void ApplyOptions(JsonLineLoaderOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _encoding = options.Encoding;
        _isDryRun = options.IsDryRun;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <param name="options">The construction-time configuration, including <see cref="JsonLineLoaderOptions.SerializerOptions"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    internal JsonLineLoader
    (
        Stream stream,
        JsonLineLoaderOptions options,
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
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class
    /// with source-generated type metadata for AOT-friendly, reflection-free serialization.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonLineLoader
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonLineLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class
    /// with source-generated type metadata and an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    internal JsonLineLoader
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



    /// <inheritdoc />
    protected override async Task LoadWorkerAsync
    (
        IAsyncEnumerable<TRecord> items,
        CancellationToken token
    )
    {
        JsonLogMessages.StartingOperation(_logger, OperationName, null);
        token.ThrowIfCancellationRequested();

        var newLine = Encoding is null ? JsonMetrics.NewLineUtf8Bytes : Encoding.GetBytes(Environment.NewLine);
        var sw = Stopwatch.StartNew();

        await foreach (var item in items.WithCancellation(token).ConfigureAwait(false))
        {
            token.ThrowIfCancellationRequested();

            if (CurrentSkippedItemCount < SkipItemCount)
            {
                IncrementCurrentSkippedItemCount();
                JsonMetrics.AddSkipped(JsonMetrics.LoadOperationTag, JsonMetrics.JsonLineComponentTag, _recordTypeTag);
                JsonLogMessages.SkippedItem(_logger, CurrentSkippedItemCount, SkipItemCount, null);
                continue;
            }

            if (CurrentItemCount >= MaximumItemCount)
            {
                JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                break;
            }

            Interlocked.Increment(ref _currentLineNumber);

            if (!IsDryRun)
            {
                byte[] bytes;

                if (Encoding is null)
                {
                    bytes = _typeInfo is not null
                        ? JsonSerializer.SerializeToUtf8Bytes(item, _typeInfo)
                        : JsonSerializer.SerializeToUtf8Bytes(item, _options);
                }
                else
                {
                    var json = _typeInfo is not null
                        ? JsonSerializer.Serialize(item, _typeInfo)
                        : JsonSerializer.Serialize(item, _options);
                    bytes = Encoding.GetBytes(json);
                }

                await _stream.WriteAsync(bytes, offset: 0, count: bytes.Length, token).ConfigureAwait(false);
                await _stream.WriteAsync(newLine, offset: 0, count: newLine.Length, token).ConfigureAwait(false);
            }

            IncrementCurrentItemCount();
            JsonMetrics.AddLoaded(JsonMetrics.LoadOperationTag, JsonMetrics.JsonLineComponentTag, _recordTypeTag);
            JsonLogMessages.LoadedItemAtLine(_logger, CurrentItemCount, Interlocked.Read(ref _currentLineNumber), null);
        }

        JsonLogMessages.JsonlLoadingCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, Interlocked.Read(ref _currentLineNumber), null);
        JsonMetrics.RecordDuration(sw.Elapsed.TotalMilliseconds, JsonMetrics.LoadOperationTag, JsonMetrics.JsonLineComponentTag, _recordTypeTag);
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
}
