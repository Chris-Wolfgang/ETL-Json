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
/// Loads items of type <typeparamref name="TRecord"/> into a single JSON array stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to load. Must be <c>notnull</c>.</typeparam>
/// <typeparam name="TProgress">The progress report type.</typeparam>
/// <remarks>
/// Writes a JSON array (e.g. <c>[{"name":"Alice"},{"name":"Bob"}]</c>) to a <see cref="Stream"/>
/// by serializing each item from the input async enumerable sequence.
/// </remarks>
/// <example>
/// <code>
/// using var stream = File.Create("output.json");
/// var loader = new JsonSingleStreamLoader&lt;Person, JsonReport&gt;(stream, logger);
/// await loader.LoadAsync(items, cancellationToken);
/// </code>
/// </example>
public class JsonSingleStreamLoader<TRecord, TProgress> : LoaderBase<TRecord, TProgress>
    where TRecord : notnull
    where TProgress : notnull
{
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private bool _progressTimerWired;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord, TProgress}"/> class.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonSingleStreamLoader
    (
        Stream stream,
        ILogger<JsonSingleStreamLoader<TRecord, TProgress>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord, TProgress}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="options">The JSON serializer options to use for serialization.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/>, <paramref name="options"/>, or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonSingleStreamLoader
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger<JsonSingleStreamLoader<TRecord, TProgress>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamLoader{TRecord, TProgress}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="options">The JSON serializer options, or <c>null</c> for defaults.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonSingleStreamLoader
    (
        Stream stream,
        JsonSerializerOptions? options,
        ILogger logger,
        IProgressTimer timer
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
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
        _logger.LogDebug("Starting JSON single-stream loading of {RecordType}.", typeof(TRecord).Name);

#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
        await using var writer = new Utf8JsonWriter(_stream);
#pragma warning restore CA2007

        writer.WriteStartArray();

        await foreach (var item in items.WithCancellation(token).ConfigureAwait(false))
        {
            token.ThrowIfCancellationRequested();

            if (CurrentSkippedItemCount < SkipItemCount)
            {
                IncrementCurrentSkippedItemCount();
                _logger.LogDebug
                (
                    "Skipped item {SkippedCount} of {SkipTotal}.",
                    CurrentSkippedItemCount,
                    SkipItemCount
                );
                continue;
            }

            if (CurrentItemCount >= MaximumItemCount)
            {
                _logger.LogDebug
                (
                    "Reached MaximumItemCount of {MaximumItemCount}. Stopping loading.",
                    MaximumItemCount
                );
                break;
            }

            JsonSerializer.Serialize(writer, item, _options);
            IncrementCurrentItemCount();

            _logger.LogDebug("Loaded item {CurrentItemCount}.", CurrentItemCount);
        }

        writer.WriteEndArray();
        await writer.FlushAsync(token).ConfigureAwait(false);

        _logger.LogInformation
        (
            "JSON single-stream loading completed. Items loaded: {ItemCount}, items skipped: {SkippedCount}.",
            CurrentItemCount,
            CurrentSkippedItemCount
        );
    }



    /// <inheritdoc />
    protected override TProgress CreateProgressReport()
    {
        if (typeof(TProgress) == typeof(JsonReport) || typeof(TProgress) == typeof(Report))
        {
            return (TProgress)(object)new JsonReport
            (
                CurrentItemCount,
                CurrentSkippedItemCount
            );
        }

        throw new NotSupportedException
        (
            $"Override {nameof(CreateProgressReport)} to supply a {typeof(TProgress).Name} instance."
        );
    }



    /// <inheritdoc />
    protected override IProgressTimer CreateProgressTimer(IProgress<TProgress> progress)
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
