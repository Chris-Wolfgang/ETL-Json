using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Wolfgang.Etl.Json;

internal static class JsonMetrics
{
    internal static readonly Meter Meter = new("Wolfgang.Etl.Json");


    internal static readonly Counter<long> ItemsExtracted =
        Meter.CreateCounter<long>(
            "wolfgang.etl.json.items.extracted",
            unit: null,
            description: "Total items successfully extracted.");


    internal static readonly Counter<long> ItemsLoaded =
        Meter.CreateCounter<long>(
            "wolfgang.etl.json.items.loaded",
            unit: null,
            description: "Total items successfully loaded.");


    internal static readonly Counter<long> ItemsSkipped =
        Meter.CreateCounter<long>(
            "wolfgang.etl.json.items.skipped",
            unit: null,
            description: "Total items skipped via SkipItemCount.");


    internal static readonly Histogram<double> OperationDuration =
        Meter.CreateHistogram<double>(
            "wolfgang.etl.json.operation.duration",
            unit: "ms",
            description: "Duration of extract/load operations in milliseconds.");



    /// <summary>
    /// Records one extracted item. The <see cref="Instrument.Enabled"/> guard skips the
    /// diagnostics machinery entirely when no listener is subscribed, keeping the hot path free.
    /// </summary>
    internal static void AddExtracted
    (
        KeyValuePair<string, object?> operation,
        KeyValuePair<string, object?> component,
        KeyValuePair<string, object?> recordType
    )
    {
        if (ItemsExtracted.Enabled)
        {
            ItemsExtracted.Add(1, operation, component, recordType);
        }
    }



    /// <summary>
    /// Records one loaded item. See <see cref="AddExtracted"/> for the enabled-guard rationale.
    /// </summary>
    internal static void AddLoaded
    (
        KeyValuePair<string, object?> operation,
        KeyValuePair<string, object?> component,
        KeyValuePair<string, object?> recordType
    )
    {
        if (ItemsLoaded.Enabled)
        {
            ItemsLoaded.Add(1, operation, component, recordType);
        }
    }



    /// <summary>
    /// Records one skipped item. See <see cref="AddExtracted"/> for the enabled-guard rationale.
    /// </summary>
    internal static void AddSkipped
    (
        KeyValuePair<string, object?> operation,
        KeyValuePair<string, object?> component,
        KeyValuePair<string, object?> recordType
    )
    {
        if (ItemsSkipped.Enabled)
        {
            ItemsSkipped.Add(1, operation, component, recordType);
        }
    }



    /// <summary>
    /// Records an operation's duration in milliseconds. See <see cref="AddExtracted"/> for the
    /// enabled-guard rationale.
    /// </summary>
    internal static void RecordDuration
    (
        double milliseconds,
        KeyValuePair<string, object?> operation,
        KeyValuePair<string, object?> component,
        KeyValuePair<string, object?> recordType
    )
    {
        if (OperationDuration.Enabled)
        {
            OperationDuration.Record(milliseconds, operation, component, recordType);
        }
    }
}
