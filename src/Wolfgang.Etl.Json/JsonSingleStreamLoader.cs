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
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private bool _progressTimerWired;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
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
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="options">The JSON serializer options to use for serialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="options"/> is <c>null</c>.
    /// </exception>
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger<JsonSingleStreamLoader<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="options">The JSON serializer options to use for serialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonSingleStreamLoader
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
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonSingleStreamLoader
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



    /// <inheritdoc />
    protected override async Task LoadWorkerAsync
    (
        IAsyncEnumerable<TRecord> items,
        CancellationToken token
    )
    {
        JsonLogMessages.StartingOperation(_logger, OperationName, null);

        // CA2007/MA0004: await using declarations do not support ConfigureAwait in C#
#pragma warning disable CA2007, MA0004
        await using var writer = new Utf8JsonWriter(_stream);
#pragma warning restore CA2007, MA0004

        writer.WriteStartArray();

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

            if (_typeInfo is not null)
            {
                JsonSerializer.Serialize(writer, item, _typeInfo);
            }
            else
            {
                JsonSerializer.Serialize(writer, item, _options);
            }
            IncrementCurrentItemCount();

            JsonLogMessages.LoadedItem(_logger, CurrentItemCount, null);
        }

        writer.WriteEndArray();
        await writer.FlushAsync(token).ConfigureAwait(false);

        JsonLogMessages.SingleStreamLoadingCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, null);
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
