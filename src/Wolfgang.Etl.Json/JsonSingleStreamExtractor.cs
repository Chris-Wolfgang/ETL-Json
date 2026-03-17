using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.IO;
using Microsoft.Extensions.Logging;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Extracts items of type <typeparamref name="TRecord"/> from a single JSON array stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to extract. Must be <c>notnull</c>.</typeparam>
/// <remarks>
/// Reads a JSON array (e.g. <c>[{"name":"Alice"},{"name":"Bob"}]</c>) from a <see cref="Stream"/>
/// and yields each deserialized object as an item in the async enumerable sequence.
/// Uses <see cref="JsonSerializer.DeserializeAsyncEnumerable{TValue}(Stream, JsonSerializerOptions, CancellationToken)"/>
/// for true streaming deserialization.
/// </remarks>
/// <example>
/// <code>
/// using var stream = File.OpenRead("data.json");
/// var extractor = new JsonSingleStreamExtractor&lt;Person&gt;(stream, logger);
/// await foreach (var person in extractor.ExtractAsync(cancellationToken))
/// {
///     Console.WriteLine(person.Name);
/// }
/// </code>
/// </example>
public class JsonSingleStreamExtractor<TRecord> : ExtractorBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private bool _progressTimerWired;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamExtractor{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream containing a JSON array to read from.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonSingleStreamExtractor
    (
        Stream stream,
        ILogger<JsonSingleStreamExtractor<TRecord>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamExtractor{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream containing a JSON array to read from.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/>, <paramref name="options"/>, or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonSingleStreamExtractor
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger<JsonSingleStreamExtractor<TRecord>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamExtractor{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing a JSON array to read from.</param>
    /// <param name="options">The JSON serializer options, or <c>null</c> for defaults.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonSingleStreamExtractor
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
    protected override async IAsyncEnumerable<TRecord> ExtractWorkerAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        _logger.LogDebug("Starting JSON single-stream extraction of {RecordType}.", typeof(TRecord).Name);

        var skipBudget = SkipItemCount;
        var itemsYielded = 0;

        await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<TRecord>
        (
            _stream,
            _options ?? new JsonSerializerOptions(),
            token
        ))
        {
            token.ThrowIfCancellationRequested();

            if (item is null)
            {
                _logger.LogDebug("Skipping null item encountered in JSON array.");
                continue;
            }

            if (skipBudget > 0)
            {
                skipBudget--;
                IncrementCurrentSkippedItemCount();
                _logger.LogDebug("Skipped item {SkippedCount} of {SkipTotal}.", CurrentSkippedItemCount, SkipItemCount);
                continue;
            }

            if (itemsYielded >= MaximumItemCount)
            {
                _logger.LogDebug("Reached MaximumItemCount of {MaximumItemCount}. Stopping.", MaximumItemCount);
                break;
            }

            IncrementCurrentItemCount();
            itemsYielded++;
            _logger.LogDebug("Extracted item {CurrentItemCount}.", CurrentItemCount);

            yield return item;
        }

        _logger.LogInformation
        (
            "JSON single-stream extraction completed. Extracted: {ItemCount}, skipped: {SkippedCount}.",
            CurrentItemCount,
            CurrentSkippedItemCount
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
