using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Loads items of type <typeparamref name="TRecord"/> into multiple streams,
/// writing one JSON object per stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to load. Must be <c>notnull</c>.</typeparam>
/// <remarks>
/// For each item in the input sequence, calls a factory function to obtain a <see cref="Stream"/>,
/// serializes the item as a single JSON object, and disposes the stream.
/// The factory receives the item being written, allowing stream creation based on item properties
/// (e.g., generating file names from record fields).
/// </remarks>
/// <example>
/// <code>
/// var loader = new JsonMultiStreamLoader&lt;Person&gt;
/// (
///     person => File.Create($"output/{person.Id}.json"),
///     logger
/// );
/// await loader.LoadAsync(items, cancellationToken);
/// </code>
/// </example>
public class JsonMultiStreamLoader<TRecord> : LoaderBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private readonly Func<TRecord, Stream> _streamFactory;
    private readonly JsonSerializerOptions? _options;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private bool _progressTimerWired;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class.
    /// </summary>
    /// <param name="streamFactory">
    /// A factory function that receives the item to be written and returns a <see cref="Stream"/> to write it to.
    /// The loader will dispose the stream after writing.
    /// </param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streamFactory"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory,
        ILogger<JsonMultiStreamLoader<TRecord>> logger
    )
    {
        _streamFactory = streamFactory ?? throw new ArgumentNullException(nameof(streamFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="streamFactory">
    /// A factory function that receives the item to be written and returns a <see cref="Stream"/> to write it to.
    /// The loader will dispose the stream after writing.
    /// </param>
    /// <param name="options">The JSON serializer options to use for serialization.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streamFactory"/>, <paramref name="options"/>, or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory,
        JsonSerializerOptions options,
        ILogger<JsonMultiStreamLoader<TRecord>> logger
    )
    {
        _streamFactory = streamFactory ?? throw new ArgumentNullException(nameof(streamFactory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="streamFactory">
    /// A factory function that receives the item to be written and returns a <see cref="Stream"/> to write it to.
    /// </param>
    /// <param name="options">The JSON serializer options, or <c>null</c> for defaults.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory,
        JsonSerializerOptions? options,
        ILogger logger,
        IProgressTimer timer
    )
    {
        _streamFactory = streamFactory ?? throw new ArgumentNullException(nameof(streamFactory));
        _options = options;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
    }



    /// <inheritdoc />
    protected override async Task LoadWorkerAsync
    (
        IAsyncEnumerable<TRecord> items,
        CancellationToken token
    )
    {
        _logger.LogDebug("Starting JSON multi-stream loading of {RecordType}.", typeof(TRecord).Name);

        var streamIndex = 0;

        await foreach (var item in items.WithCancellation(token).ConfigureAwait(false))
        {
            token.ThrowIfCancellationRequested();

            if (CurrentSkippedItemCount < SkipItemCount)
            {
                IncrementCurrentSkippedItemCount();
                _logger.LogDebug("Skipped item {SkippedCount} of {SkipTotal}.", CurrentSkippedItemCount, SkipItemCount);
                continue;
            }

            if (CurrentItemCount >= MaximumItemCount)
            {
                _logger.LogDebug("Reached MaximumItemCount of {MaximumItemCount}. Stopping.", MaximumItemCount);
                break;
            }

            var stream = _streamFactory(item);
            if (stream is null)
            {
                _logger.LogError("Stream factory returned null for item at index {StreamIndex}.", streamIndex);
                throw new InvalidOperationException($"Stream factory returned null for item at index {streamIndex}.");
            }

            try
            {
                await JsonSerializer.SerializeAsync(stream, item, _options, token).ConfigureAwait(false);
#if NETSTANDARD2_0 || NET462 || NET481
                await stream.FlushAsync().ConfigureAwait(false);
#else
                await stream.FlushAsync(token).ConfigureAwait(false);
#endif
                IncrementCurrentItemCount();
                streamIndex++;
                _logger.LogDebug("Loaded item {CurrentItemCount} to stream {StreamIndex}.", CurrentItemCount, streamIndex - 1);
            }
            finally
            {
#if NETSTANDARD2_0 || NET462 || NET481
                stream.Dispose();
#else
                await stream.DisposeAsync().ConfigureAwait(false);
#endif
            }
        }

        _logger.LogInformation
        (
            "Multi-stream loading completed. Loaded: {ItemCount}, skipped: {SkippedCount}, streams: {StreamCount}.",
            CurrentItemCount, CurrentSkippedItemCount, streamIndex
        );
    }



    /// <inheritdoc />
    protected override JsonReport CreateProgressReport()
    {
        return new JsonReport
        (
            CurrentItemCount,
            CurrentSkippedItemCount
        );
    }



    /// <inheritdoc />
    protected override IProgressTimer CreateProgressTimer(IProgress<JsonReport> progress)
    {
        if (_progressTimer is not null)
        {
            if (!_progressTimerWired)
            {
                _progressTimerWired = true;
                _progressTimer.Elapsed += () => progress.Report(CreateProgressReport());
            }

            return _progressTimer;
        }

        return base.CreateProgressTimer(progress);
    }
}
