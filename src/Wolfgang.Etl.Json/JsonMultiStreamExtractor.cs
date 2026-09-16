using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Extracts items of type <typeparamref name="TRecord"/> from multiple streams,
/// reading one JSON object per stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to extract. Must be <c>notnull</c>.</typeparam>
/// <remarks>
/// Iterates over an enumerable of streams, deserializing a single <typeparamref name="TRecord"/> from each.
/// Each stream is disposed after the item is read.
/// Extraction stops when the enumerable is exhausted or <see cref="ExtractorBase{TSource, TProgress}.MaximumItemCount"/> is reached.
/// Supply <see cref="JsonNamedStream"/> sources to surface the current source name in progress reports
/// via <see cref="JsonReport.CurrentSourceName"/>.
/// </remarks>
/// <example>
/// <code>
/// var sources = Directory.GetFiles("data/", "*.json")
///     .Select(path => new JsonNamedStream(File.OpenRead(path), path));
/// var extractor = new JsonMultiStreamExtractor&lt;Person&gt;(sources);
/// await foreach (var person in extractor.ExtractAsync(cancellationToken))
/// {
///     Console.WriteLine(person.Name);
/// }
/// </code>
/// </example>
public sealed class JsonMultiStreamExtractor<TRecord> : ExtractorBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private static readonly string OperationName = $"JSON multi-stream extraction of {typeof(TRecord).Name}";
    private static readonly KeyValuePair<string, object?> _recordTypeTag = new("etl.record_type", typeof(TRecord).Name);
    private readonly IEnumerable<JsonNamedStream> _sources;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private int _progressTimerWired;
    private volatile string? _currentSourceName;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streams"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Retained for binary compatibility with assemblies compiled before the optional-logger overload
    /// existed: <c>new JsonMultiStreamExtractor&lt;T&gt;(streams)</c> in such an assembly is bound to this exact signature,
    /// and removing it would fail at runtime with <see cref="MissingMethodException"/> with no compile-time
    /// signal. Hidden from IntelliSense; source code binds here too, so nothing changes for callers. New
    /// code has no reason to name this overload.
    /// </remarks>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    [EditorBrowsable(EditorBrowsableState.Never)]
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _logger = NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with named sources for progress reporting.
    /// </summary>
    /// <param name="sources">
    /// An enumerable of <see cref="JsonNamedStream"/> instances, each containing a stream and an optional name.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="sources"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Retained for binary compatibility with assemblies compiled before the optional-logger overload
    /// existed: <c>new JsonMultiStreamExtractor&lt;T&gt;(sources)</c> in such an assembly is bound to this exact signature,
    /// and removing it would fail at runtime with <see cref="MissingMethodException"/> with no compile-time
    /// signal. Hidden from IntelliSense; source code binds here too, so nothing changes for callers. New
    /// code has no reason to name this overload.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources
    )
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _logger = NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with diagnostic logging.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <param name="logger">
    /// An optional logger instance for diagnostic output. When <c>null</c> — or omitted —
    /// <see cref="NullLogger.Instance"/> is used and logging is disabled.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streams"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with named sources and diagnostic logging.
    /// </summary>
    /// <param name="sources">
    /// An enumerable of <see cref="JsonNamedStream"/> instances, each containing a stream and an optional name.
    /// </param>
    /// <param name="logger">
    /// An optional logger instance for diagnostic output. When <c>null</c> — or omitted —
    /// <see cref="NullLogger.Instance"/> is used and logging is disabled.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="sources"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <param name="options">The JSON serializer options to use for deserialization, or <c>null</c> for the serializer default.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streams"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonSerializerOptions? options = null,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _options = options;
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with named sources and custom serialization options.
    /// </summary>
    /// <param name="sources">
    /// An enumerable of <see cref="JsonNamedStream"/> instances, each containing a stream and an optional name.
    /// </param>
    /// <param name="options">The JSON serializer options to use for deserialization, or <c>null</c> for the serializer default.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="sources"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        JsonSerializerOptions? options = null,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _options = options;
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class configured through an options record.
    /// </summary>
    /// <param name="streams">The streams to read from, in order.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="ExtractorOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonMultiStreamExtractorOptions options,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
        : base(options)
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _options = (options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions;
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class configured through an options record and a source-generated <see cref="JsonTypeInfo{TRecord}"/>.
    /// </summary>
    /// <param name="streams">The streams to read from, in order.</param>
    /// <param name="typeInfo">The source-generated type information used to deserialize <typeparamref name="TRecord"/>; it carries its own serializer options.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="ExtractorOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="options"/> sets <see cref="JsonMultiStreamExtractorOptions.SerializerOptions"/>, which cannot be combined with a <paramref name="typeInfo"/>; the type info carries its own serializer options.</exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonTypeInfo<TRecord> typeInfo,
        JsonMultiStreamExtractorOptions options,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
        : base(options)
    {
        RejectSerializerOptions(options);
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class configured through an options record.
    /// </summary>
    /// <param name="sources">The named sources to read from, in order.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="ExtractorOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        JsonMultiStreamExtractorOptions options,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
        : base(options)
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _options = (options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions;
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class configured through an options record and a source-generated <see cref="JsonTypeInfo{TRecord}"/>.
    /// </summary>
    /// <param name="sources">The named sources to read from, in order.</param>
    /// <param name="typeInfo">The source-generated type information used to deserialize <typeparamref name="TRecord"/>; it carries its own serializer options.</param>
    /// <param name="options">The construction-time configuration for this stage, including the settings inherited from <see cref="ExtractorOptions"/>.</param>
    /// <param name="logger">An optional logger; <see langword="null"/> disables logging.</param>
    /// <exception cref="ArgumentNullException">A required argument is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="options"/> sets <see cref="JsonMultiStreamExtractorOptions.SerializerOptions"/>, which cannot be combined with a <paramref name="typeInfo"/>; the type info carries its own serializer options.</exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        JsonTypeInfo<TRecord> typeInfo,
        JsonMultiStreamExtractorOptions options,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
        : base(options)
    {
        RejectSerializerOptions(options);
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        ApplyOptions(options);
    }



    /// <summary>
    /// Rejects a record that sets <see cref="JsonMultiStreamExtractorOptions.SerializerOptions"/> when a source-generated type info is
    /// supplied: the type info carries its own serializer options, so the record's would be ignored, and an ignored
    /// setting is worse than an error.
    /// </summary>
    /// <param name="options">The record to check.</param>
    /// <exception cref="ArgumentException">The record sets <see cref="JsonMultiStreamExtractorOptions.SerializerOptions"/>.</exception>
    private static void RejectSerializerOptions(JsonMultiStreamExtractorOptions options)
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
    private void ApplyOptions(JsonMultiStreamExtractorOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <param name="options">The construction-time configuration, including <see cref="JsonMultiStreamExtractorOptions.SerializerOptions"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    internal JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonMultiStreamExtractorOptions options,
        IProgressTimer timer,
        ILogger? logger = null
    )
        : base(options)
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _options = (options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions;
        _logger = logger ?? NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
        ApplyOptions(options);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with named sources and an injected progress timer for testing.
    /// </summary>
    /// <param name="sources">An enumerable of <see cref="JsonNamedStream"/> instances.</param>
    /// <param name="options">The construction-time configuration, including <see cref="JsonMultiStreamExtractorOptions.SerializerOptions"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    internal JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        JsonMultiStreamExtractorOptions options,
        IProgressTimer timer,
        ILogger? logger = null
    )
        : base(options)
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _options = (options ?? throw new ArgumentNullException(nameof(options))).SerializerOptions;
        _logger = logger ?? NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
        ApplyOptions(options);
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with source-generated type metadata for AOT-friendly, reflection-free deserialization.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streams"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with named sources and source-generated type metadata for AOT-friendly deserialization.
    /// </summary>
    /// <param name="sources">
    /// An enumerable of <see cref="JsonNamedStream"/> instances, each containing a stream and an optional name.
    /// </param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="sources"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with source-generated type metadata and an injected progress timer for testing.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="timer">The progress timer to inject.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    internal JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonTypeInfo<TRecord> typeInfo,
        IProgressTimer timer,
        ILogger? logger = null
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
    }





    /// <inheritdoc />
    protected override async IAsyncEnumerable<TRecord> ExtractWorkerAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        JsonLogMessages.StartingOperation(_logger, OperationName, null);

        var skipBudget = SkipItemCount;
        var streamIndex = 0;
        var sw = Stopwatch.StartNew();

        try
        {
            foreach (var source in _sources)
            {
                token.ThrowIfCancellationRequested();
                _currentSourceName = source.Name;
                JsonLogMessages.ReadingStream(_logger, streamIndex, null);

                var (item, failed) = await TryDeserializeStreamAsync(source.Stream, streamIndex, token).ConfigureAwait(false);
                streamIndex++;
                if (failed) { continue; }

                if (item is null)
                {
                    JsonLogMessages.StreamDeserializedToNull(_logger, streamIndex - 1, null);
                    continue;
                }

                if (skipBudget > 0)
                {
                    skipBudget--;
                    IncrementCurrentSkippedItemCount();
                    JsonMetrics.AddSkipped(JsonMetrics.ExtractOperationTag, JsonMetrics.JsonMultiStreamComponentTag, _recordTypeTag);
                    JsonLogMessages.SkippedItem(_logger, CurrentSkippedItemCount, SkipItemCount, null);
                    continue;
                }

                if (CurrentItemCount >= MaximumItemCount)
                {
                    JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                    break;
                }

                IncrementCurrentItemCount();
                JsonMetrics.AddExtracted(JsonMetrics.ExtractOperationTag, JsonMetrics.JsonMultiStreamComponentTag, _recordTypeTag);
                JsonLogMessages.ExtractedItemFromStream(_logger, CurrentItemCount, streamIndex - 1, null);

                yield return item;
            }

            JsonLogMessages.MultiStreamExtractionCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, streamIndex, null);
        }
        finally
        {
            JsonMetrics.RecordDuration(sw.Elapsed.TotalMilliseconds, JsonMetrics.ExtractOperationTag, JsonMetrics.JsonMultiStreamComponentTag, _recordTypeTag);
        }
    }



    private async System.Threading.Tasks.Task<(TRecord? Item, bool Failed)> TryDeserializeStreamAsync
    (
        Stream stream,
        int streamIndex,
        CancellationToken token
    )
    {
        TRecord? item = default;
        bool failed = false;
        JsonException? deserializationEx = null;
        try
        {
            item = _typeInfo is not null
                ? await JsonSerializer.DeserializeAsync(stream, _typeInfo, token).ConfigureAwait(false)
                : await JsonSerializer.DeserializeAsync<TRecord>(stream, _options, token).ConfigureAwait(false);
        }
#pragma warning disable CA1031 // catch JsonException to implement error-handling policy
        catch (JsonException ex)
#pragma warning restore CA1031
        {
            failed = true;
            deserializationEx = ex;
        }
        finally
        {
#if NETSTANDARD2_0 || NET462 || NET481
            stream.Dispose();
#else
            await stream.DisposeAsync().ConfigureAwait(false);
#endif
        }

        if (failed)
        {
            var context = new ItemErrorContext(streamIndex + 1, deserializationEx!, rawContent: null);
            if (HandleItemError(context) == ItemErrorAction.Abort)
            {
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(deserializationEx!).Throw();
            }
        }

        return (item, failed);
    }



    /// <inheritdoc />
    protected override JsonReport CreateProgressReport() =>
        new
        (
            CurrentItemCount,
            CurrentSkippedItemCount,
            _currentSourceName
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
