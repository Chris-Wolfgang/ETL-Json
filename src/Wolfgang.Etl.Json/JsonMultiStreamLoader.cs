using System;
using System.Collections.Generic;
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
public sealed class JsonMultiStreamLoader<TRecord> : LoaderBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private static readonly string OperationName = $"JSON multi-stream loading of {typeof(TRecord).Name}";
    private readonly Func<TRecord, Stream> _streamFactory;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
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
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streamFactory"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory,
        ILogger<JsonMultiStreamLoader<TRecord>>? logger = null
    )
    {
        _streamFactory = streamFactory ?? throw new ArgumentNullException(nameof(streamFactory));
        _logger = logger ?? (ILogger)NullLogger.Instance;
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
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streamFactory"/> or <paramref name="options"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory,
        JsonSerializerOptions options,
        ILogger<JsonMultiStreamLoader<TRecord>>? logger = null
    )
    {
        _streamFactory = streamFactory ?? throw new ArgumentNullException(nameof(streamFactory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="streamFactory">
    /// A factory function that receives the item to be written and returns a <see cref="Stream"/> to write it to.
    /// </param>
    /// <param name="options">The JSON serializer options, or <c>null</c> for defaults.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory,
        JsonSerializerOptions options,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        _streamFactory = streamFactory ?? throw new ArgumentNullException(nameof(streamFactory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with source-generated type metadata for AOT-friendly, reflection-free serialization.
    /// </summary>
    /// <param name="streamFactory">
    /// A factory function that receives the item to be written and returns a <see cref="Stream"/> to write it to.
    /// The loader will dispose the stream after writing.
    /// </param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streamFactory"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonMultiStreamLoader<TRecord>>? logger = null
    )
    {
        _streamFactory = streamFactory ?? throw new ArgumentNullException(nameof(streamFactory));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with source-generated type metadata and an injected progress timer for testing.
    /// </summary>
    /// <param name="streamFactory">
    /// A factory function that receives the item to be written and returns a <see cref="Stream"/> to write it to.
    /// </param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        _streamFactory = streamFactory ?? throw new ArgumentNullException(nameof(streamFactory));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
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

        var streamIndex = 0;

        await foreach (var item in items.WithCancellation(token).ConfigureAwait(false))
        {
            token.ThrowIfCancellationRequested();

            if (CurrentSkippedItemCount < SkipItemCount)
            {
                IncrementCurrentSkippedItemCount();
                JsonLogMessages.SkippedItem(_logger, CurrentSkippedItemCount, SkipItemCount, null);
                continue;
            }

            if (CurrentItemCount >= MaximumItemCount)
            {
                JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                break;
            }

            var stream = _streamFactory(item);
            if (stream is null)
            {
                JsonLogMessages.StreamFactoryReturnedNull(_logger, streamIndex, null);
                throw new InvalidOperationException($"Stream factory returned null for item at index {streamIndex}.");
            }

            try
            {
                await SerializeToStreamAsync(stream, item, token).ConfigureAwait(false);
#if NETSTANDARD2_0 || NET462 || NET481
#pragma warning disable CA2016, MA0040 // FlushAsync(CancellationToken) not available on this TFM
                await stream.FlushAsync().ConfigureAwait(false);
#pragma warning restore CA2016, MA0040
#else
                await stream.FlushAsync(token).ConfigureAwait(false);
#endif
                IncrementCurrentItemCount();
                streamIndex++;
                JsonLogMessages.LoadedItemToStream(_logger, CurrentItemCount, streamIndex - 1, null);
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

        JsonLogMessages.MultiStreamLoadingCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, streamIndex, null);
    }



    private Task SerializeToStreamAsync(Stream stream, TRecord item, CancellationToken token) =>
        _typeInfo is not null
            ? JsonSerializer.SerializeAsync(stream, item, _typeInfo, token)
            : JsonSerializer.SerializeAsync(stream, item, _options, token);



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
