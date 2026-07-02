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
/// For each item in the input sequence, calls a factory function to obtain a stream,
/// serializes the item as a single JSON object, and disposes the stream.
/// Supply a <see cref="JsonNamedDestination"/> factory to surface the current destination name
/// in progress reports via <see cref="JsonReport.CurrentSourceName"/>.
/// </remarks>
/// <example>
/// <code>
/// var loader = new JsonMultiStreamLoader&lt;Person&gt;
/// (
///     person => new JsonNamedDestination(File.Create($"output/{person.Id}.json"), $"output/{person.Id}.json"),
///     logger
/// );
/// await loader.LoadAsync(items, cancellationToken);
/// </code>
/// </example>
public sealed class JsonMultiStreamLoader<TRecord> : LoaderBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private static readonly string OperationName = $"JSON multi-stream loading of {typeof(TRecord).Name}";
    private readonly Func<TRecord, JsonNamedDestination> _destinationFactory;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private int _progressTimerWired;
    private volatile string? _currentDestinationName;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class.
    /// </summary>
    /// <param name="streamFactory">
    /// A factory function that receives the item to be written and returns a <see cref="Stream"/> to write it to.
    /// The loader will dispose the stream after writing.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="streamFactory"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, Stream> streamFactory
    )
    {
        if (streamFactory is null)
        {
            throw new ArgumentNullException(nameof(streamFactory));
        }

        _destinationFactory = item => new JsonNamedDestination(streamFactory(item));
        _logger = NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with a named-destination factory for progress reporting.
    /// </summary>
    /// <param name="destinationFactory">
    /// A factory function that receives the item to be written and returns a <see cref="JsonNamedDestination"/>
    /// containing the stream and an optional name. The loader will dispose the stream after writing.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="destinationFactory"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, JsonNamedDestination> destinationFactory
    )
    {
        _destinationFactory = destinationFactory ?? throw new ArgumentNullException(nameof(destinationFactory));
        _logger = NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with diagnostic logging.
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
        if (streamFactory is null)
        {
            throw new ArgumentNullException(nameof(streamFactory));
        }

        _destinationFactory = item => new JsonNamedDestination(streamFactory(item));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with a named-destination factory and diagnostic logging.
    /// </summary>
    /// <param name="destinationFactory">
    /// A factory function that receives the item to be written and returns a <see cref="JsonNamedDestination"/>
    /// containing the stream and an optional name. The loader will dispose the stream after writing.
    /// </param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="destinationFactory"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, JsonNamedDestination> destinationFactory,
        ILogger<JsonMultiStreamLoader<TRecord>> logger
    )
    {
        _destinationFactory = destinationFactory ?? throw new ArgumentNullException(nameof(destinationFactory));
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
        if (streamFactory is null)
        {
            throw new ArgumentNullException(nameof(streamFactory));
        }

        _destinationFactory = item => new JsonNamedDestination(streamFactory(item));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with a named-destination factory and custom serialization options.
    /// </summary>
    /// <param name="destinationFactory">
    /// A factory function that receives the item to be written and returns a <see cref="JsonNamedDestination"/>
    /// containing the stream and an optional name. The loader will dispose the stream after writing.
    /// </param>
    /// <param name="options">The JSON serializer options to use for serialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="destinationFactory"/> or <paramref name="options"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, JsonNamedDestination> destinationFactory,
        JsonSerializerOptions options,
        ILogger<JsonMultiStreamLoader<TRecord>>? logger = null
    )
    {
        _destinationFactory = destinationFactory ?? throw new ArgumentNullException(nameof(destinationFactory));
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
    /// <param name="options">The JSON serializer options to use for serialization.</param>
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
        if (streamFactory is null)
        {
            throw new ArgumentNullException(nameof(streamFactory));
        }

        _destinationFactory = item => new JsonNamedDestination(streamFactory(item));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with a named-destination factory and an injected progress timer for testing.
    /// </summary>
    /// <param name="destinationFactory">
    /// A factory function that returns a <see cref="JsonNamedDestination"/> for each item.
    /// The loader will dispose the stream after writing.
    /// </param>
    /// <param name="options">The JSON serializer options to use for serialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonMultiStreamLoader
    (
        Func<TRecord, JsonNamedDestination> destinationFactory,
        JsonSerializerOptions options,
        ILogger? logger,
        IProgressTimer timer
    )
    {
        _destinationFactory = destinationFactory ?? throw new ArgumentNullException(nameof(destinationFactory));
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
        if (streamFactory is null)
        {
            throw new ArgumentNullException(nameof(streamFactory));
        }

        _destinationFactory = item => new JsonNamedDestination(streamFactory(item));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonMultiStreamLoader{TRecord}"/> class
    /// with a named-destination factory and source-generated type metadata for AOT-friendly serialization.
    /// </summary>
    /// <param name="destinationFactory">
    /// A factory function that receives the item to be written and returns a <see cref="JsonNamedDestination"/>
    /// containing the stream and an optional name. The loader will dispose the stream after writing.
    /// </param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="destinationFactory"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonMultiStreamLoader
    (
        Func<TRecord, JsonNamedDestination> destinationFactory,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonMultiStreamLoader<TRecord>>? logger = null
    )
    {
        _destinationFactory = destinationFactory ?? throw new ArgumentNullException(nameof(destinationFactory));
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
        if (streamFactory is null)
        {
            throw new ArgumentNullException(nameof(streamFactory));
        }

        _destinationFactory = item => new JsonNamedDestination(streamFactory(item));
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

            var destination = _destinationFactory(item);
            _currentDestinationName = destination?.Name;
            if (destination?.Stream is null)
            {
                JsonLogMessages.StreamFactoryReturnedNull(_logger, streamIndex, null);
                throw new InvalidOperationException($"Destination factory returned null or a null stream for item at index {streamIndex}.");
            }
            await WriteItemToStreamAsync(destination.Stream, item, streamIndex, token).ConfigureAwait(false);
            streamIndex++;
        }

        JsonLogMessages.MultiStreamLoadingCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, streamIndex, null);
    }



    private async Task WriteItemToStreamAsync(Stream stream, TRecord item, int streamIndex, CancellationToken token)
    {
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
            JsonLogMessages.LoadedItemToStream(_logger, CurrentItemCount, streamIndex, null);
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



    private Task SerializeToStreamAsync(Stream stream, TRecord item, CancellationToken token) =>
        _typeInfo is not null
            ? JsonSerializer.SerializeAsync(stream, item, _typeInfo, token)
            : JsonSerializer.SerializeAsync(stream, item, _options, token);



    /// <inheritdoc />
    protected override JsonReport CreateProgressReport() =>
        new
        (
            CurrentItemCount,
            CurrentSkippedItemCount,
            _currentDestinationName
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
