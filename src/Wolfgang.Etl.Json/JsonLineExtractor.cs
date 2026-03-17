using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.Logging;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Extracts items of type <typeparamref name="TRecord"/> from a JSONL (JSON Lines / NDJSON) stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to extract. Must be <c>notnull</c>.</typeparam>
/// <typeparam name="TProgress">The progress report type.</typeparam>
/// <remarks>
/// Reads a stream line by line, deserializing each non-empty line as a single JSON object.
/// Blank lines are skipped with a warning logged.
/// Compatible with both JSONL and NDJSON formats.
/// </remarks>
/// <example>
/// <code>
/// using var stream = File.OpenRead("data.jsonl");
/// var extractor = new JsonLineExtractor&lt;Person, JsonReport&gt;(stream, logger);
/// await foreach (var person in extractor.ExtractAsync(cancellationToken))
/// {
///     Console.WriteLine(person.Name);
/// }
/// </code>
/// </example>
public class JsonLineExtractor<TRecord, TProgress> : ExtractorBase<TRecord, TProgress>
    where TRecord : notnull
    where TProgress : notnull
{
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private bool _progressTimerWired;
    private long _currentLineNumber;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord, TProgress}"/> class.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonLineExtractor
    (
        Stream stream,
        ILogger<JsonLineExtractor<TRecord, TProgress>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord, TProgress}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/>, <paramref name="options"/>, or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonLineExtractor
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger<JsonLineExtractor<TRecord, TProgress>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord, TProgress}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The JSON serializer options, or <c>null</c> for defaults.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonLineExtractor
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
        _logger.LogDebug("Starting JSONL extraction of {RecordType}.", typeof(TRecord).Name);

        var skipBudget = SkipItemCount;
        var itemsYielded = 0;

#if NETSTANDARD2_0 || NET462 || NET481
        using var reader = new StreamReader(_stream, System.Text.Encoding.UTF8, true, 1024, true);
#else
        using var reader = new StreamReader(_stream, leaveOpen: true);
#endif

        string? line;
#if NETSTANDARD2_0 || NET462 || NET481
        while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) is not null)
#else
        while ((line = await reader.ReadLineAsync(token).ConfigureAwait(false)) is not null)
#endif
        {
            token.ThrowIfCancellationRequested();
            System.Threading.Interlocked.Increment(ref _currentLineNumber);
            var lineNum = System.Threading.Interlocked.Read(ref _currentLineNumber);

            if (string.IsNullOrWhiteSpace(line))
            {
                _logger.LogDebug("Skipping blank line at {LineNumber}.", lineNum);
                continue;
            }

            var item = JsonSerializer.Deserialize<TRecord>(line, _options);
            if (item is null)
            {
                _logger.LogWarning("Line {LineNumber} deserialized to null.", lineNum);
                continue;
            }

            if (skipBudget > 0)
            {
                skipBudget--;
                IncrementCurrentSkippedItemCount();
                _logger.LogDebug("Skipped item {SkippedCount} of {SkipTotal} at line {LineNumber}.", CurrentSkippedItemCount, SkipItemCount, lineNum);
                continue;
            }

            if (itemsYielded >= MaximumItemCount)
            {
                _logger.LogDebug("Reached MaximumItemCount of {MaximumItemCount}. Stopping.", MaximumItemCount);
                break;
            }

            IncrementCurrentItemCount();
            itemsYielded++;
            _logger.LogDebug("Extracted item {CurrentItemCount} from line {LineNumber}.", CurrentItemCount, lineNum);

            yield return item;
        }

        _logger.LogInformation
        (
            "JSONL extraction completed. Extracted: {ItemCount}, skipped: {SkippedCount}, lines: {LineCount}.",
            CurrentItemCount, CurrentSkippedItemCount, System.Threading.Interlocked.Read(ref _currentLineNumber)
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
