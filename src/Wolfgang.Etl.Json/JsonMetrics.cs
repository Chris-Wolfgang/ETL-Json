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
}
