using System;
using System.Collections.Generic;
using System.IO;
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
/// Iterates over an <see cref="IEnumerable{T}"/> of <see cref="Stream"/> instances,
/// deserializing a single <typeparamref name="TRecord"/> from each stream.
/// Each stream is disposed after the item is read.
/// Extraction stops when the enumerable is exhausted or <see cref="ExtractorBase{TSource, TProgress}.MaximumItemCount"/> is reached.
/// </remarks>
/// <example>
/// <code>
/// var streams = Directory.GetFiles("data/", "*.json").Select(File.OpenRead);
/// var extractor = new JsonMultiStreamExtractor&lt;Person&gt;(streams, logger);
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
    private readonly IEnumerable<Stream> _streams;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private int _progressTimerWired;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamExtractor{TRecord}"/> class.
    /// </summary>
    /// <param name="streams">An enumerable of streams, each containing a single JSON object.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streams"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams
    )
    {
        _streams = streams ?? throw new ArgumentNullException(nameof(streams));
        _logger = NullLogger.Instance;
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
    public JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonSerializerOptions options,
        ILogger<JsonMultiStreamExtractor<TRecord>>? logger = null
    )
    {
        _streams = streams ?? throw new ArgumentNullException(nameof(streams));
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
    internal JsonMultiStreamExtractor
    (
        IEnumerable<Stream> streams,
        JsonSerializerOptions options,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        _streams = streams ?? throw new ArgumentNullException(nameof(streams));
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
        _streams = streams ?? throw new ArgumentNullException(nameof(streams));
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
        _streams = streams ?? throw new ArgumentNullException(nameof(streams));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
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

        foreach (var stream in _streams)
        {
            token.ThrowIfCancellationRequested();
            JsonLogMessages.ReadingStream(_logger, streamIndex, null);

            TRecord? item;
            try
            {
                item = _typeInfo is not null
                    ? await JsonSerializer.DeserializeAsync(stream, _typeInfo, token).ConfigureAwait(false)
                    : await JsonSerializer.DeserializeAsync<TRecord>(stream, _options, token).ConfigureAwait(false);
            }
            finally
            {
#if NETSTANDARD2_0 || NET462 || NET481
                stream.Dispose();
#else
                await stream.DisposeAsync().ConfigureAwait(false);
#endif
            }

            streamIndex++;

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
