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
/// var extractor = new JsonLineExtractor&lt;Person&gt;(stream);
/// await foreach (var person in extractor.ExtractAsync(cancellationToken))
/// {
///     Console.WriteLine(person.Name);
/// }
/// </code>
/// </example>
public sealed class JsonLineExtractor<TRecord> : ExtractorBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private static readonly string OperationName = $"JSONL extraction of {typeof(TRecord).Name}";
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private bool _progressTimerWired;
    private long _currentLineNumber;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    public JsonLineExtractor
    (
        Stream stream
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="options"/> is <c>null</c>.
    /// </exception>
    public JsonLineExtractor
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonLineExtractor
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
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with source-generated type metadata for AOT-friendly, reflection-free deserialization.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonLineExtractor
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonLineExtractor<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineExtractor{TRecord}"/> class
    /// with source-generated type metadata and an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing JSONL data to read from.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonLineExtractor
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
    protected override async IAsyncEnumerable<TRecord> ExtractWorkerAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        JsonLogMessages.StartingOperation(_logger, OperationName, null);

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

            var item = _typeInfo is not null
                ? JsonSerializer.Deserialize(line, _typeInfo)
                : JsonSerializer.Deserialize<TRecord>(line, _options);
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
