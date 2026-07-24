using System;

namespace Wolfgang.Etl.Json;

/// <summary>Represents a record that could not be deserialized during extraction.</summary>
public sealed class JsonDeserializationError
{
    internal JsonDeserializationError
    (
        long itemNumber,
        string? rawContent,
        Exception exception
    )
    {
        ItemNumber = itemNumber;
        RawContent = rawContent;
        Exception = exception;
    }



    /// <summary>
    /// The 1-based ordinal of the failed item within the extraction — the line number for JSONL,
    /// or the item's position for a single- or multi-stream source.
    /// </summary>
    public long ItemNumber { get; }



    /// <summary>The raw string content that failed to deserialize, if available.</summary>
    public string? RawContent { get; }



    /// <summary>The exception thrown during deserialization.</summary>
    public Exception Exception { get; }
}
