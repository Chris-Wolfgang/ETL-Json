using System;
using Microsoft.Extensions.Logging;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Cached <see cref="LoggerMessage"/> delegates for high-performance structured logging
/// across all JSON extractors and loaders.
/// </summary>
internal static class JsonLogMessages
{
    // ── Shared: Debug ────────────────────────────────────────────────

    internal static readonly Action<ILogger, string, Exception?> StartingOperation =
        LoggerMessage.Define<string>(LogLevel.Debug, new EventId(1, nameof(StartingOperation)), "Starting {Operation}.");

    internal static readonly Action<ILogger, int, int, Exception?> SkippedItem =
        LoggerMessage.Define<int, int>(LogLevel.Debug, new EventId(2, nameof(SkippedItem)), "Skipped item {SkippedCount} of {SkipTotal}.");

    internal static readonly Action<ILogger, int, Exception?> ReachedMaximumItemCount =
        LoggerMessage.Define<int>(LogLevel.Debug, new EventId(3, nameof(ReachedMaximumItemCount)), "Reached MaximumItemCount of {MaximumItemCount}. Stopping.");



    // ── SingleStream Extractor ───────────────────────────────────────

    internal static readonly Action<ILogger, Exception?> SkippingNullArrayItem =
        LoggerMessage.Define(LogLevel.Debug, new EventId(100, nameof(SkippingNullArrayItem)), "Skipping null item encountered in JSON array.");

    internal static readonly Action<ILogger, int, Exception?> ExtractedItem =
        LoggerMessage.Define<int>(LogLevel.Debug, new EventId(101, nameof(ExtractedItem)), "Extracted item {CurrentItemCount}.");

    internal static readonly Action<ILogger, int, int, Exception?> SingleStreamExtractionCompleted =
        LoggerMessage.Define<int, int>(LogLevel.Information, new EventId(102, nameof(SingleStreamExtractionCompleted)), "JSON single-stream extraction completed. Extracted: {ItemCount}, skipped: {SkippedCount}.");



    // ── SingleStream Loader ──────────────────────────────────────────

    internal static readonly Action<ILogger, int, Exception?> LoadedItem =
        LoggerMessage.Define<int>(LogLevel.Debug, new EventId(110, nameof(LoadedItem)), "Loaded item {CurrentItemCount}.");

    internal static readonly Action<ILogger, int, int, Exception?> SingleStreamLoadingCompleted =
        LoggerMessage.Define<int, int>(LogLevel.Information, new EventId(111, nameof(SingleStreamLoadingCompleted)), "JSON single-stream loading completed. Loaded: {ItemCount}, skipped: {SkippedCount}.");



    // ── MultiStream Extractor ────────────────────────────────────────

    internal static readonly Action<ILogger, int, Exception?> ReadingStream =
        LoggerMessage.Define<int>(LogLevel.Debug, new EventId(200, nameof(ReadingStream)), "Reading stream {StreamIndex}.");

    internal static readonly Action<ILogger, int, Exception?> StreamDeserializedToNull =
        LoggerMessage.Define<int>(LogLevel.Warning, new EventId(201, nameof(StreamDeserializedToNull)), "Stream {StreamIndex} deserialized to null.");

    internal static readonly Action<ILogger, int, int, Exception?> ExtractedItemFromStream =
        LoggerMessage.Define<int, int>(LogLevel.Debug, new EventId(202, nameof(ExtractedItemFromStream)), "Extracted item {CurrentItemCount} from stream {StreamIndex}.");

    internal static readonly Action<ILogger, int, int, int, Exception?> MultiStreamExtractionCompleted =
        LoggerMessage.Define<int, int, int>(LogLevel.Information, new EventId(203, nameof(MultiStreamExtractionCompleted)), "Multi-stream extraction completed. Extracted: {ItemCount}, skipped: {SkippedCount}, streams: {StreamCount}.");



    // ── MultiStream Loader ───────────────────────────────────────────

    internal static readonly Action<ILogger, int, Exception?> StreamFactoryReturnedNull =
        LoggerMessage.Define<int>(LogLevel.Error, new EventId(210, nameof(StreamFactoryReturnedNull)), "Stream factory returned null for item at index {StreamIndex}.");

    internal static readonly Action<ILogger, int, int, Exception?> LoadedItemToStream =
        LoggerMessage.Define<int, int>(LogLevel.Debug, new EventId(211, nameof(LoadedItemToStream)), "Loaded item {CurrentItemCount} to stream {StreamIndex}.");

    internal static readonly Action<ILogger, int, int, int, Exception?> MultiStreamLoadingCompleted =
        LoggerMessage.Define<int, int, int>(LogLevel.Information, new EventId(212, nameof(MultiStreamLoadingCompleted)), "Multi-stream loading completed. Loaded: {ItemCount}, skipped: {SkippedCount}, streams: {StreamCount}.");



    // ── JSONL Extractor ──────────────────────────────────────────────

    internal static readonly Action<ILogger, long, Exception?> SkippingBlankLine =
        LoggerMessage.Define<long>(LogLevel.Debug, new EventId(300, nameof(SkippingBlankLine)), "Skipping blank line at {LineNumber}.");

    internal static readonly Action<ILogger, long, Exception?> LineDeserializedToNull =
        LoggerMessage.Define<long>(LogLevel.Warning, new EventId(301, nameof(LineDeserializedToNull)), "Line {LineNumber} deserialized to null.");

    internal static readonly Action<ILogger, int, int, long, Exception?> SkippedItemAtLine =
        LoggerMessage.Define<int, int, long>(LogLevel.Debug, new EventId(302, nameof(SkippedItemAtLine)), "Skipped item {SkippedCount} of {SkipTotal} at line {LineNumber}.");

    internal static readonly Action<ILogger, int, long, Exception?> ExtractedItemFromLine =
        LoggerMessage.Define<int, long>(LogLevel.Debug, new EventId(303, nameof(ExtractedItemFromLine)), "Extracted item {CurrentItemCount} from line {LineNumber}.");

    internal static readonly Action<ILogger, int, int, long, Exception?> JsonlExtractionCompleted =
        LoggerMessage.Define<int, int, long>(LogLevel.Information, new EventId(304, nameof(JsonlExtractionCompleted)), "JSONL extraction completed. Extracted: {ItemCount}, skipped: {SkippedCount}, lines: {LineCount}.");



    // ── JSONL Loader ─────────────────────────────────────────────────

    internal static readonly Action<ILogger, int, long, Exception?> LoadedItemAtLine =
        LoggerMessage.Define<int, long>(LogLevel.Debug, new EventId(310, nameof(LoadedItemAtLine)), "Loaded item {CurrentItemCount} at line {LineNumber}.");

    internal static readonly Action<ILogger, int, int, long, Exception?> JsonlLoadingCompleted =
        LoggerMessage.Define<int, int, long>(LogLevel.Information, new EventId(311, nameof(JsonlLoadingCompleted)), "JSONL loading completed. Loaded: {ItemCount}, skipped: {SkippedCount}, lines: {LineCount}.");
}
