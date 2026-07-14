using System;

namespace Wolfgang.Etl.Json;

/// <summary>Represents a record that could not be deserialized during extraction.</summary>
public sealed class JsonDeserializationError
{
    internal JsonDeserializationError
    (
        long itemIndex,
        long? lineNumber,
        string? rawContent,
        Exception exception
    )
    {
        ItemIndex = itemIndex;
        LineNumber = lineNumber;
        RawContent = rawContent;
        Exception = exception;
    }



    /// <summary>The zero-based index of the item in the extraction sequence.</summary>
    public long ItemIndex { get; }



    /// <summary>The line number in the source, if applicable (JSONL only).</summary>
    public long? LineNumber { get; }



    /// <summary>The raw string content that failed to deserialize, if available.</summary>
    public string? RawContent { get; }



    /// <summary>The exception thrown during deserialization.</summary>
    public Exception Exception { get; }
}
