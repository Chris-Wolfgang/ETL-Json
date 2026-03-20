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
/// <remarks>
/// Reads a stream, line by line, deserializing each non-empty line as a single JSON object.
/// Blank lines are skipped with a warning logged.
/// Compatible with both JSONL and NDJSON formats.
/// </remarks>
/// <example>
/// <code>
/// using var stream = File.OpenRead("data.jsonl");
/// var extractor = new JsonLineExtractor&lt;Person&gt;(stream, logger);
/// await foreach (var person in extractor.ExtractAsync(cancellationToken))
/// {
///     Console.WriteLine(person.Name);
/// }
/// </code>
/// </example>
public class JsonLineExtractor<TRecord> : ExtractorBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private bool _progressTimerWired;
    private long _currentLineNumber;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonLineExtractor
    (
        Stream stream,
        ILogger<JsonLineExtractor<TRecord>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
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
        ILogger<JsonLineExtractor<TRecord>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The JSON serializer options, or <c>null</c> for defaults.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonLineExtractor
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger logger,
        IProgressTimer timer
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _progressTimer = timer ?? throw new ArgumentNullException(nameof(timer));
    }



    /// <inheritdoc />
    protected override async IAsyncEnumerable<TRecord> ExtractWorkerAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        JsonLogMessages.StartingOperation(_logger, $"JSONL extraction of {typeof(TRecord).Name}", null);

        var skipBudget = SkipItemCount;

#if NETSTANDARD2_0 || NET462 || NET481
        using var reader = new StreamReader(_stream, System.Text.Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true);
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
            Interlocked.Increment(ref _currentLineNumber);
            var lineNum = Interlocked.Read(ref _currentLineNumber);

            if (string.IsNullOrWhiteSpace(line))
            {
                JsonLogMessages.SkippingBlankLine(_logger, lineNum, null);
                continue;
            }

            var item = JsonSerializer.Deserialize<TRecord>(line, _options);
            if (item is null)
            {
                JsonLogMessages.LineDeserializedToNull(_logger, lineNum, null);
                continue;
            }

            if (skipBudget > 0)
            {
                skipBudget--;
                IncrementCurrentSkippedItemCount();
                JsonLogMessages.SkippedItemAtLine(_logger, CurrentSkippedItemCount, SkipItemCount, lineNum, null);
                continue;
            }

            if (CurrentItemCount >= MaximumItemCount)
            {
                JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                break;
            }

            IncrementCurrentItemCount();
            JsonLogMessages.ExtractedItemFromLine(_logger, CurrentItemCount, lineNum, null);

            yield return item;
        }

        JsonLogMessages.JsonlExtractionCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, Interlocked.Read(ref _currentLineNumber), null);
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
