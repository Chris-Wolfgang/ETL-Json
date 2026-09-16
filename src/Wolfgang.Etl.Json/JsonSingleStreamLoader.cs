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
    private static readonly KeyValuePair<string, object?> _recordTypeTag = new("etl.record_type", typeof(TRecord).Name);
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private int _progressTimerWired;



    /// <inheritdoc />
    /// <remarks>
    /// When <see langword="true"/>, the loader enumerates the source and increments
    /// progress counters as usual but does not write any JSON to the output stream.
    /// </remarks>
    public bool IsDryRun { get; [Obsolete("Configure IsDryRun through JsonSingleStreamLoaderOptions passed to the constructor instead. The setter will be removed in a later release.")] set; }



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
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    [EditorBrowsable(EditorBrowsableState.Never)]
    public JsonSingleStreamLoader
    (
        Stream stream
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = NullLogger.Instance;
        _options = null;
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
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonSingleStreamLoader
    (
        Stream stream,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="serializerOptions">The JSON serializer options to use for serialization, or <c>null</c> for the serializer default.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonSerializerOptions? serializerOptions = null,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = serializerOptions;
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class configured through an options record.
    /// </summary>
    /// <param name="stream">The stream to write a single JSON array to.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="LoaderOptions"/>.</param>
    /// <param name="serializerOptions">Optional <see cref="JsonSerializerOptions"/> for the serializer; <see langword="null"/> uses the defaults.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonSingleStreamLoaderOptions options,
        JsonSerializerOptions? serializerOptions = null,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
        : base(options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = serializerOptions;
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
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        JsonSingleStreamLoaderOptions options,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
        : base(options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }



    /// <summary>
    /// Copies the stage-specific settings from <paramref name="options"/> onto this instance; the inherited
    /// settings were applied by the <see cref="LoaderBase{TDestination, TProgress}"/> constructor.
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <see langword="null"/>.</exception>
#pragma warning disable CS0618 // ApplyOptions is the supported replacement for these setters; it necessarily writes them.
    private void ApplyOptions(JsonSingleStreamLoaderOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        IsDryRun = options.IsDryRun;
    }
#pragma warning restore CS0618



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="serializerOptions">The JSON serializer options to use for serialization, or <c>null</c> for the serializer default.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    internal JsonSingleStreamLoader
    (
        Stream stream,
        JsonSerializerOptions serializerOptions,
        IProgressTimer timer,
        ILogger? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = serializerOptions ?? throw new ArgumentNullException(nameof(serializerOptions));
        _logger = logger ?? NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
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
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
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

        var sw = Stopwatch.StartNew();

        // CA2007/MA0004: await using declarations do not support ConfigureAwait in C#
#pragma warning disable CA2007, MA0004
        await using var writer = IsDryRun ? null : new Utf8JsonWriter(_stream);
#pragma warning restore CA2007, MA0004

        writer?.WriteStartArray();

        await foreach (var item in items.WithCancellation(token).ConfigureAwait(false))
        {
            token.ThrowIfCancellationRequested();

            if (CurrentSkippedItemCount < SkipItemCount)
            {
                IncrementCurrentSkippedItemCount();
                JsonMetrics.AddSkipped(JsonMetrics.LoadOperationTag, JsonMetrics.JsonSingleStreamComponentTag, _recordTypeTag);
                JsonLogMessages.SkippedItem(_logger, CurrentSkippedItemCount, SkipItemCount, null);
                continue;
            }

            if (CurrentItemCount >= MaximumItemCount)
            {
                JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                break;
            }

            if (writer is not null)
            {
                if (_typeInfo is not null)
                {
                    JsonSerializer.Serialize(writer, item, _typeInfo);
                }
                else
                {
                    JsonSerializer.Serialize(writer, item, _options);
                }
            }

            IncrementCurrentItemCount();
            JsonMetrics.AddLoaded(JsonMetrics.LoadOperationTag, JsonMetrics.JsonSingleStreamComponentTag, _recordTypeTag);
            JsonLogMessages.LoadedItem(_logger, CurrentItemCount, null);
        }

        writer?.WriteEndArray();
        if (writer is not null)
        {
            await writer.FlushAsync(token).ConfigureAwait(false);
        }

        JsonLogMessages.SingleStreamLoadingCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, null);
        JsonMetrics.RecordDuration(sw.Elapsed.TotalMilliseconds, JsonMetrics.LoadOperationTag, JsonMetrics.JsonSingleStreamComponentTag, _recordTypeTag);
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
