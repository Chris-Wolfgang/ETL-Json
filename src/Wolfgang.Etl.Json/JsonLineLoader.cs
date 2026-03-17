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
/// Loads items of type <typeparamref name="TRecord"/> into a JSONL (JSON Lines / NDJSON) stream.
/// </summary>
/// <typeparam name="TRecord">The type of items to load. Must be <c>notnull</c>.</typeparam>
/// <remarks>
/// Writes each item as a single line of JSON to the output stream,
/// with each line separated by a newline character.
/// Compatible with both JSONL and NDJSON consumers.
/// </remarks>
/// <example>
/// <code>
/// using var stream = File.Create("output.jsonl");
/// var loader = new JsonLineLoader&lt;Person&gt;(stream, logger);
/// await loader.LoadAsync(items, cancellationToken);
/// </code>
/// </example>
public class JsonLineLoader<TRecord> : LoaderBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private bool _progressTimerWired;
    private long _currentLineNumber;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonLineLoader
    (
        Stream stream,
        ILogger<JsonLineLoader<TRecord>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class
    /// with custom serialization options.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <param name="options">The JSON serializer options to use for serialization.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/>, <paramref name="options"/>, or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
    public JsonLineLoader
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger<JsonLineLoader<TRecord>> logger
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonLineLoader{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream to write JSONL data to.</param>
    /// <param name="options">The JSON serializer options, or <c>null</c> for defaults.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonLineLoader
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
        JsonLogMessages.StartingOperation(_logger, $"JSONL loading of {typeof(TRecord).Name}", null);

#if NETSTANDARD2_0 || NET462 || NET481
        using var writer = new StreamWriter(_stream, System.Text.Encoding.UTF8, 1024, true);
#else
        using var writer = new StreamWriter(_stream, leaveOpen: true);
#endif

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

            var json = JsonSerializer.Serialize(item, _options);
            System.Threading.Interlocked.Increment(ref _currentLineNumber);

#if NETSTANDARD2_0 || NET462 || NET481
            await writer.WriteLineAsync(json).ConfigureAwait(false);
#else
            await writer.WriteLineAsync(json.AsMemory(), token).ConfigureAwait(false);
#endif

            IncrementCurrentItemCount();
            JsonLogMessages.LoadedItemAtLine(_logger, CurrentItemCount, System.Threading.Interlocked.Read(ref _currentLineNumber), null);
        }

        await writer.FlushAsync().ConfigureAwait(false);

        JsonLogMessages.JsonlLoadingCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, System.Threading.Interlocked.Read(ref _currentLineNumber), null);
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
