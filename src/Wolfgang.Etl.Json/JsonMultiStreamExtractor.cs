using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
/// var extractor = new JsonMultiStreamExtractor&lt;Person&gt;(sources, logger);
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
    private readonly IEnumerable<JsonNamedStream> _sources;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private readonly List<JsonDeserializationError> _errors = new();
    private int _progressTimerWired;
    private volatile string? _currentSourceName;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streams"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
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
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streams"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        ILogger<JsonMultiStreamExtractor<TRecord>> logger
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with named sources and diagnostic logging.
    /// </summary>
    /// <param name="sources">
    /// An enumerable of <see cref="JsonNamedStream"/> instances, each containing a stream and an optional name.
    /// </param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="sources"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        ILogger<JsonMultiStreamExtractor<TRecord>> logger
    )
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streams"/> or <paramref name="options"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonSerializerOptions options,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with named sources and custom serialization options.
    /// </summary>
    /// <param name="sources">
    /// An enumerable of <see cref="JsonNamedStream"/> instances, each containing a stream and an optional name.
    /// </param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="sources"/> or <paramref name="options"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        JsonSerializerOptions options,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    internal JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonSerializerOptions options,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class
    /// with named sources and an injected progress timer for testing.
    /// </summary>
    /// <param name="sources">An enumerable of <see cref="JsonNamedStream"/> instances.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonMultiStreamExtractor
    (
        IEnumerable<JsonNamedStream> sources,
        JsonSerializerOptions options,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
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
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        if (streams is null)
        {
            throw new ArgumentNullException(nameof(streams));
        }

        _sources = streams.Select(s => new JsonNamedStream(s));
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



    /// <inheritdoc />
    protected override async IAsyncEnumerable<TRecord> ExtractWorkerAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        _errors.Clear();
        JsonLogMessages.StartingOperation(_logger, OperationName, null);

        var skipBudget = SkipItemCount;
        var streamIndex = 0;

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
                JsonLogMessages.SkippedItem(_logger, CurrentSkippedItemCount, SkipItemCount, null);
                continue;
            }

            if (CurrentItemCount >= MaximumItemCount)
            {
                JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                break;
            }

            IncrementCurrentItemCount();
            JsonLogMessages.ExtractedItemFromStream(_logger, CurrentItemCount, streamIndex - 1, null);

            yield return item;
        }

        JsonLogMessages.MultiStreamExtractionCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, streamIndex, null);
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
            if (ErrorHandling == ErrorHandling.Throw)
            {
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(deserializationEx!).Throw();
            }

            var error = new JsonDeserializationError(
                itemIndex: streamIndex,
                lineNumber: null,
                rawContent: null,
                exception: deserializationEx!);
            if (ErrorHandling == ErrorHandling.CaptureAndContinue) { _errors.Add(error); }
            JsonLogMessages.DeserializationErrorAtIndex(_logger, streamIndex, deserializationEx);
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
