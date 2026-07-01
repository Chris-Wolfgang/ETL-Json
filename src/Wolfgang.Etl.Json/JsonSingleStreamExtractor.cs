using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
/// var extractor = new JsonSingleStreamExtractor&lt;Person&gt;(stream);
/// await foreach (var person in extractor.ExtractAsync(cancellationToken))
/// {
///     Console.WriteLine(person.Name);
/// }
/// </code>
/// </example>
public sealed class JsonSingleStreamExtractor<TRecord> : ExtractorBase<TRecord, JsonReport>
    where TRecord : notnull
{
    private static readonly string OperationName = $"JSON single-stream extraction of {typeof(TRecord).Name}";
    private static readonly JsonSerializerOptions DefaultOptions = new();

    private readonly Stream _stream;
    private readonly JsonSerializerOptions? _options;
    private readonly JsonTypeInfo<TRecord>? _typeInfo;
    private readonly ILogger _logger;
    private readonly IProgressTimer? _progressTimer;
    private readonly List<JsonDeserializationError> _errors = new();
    private int _progressTimerWired;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamExtractor{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The stream containing a JSON array to read from.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonSingleStreamExtractor
    (
        Stream stream
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _logger = NullLogger.Instance;
        _options = null;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamExtractor{TRecord}"/> class
    /// with diagnostic logging.
    /// </summary>
    /// <param name="stream">The stream containing a JSON array to read from.</param>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="logger"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
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
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="options"/> is <c>null</c>.
    /// </exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    public JsonSingleStreamExtractor
    (
        Stream stream,
        JsonSerializerOptions options,
        ILogger<JsonSingleStreamExtractor<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamExtractor{TRecord}"/> class
    /// with an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing a JSON array to read from.</param>
    /// <param name="options">The JSON serializer options to use for deserialization.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.")]
#endif
    internal JsonSingleStreamExtractor
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
    /// Initializes a new instance of the <see cref="JsonSingleStreamExtractor{TRecord}"/> class
    /// with source-generated type metadata for AOT-friendly, reflection-free deserialization.
    /// </summary>
    /// <param name="stream">The stream containing a JSON array to read from.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="stream"/> or <paramref name="typeInfo"/> is <c>null</c>.
    /// </exception>
    public JsonSingleStreamExtractor
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo,
        ILogger<JsonSingleStreamExtractor<TRecord>>? logger = null
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        _logger = logger ?? (ILogger)NullLogger.Instance;
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSingleStreamExtractor{TRecord}"/> class
    /// with source-generated type metadata and an injected progress timer for testing.
    /// </summary>
    /// <param name="stream">The stream containing a JSON array to read from.</param>
    /// <param name="typeInfo">The source-generated type metadata for <typeparamref name="TRecord"/>.</param>
    /// <param name="logger">An optional logger instance for diagnostic output.</param>
    /// <param name="timer">The progress timer to inject.</param>
    internal JsonSingleStreamExtractor
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



    /// <summary>
    /// Gets or sets how deserialization errors are handled during extraction.
    /// Default is <see cref="ErrorHandling.Throw"/>.
    /// </summary>
    /// <remarks>
    /// Because JSON array deserialization is streaming, a <see cref="System.Text.Json.JsonException"/>
    /// leaves the underlying reader in an unrecoverable state. When
    /// <see cref="ErrorHandling.CaptureAndContinue"/> is set, the error is captured and enumeration
    /// stops at the point of failure; subsequent records in the array are not returned.
    /// </remarks>
    public ErrorHandling ErrorHandling { get; init; } = ErrorHandling.Throw;



    /// <summary>
    /// Gets the collection of deserialization errors captured during the most recent extraction.
    /// Only populated when <see cref="ErrorHandling"/> is <see cref="ErrorHandling.CaptureAndContinue"/>.
    /// </summary>
    public IReadOnlyList<JsonDeserializationError> Errors => _errors.AsReadOnly();



    /// <inheritdoc />
    protected override async IAsyncEnumerable<TRecord> ExtractWorkerAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        _errors.Clear();
        JsonLogMessages.StartingOperation(_logger, OperationName, null);

        var skipBudget = SkipItemCount;
        var enumerable = _typeInfo is not null
            ? JsonSerializer.DeserializeAsyncEnumerable(_stream, _typeInfo, token)
            : JsonSerializer.DeserializeAsyncEnumerable<TRecord>(_stream, _options ?? DefaultOptions, token);

        var enumerator = enumerable.GetAsyncEnumerator(token);
        try
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                TRecord? item = default;
                bool hasNext = false;
                JsonException? caughtEx = null;
                try
                {
                    hasNext = await enumerator.MoveNextAsync().ConfigureAwait(false);
                    item = hasNext ? enumerator.Current : default;
                }
#pragma warning disable CA1031 // catch JsonException to implement error-handling policy
                catch (JsonException ex) { caughtEx = ex; }
#pragma warning restore CA1031
                if (caughtEx is not null) { HandleDeserializationError(caughtEx); break; }
                if (!hasNext) { break; }
                if (item is null) { JsonLogMessages.SkippingNullArrayItem(_logger, null); continue; }
                if (skipBudget > 0)
                {
                    skipBudget--;
                    IncrementCurrentSkippedItemCount();
                    JsonLogMessages.SkippedItem(_logger, CurrentSkippedItemCount, SkipItemCount, null);
                    continue;
                }
                if (CurrentItemCount >= MaximumItemCount)
                {
                    JsonLogMessages.ReachedMaximumItemCount(_logger, MaximumItemCount, null);
                    break;
                }
                IncrementCurrentItemCount();
                JsonLogMessages.ExtractedItem(_logger, CurrentItemCount, null);
                yield return item;
            }
        }
        finally
        {
            await enumerator.DisposeAsync().ConfigureAwait(false);
        }

        JsonLogMessages.SingleStreamExtractionCompleted(_logger, CurrentItemCount, CurrentSkippedItemCount, null);
    }



    private void HandleDeserializationError(JsonException ex)
    {
        var error = new JsonDeserializationError(
            itemIndex: _errors.Count + CurrentItemCount + CurrentSkippedItemCount,
            lineNumber: null,
            rawContent: null,
            exception: ex);
        if (ErrorHandling == ErrorHandling.Throw)
        {
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
        }

        if (ErrorHandling == ErrorHandling.CaptureAndContinue)
        {
            _errors.Add(error);
        }

        JsonLogMessages.DeserializationErrorAtIndex(_logger, error.ItemIndex, ex);
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
            if (Interlocked.CompareExchange(ref _progressTimerWired, 1, 0) == 0)
            {
                _progressTimer.Elapsed += () => progress.Report(CreateProgressReport());
            }

            return _progressTimer;
        }

        return base.CreateProgressTimer(progress);
    }
}
