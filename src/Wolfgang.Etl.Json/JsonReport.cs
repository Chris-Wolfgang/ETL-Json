using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Progress report for JSON extraction and loading operations.
/// </summary>
/// <remarks>
/// Extends <see cref="Report"/> with JSON-specific progress information,
/// including the count of skipped items.
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
        : base(currentItemCount)
    {
        CurrentSkippedItemCount = currentSkippedItemCount;
    }



    /// <summary>
    /// Gets the number of items that have been skipped during processing.
    /// </summary>
    public int CurrentSkippedItemCount { get; }
}
