using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public sealed class JsonMetricsTests
{
    private static readonly PersonRecord[] People =
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        new() { FirstName = "Carol", LastName = "White", Age = 35 },
    };


    // Subscribes to the Wolfgang.Etl.Json meter and records which instruments fired. When a
    // listener is attached the extractors/loaders' Instrument.Enabled guards are true, so the
    // Counter.Add / Histogram.Record calls execute.
    private sealed class MetricsCollector : IDisposable
    {
        private readonly MeterListener _listener = new();
        private readonly List<string> _counters = new();
        private readonly List<string> _histograms = new();

        public MetricsCollector()
        {
            _listener.InstrumentPublished = (instrument, listener) =>
            {
                if (string.Equals(instrument.Meter.Name, "Wolfgang.Etl.Json", StringComparison.Ordinal))
                {
                    listener.EnableMeasurementEvents(instrument);
                }
            };

            _listener.SetMeasurementEventCallback<long>((instrument, _, _, _) =>
            {
                lock (_counters)
                {
                    _counters.Add(instrument.Name);
                }
            });

            _listener.SetMeasurementEventCallback<double>((instrument, _, _, _) =>
            {
                lock (_histograms)
                {
                    _histograms.Add(instrument.Name);
                }
            });

            _listener.Start();
        }

        // Reads must hold the same lock as the callbacks' writes: the listener fires on other
        // threads (including measurements from tests running in parallel), so an unlocked
        // enumeration can race with an Add and throw "Collection was modified".
        public bool CounterRecorded(string name)
        {
            lock (_counters)
            {
                return _counters.Contains(name);
            }
        }

        public bool HistogramRecorded(string name)
        {
            lock (_histograms)
            {
                return _histograms.Contains(name);
            }
        }

        public void Dispose() => _listener.Dispose();
    }


    private static MemoryStream JsonlStream() =>
        new(Encoding.UTF8.GetBytes(string.Join("\n", People.Select(p => JsonSerializer.Serialize(p)))));


    [Fact]
    public async Task Extractor_emits_extracted_skipped_and_duration_metrics_when_a_listener_is_subscribed()
    {
        using var collector = new MetricsCollector();
        var sut = new JsonLineExtractor<PersonRecord>(JsonlStream()) { SkipItemCount = 1 };

        await foreach (var _ in sut.ExtractAsync())
        {
        }

        Assert.True(collector.CounterRecorded("wolfgang.etl.json.items.extracted"));
        Assert.True(collector.CounterRecorded("wolfgang.etl.json.items.skipped"));
        Assert.True(collector.HistogramRecorded("wolfgang.etl.json.operation.duration"));
    }


    [Fact]
    public async Task Loader_emits_loaded_and_duration_metrics_when_a_listener_is_subscribed()
    {
        using var collector = new MetricsCollector();
        var loader = new JsonLineLoader<PersonRecord>(new MemoryStream());

        await loader.LoadAsync(Source());

        Assert.True(collector.CounterRecorded("wolfgang.etl.json.items.loaded"));
        Assert.True(collector.HistogramRecorded("wolfgang.etl.json.operation.duration"));
    }


    private static async IAsyncEnumerable<PersonRecord> Source()
    {
        foreach (var person in People)
        {
            await Task.Yield();
            yield return person;
        }
    }
}
