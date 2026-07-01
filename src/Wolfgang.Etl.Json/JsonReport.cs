using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Progress report for JSON extraction and loading operations.
/// </summary>
/// <remarks>
/// Extends <see cref="Report"/> with JSON-specific progress information,
/// including the count of skipped items and the name of the stream currently being processed.
/// </remarks>
public record JsonReport : Report
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonReport"/> class.
    /// </summary>
    /// <param name="currentItemCount">The number of items processed so far.</param>
    /// <param name="currentSkippedItemCount">The number of items skipped so far.</param>
    public JsonReport
    (
        int currentItemCount,
        int currentSkippedItemCount
    )
        : this(currentItemCount, currentSkippedItemCount, currentSourceName: null)
    {
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonReport"/> class
    /// with the name of the stream currently being processed.
    /// </summary>
    /// <param name="currentItemCount">The number of items processed so far.</param>
    /// <param name="currentSkippedItemCount">The number of items skipped so far.</param>
    /// <param name="currentSourceName">
    /// The name of the stream currently being processed, or <c>null</c> if no name was supplied.
    /// </param>
    public JsonReport
    (
        int currentItemCount,
        int currentSkippedItemCount,
        string? currentSourceName
    )
        : base(currentItemCount)
    {
        CurrentSkippedItemCount = currentSkippedItemCount;
        CurrentSourceName = currentSourceName;
    }



    /// <summary>
    /// Gets the number of items that have been skipped during processing.
    /// </summary>
    public int CurrentSkippedItemCount { get; }



    /// <summary>
    /// Gets the name of the stream currently being processed, or <c>null</c>
    /// when the caller did not supply a name or when the operation is not multi-stream.
    /// </summary>
    public string? CurrentSourceName { get; }
}
