using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Loads items of type <typeparamref name="TRecord"/> into a single JSON array stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to load. Must be <c>notnull</c>.</typeparam>
/// <remarks>
/// Writes a JSON array (e.g. <c>[{"name":"Alice"},{"name":"Bob"}]</c>) to a <see cref="Stream"/>
/// by serializing each item from the input async enumerable sequence.
/// </remarks>
/// <example>
/// <code>
/// using var stream = File.Create("output.json");
/// var loader = new JsonSingleStreamLoader&lt;Person&gt;(stream);
/// await loader.LoadAsync(items, cancellationToken);
/// </code>
/// </example>
public sealed class JsonSingleStreamLoader<TRecord> : LoaderBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private static readonly string OperationName = $"JSON single-stream loading of {typeof(TRecord).Name}";
    private static readonly KeyValuePair<string, object?> RecordTypeTag = new("etl.record_type", typeof(TRecord).Name);
    // Buffered bytes are written to the stream once they reach this size (an item is never split).
    private const int FlushThreshold = 32 * 1024;

    private readonly Stream _stream;
    private ByteBufferWriter? _scratchBuffer;
    private Utf8JsonWriter? _scratchWriter;
    private readonly RecordTypeInfo<TRecord> _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private int _progressTimerWired;



    private bool _isDryRun;

    /// <summary>
    /// Gets or sets a value indicating whether the run is a dry run that exercises the pipeline without
    /// writing any output.
    /// </summary>
    /// <remarks>
    /// When <see langword="true"/>, the loader enumerates the source and increments
    /// progress counters as usual but does not write any JSON to the output stream.
    /// </remarks>
    public bool IsDryRun { get => _isDryRun; [Obsolete("Configure IsDryRun through JsonSingleStreamLoaderOptions passed to the constructor instead. The setter will be removed in a later release.")] set => _isDryRun = value; }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Retained for binary compatibility with assemblies compiled before the optional-logger overload
    /// existed: <c>new JsonSingleStreamLoader&lt;T&gt;(stream)</c> in such an assembly is bound to this exact signature,
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
    public JsonSingleStreamLoader
    (
        Stream stream
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = NullLogger.Instance;
        _typeInfo = RecordTypeInfo<TRecord>.FromOptions(options: null);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class
    /// with diagnostic logging.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
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
    public JsonSingleStreamLoader
    (
        Stream stream,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _typeInfo = RecordTypeInfo<TRecord>.FromOptions(options: null);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="options">The JSON serializer options to use for serialization, or <c>null</c> for the serializer default.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Superseded by the overload that takes a <see cref="JsonSingleStreamLoaderOptions"/> record before the serializer options.
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
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonSerializerOptions? options = null,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = RecordTypeInfo<TRecord>.FromOptions(options);
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class configured through an options record.
    /// </summary>
    /// <param name="stream">The stream to write a single JSON array to.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="LoaderOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonSingleStreamLoaderOptions options,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
        : base(options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = RecordTypeInfo<TRecord>.FromOptions((options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions);
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class configured through an options record and a source-generated <see cref="JsonTypeInfo{TRecord}"/>.
    /// </summary>
    /// <param name="stream">The stream to write a single JSON array to.</param>
    /// <param name="typeInfo">The source-generated type information used to serialize <typeparamref name="TRecord"/>; it carries its own serializer options.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="LoaderOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="options"/> sets <see cref="JsonSingleStreamLoaderOptions.SerializerOptions"/>, which cannot be combined with a <paramref name="typeInfo"/>; the type info carries its own serializer options.</exception>
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        JsonSingleStreamLoaderOptions options,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
        : base(options)
    {
        RejectSerializerOptions(options);
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = RecordTypeInfo<TRecord>.FromTypeInfo(typeInfo ?? throw new ArgumentNullException(nameof(typeInfo)));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }



    /// <summary>
    /// Rejects a record that sets <see cref="JsonSingleStreamLoaderOptions.SerializerOptions"/> when a source-generated type info is
    /// supplied: the type info carries its own serializer options, so the record's would be ignored, and an ignored
    /// setting is worse than an error.
    /// </summary>
    /// <param name="options">The record to check.</param>
    /// <exception cref="ArgumentException">The record sets <see cref="JsonSingleStreamLoaderOptions.SerializerOptions"/>.</exception>
    private static void RejectSerializerOptions(JsonSingleStreamLoaderOptions? options)
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
    private void ApplyOptions(JsonSingleStreamLoaderOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _isDryRun = options.IsDryRun;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="options">The construction-time configuration, including <see cref="JsonSingleStreamLoaderOptions.SerializerOptions"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
#endif
    internal JsonSingleStreamLoader
    (
        Stream stream,
        JsonSingleStreamLoaderOptions options,
        IProgressTimer timer,
        ILogger? logger = null
    )
        : base(options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = RecordTypeInfo<TRecord>.FromOptions((options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions);
        _logger = logger ?? NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
        ApplyOptions(options);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class
    /// with source-generated type metadata for AOT-friendly, reflection-free serialization.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = RecordTypeInfo<TRecord>.FromTypeInfo(typeInfo ?? throw new ArgumentNullException(nameof(typeInfo)));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class
    /// with source-generated type metadata and an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    internal JsonSingleStreamLoader
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        IProgressTimer timer,
        ILogger? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = RecordTypeInfo<TRecord>.FromTypeInfo(typeInfo ?? throw new ArgumentNullException(nameof(typeInfo)));
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

        var sw = Stopwatch.StartNew();

        // Disposed by hand: `await using var` can't take ConfigureAwait(false), and a context-captured
        // DisposeAsync can deadlock a sync-over-async host on net462/netstandard2.0. The writer targets
        // an in-memory buffer so no synchronous Stream.Write happens; it is written with WriteAsync.
        var buffer = IsDryRun ? null : new ByteBufferWriter();
        var writer = buffer is null ? null : new Utf8JsonWriter(buffer, CreateWriterOptions());
        try
        {
            writer?.WriteStartArray();

            await foreach (var item in ItemLimit.Until(items, () => CurrentItemCount >= MaximumItemCount, token).ConfigureAwait(false))
            {
                token.ThrowIfCancellationRequested();

                if (CurrentSkippedItemCount < SkipItemCount)
                {
                    IncrementCurrentSkippedItemCount();
                    JsonMetrics.AddSkipped(JsonMetrics.LoadOperationTag, JsonMetrics.JsonSingleStreamComponentTag, RecordTypeTag);
                    JsonLogMessages.SkippedItem(_logger, CurrentSkippedItemCount, SkipItemCount, null);
                    continue;
                }

                if (writer is not null && buffer is not null && !await WriteItemAsync(writer, buffer, item, token).ConfigureAwait(false))
                {
                    continue;
                }

                IncrementCurrentItemCount();
                JsonMetrics.AddLoaded(JsonMetrics.LoadOperationTag, JsonMetrics.JsonSingleStreamComponentTag, RecordTypeTag);
                JsonLogMessages.LoadedItem(_logger, CurrentItemCount, null);
            }

            if (CurrentItemCount >= MaximumItemCount)
            {
                JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
            }

            if (writer is not null && buffer is not null)
            {
                await CompleteArrayAsync(writer, buffer, token).ConfigureAwait(false);
            }

            JsonLogMessages.SingleStreamLoadingCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, null);
            JsonMetrics.RecordDuration(sw.Elapsed.TotalMilliseconds, JsonMetrics.LoadOperationTag, JsonMetrics.JsonSingleStreamComponentTag, RecordTypeTag);
        }
        finally
        {
            if (writer is not null)
            {
                await writer.DisposeAsync().ConfigureAwait(false);
            }

            buffer?.Dispose();
            await ReleaseScratchAsync().ConfigureAwait(false);
        }
    }



    /// <summary>
    /// Builds the writer options from the serializer configuration in force.
    /// <see cref="JsonSerializer"/> takes indentation, encoder and depth from the
    /// <see cref="Utf8JsonWriter"/> it writes to, not from the serializer options passed alongside,
    /// so they must be copied onto the writer or they are silently ignored.
    /// </summary>
    private JsonWriterOptions CreateWriterOptions()
    {
        // Both construction paths end in a JsonTypeInfo whose Options are the serializer options in
        // force (JsonSerializerOptions.Default when none were given, which maps onto the default
        // writer options).
        var serializerOptions = _typeInfo.Value.Options;
        return new JsonWriterOptions
        {
            Indented = serializerOptions.WriteIndented,
            IndentCharacter = serializerOptions.IndentCharacter,
            IndentSize = serializerOptions.IndentSize,
            NewLine = serializerOptions.NewLine,
            Encoder = serializerOptions.Encoder,
            MaxDepth = serializerOptions.MaxDepth,
        };
    }



    /// <returns><see langword="false"/> when the item failed to serialize and the error policy skipped it.</returns>
    private async Task<bool> WriteItemAsync(Utf8JsonWriter writer, ByteBufferWriter buffer, TRecord item, CancellationToken token)
    {
        if (!TryWriteItem(writer, item))
        {
            return false;
        }

        // WriteRawValue leaves its bytes in the writer until flushed; commit them to the buffer before sizing it.
        await writer.FlushAsync(token).ConfigureAwait(false);
        if (buffer.WrittenCount >= FlushThreshold)
        {
            await WriteBufferAsync(buffer, token).ConfigureAwait(false);
        }

        return true;
    }



    /// <summary>
    /// Writes <paramref name="item"/> as the next array element. The record is serialized to a scratch buffer first
    /// and then replayed into the array writer, so a record that fails to serialize leaves no partial element in
    /// the array and the failure can go through the stage's error policy.
    /// </summary>
    private bool TryWriteItem(Utf8JsonWriter writer, TRecord item)
    {
        // One scratch buffer and writer are reused for every item of the run, so honouring the error policy
        // does not allocate per record.
        var scratchBuffer = _scratchBuffer ??= new ByteBufferWriter();
        var scratchWriter = _scratchWriter ??= new Utf8JsonWriter(scratchBuffer, CreateWriterOptions());
        scratchBuffer.Clear();
        scratchWriter.Reset(scratchBuffer);
        try
        {
            JsonSerializer.Serialize(scratchWriter, item, _typeInfo.Value);
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException)
        {
            HandleSerializationError(ex);
            return false;
        }

        if (_typeInfo.Value.Options.WriteIndented)
        {
            // Compact scratch bytes would lose the array writer's indentation, so re-emit them through it.
            using var document = JsonDocument.Parse(new ReadOnlyMemory<byte>(scratchBuffer.Buffer, 0, scratchBuffer.WrittenCount));
            document.RootElement.WriteTo(writer);
        }
        else
        {
            writer.WriteRawValue(new ReadOnlySpan<byte>(scratchBuffer.Buffer, 0, scratchBuffer.WrittenCount), skipInputValidation: true);
        }

        return true;
    }



    /// <summary>
    /// Applies the stage's error policy to an item that failed to serialize: returns when the policy skips
    /// the item and re-throws the original failure when it aborts.
    /// </summary>
    /// <param name="exception">The serialization failure.</param>
    private void HandleSerializationError(Exception exception)
    {
        var context = new ItemErrorContext
        (
            CurrentItemCount + CurrentSkippedItemCount + CurrentErrorItemCount + 1,
            exception,
            rawContent: null
        );

        if (HandleItemError(context) == ItemErrorAction.Abort)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }



    private async Task ReleaseScratchAsync()
    {
        if (_scratchWriter is not null)
        {
            await _scratchWriter.DisposeAsync().ConfigureAwait(false);
            _scratchWriter = null;
        }

        _scratchBuffer?.Dispose();
        _scratchBuffer = null;
    }



    private async Task CompleteArrayAsync(Utf8JsonWriter writer, ByteBufferWriter buffer, CancellationToken token)
    {
        writer.WriteEndArray();
        await writer.FlushAsync(token).ConfigureAwait(false);
        await WriteBufferAsync(buffer, token).ConfigureAwait(false);
        await _stream.FlushAsync(token).ConfigureAwait(false);
    }



    private async Task WriteBufferAsync(ByteBufferWriter buffer, CancellationToken token)
    {
        if (buffer.WrittenCount == 0)
        {
            return;
        }

        await _stream.WriteAsync(buffer.Buffer, offset: 0, count: buffer.WrittenCount, token).ConfigureAwait(false);
        buffer.Clear();
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
