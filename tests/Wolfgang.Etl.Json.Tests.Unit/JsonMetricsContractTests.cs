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

/// <summary>
/// Pins the names, units, descriptions and tags that a metrics consumer sees on the
/// <c>Wolfgang.Etl.Json</c> meter. Dashboards and alerts key on these strings, so a change to
/// any of them is a breaking change for consumers even though the types are internal.
/// </summary>
public sealed class JsonMetricsContractTests
{
    private const string MeterName = "Wolfgang.Etl.Json";

    private static readonly PersonRecord[] People =
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
    };



    // Records every instrument the meter publishes and the tags of every measurement. The
    // listener also sees measurements from tests running in parallel, so assertions check that
    // an expected tag set is present rather than that it is the only one.
    private sealed class MeterRecorder : IDisposable
    {
        private readonly MeterListener _listener = new();
        private readonly List<(string Name, string? Unit, string? Description)> _instruments = new();
        private readonly List<(string Instrument, Dictionary<string, object?> Tags)> _measurements = new();



        public MeterRecorder()
        {
            _listener.InstrumentPublished = (instrument, listener) =>
            {
                if (string.Equals(instrument.Meter.Name, MeterName, StringComparison.Ordinal))
                {
                    lock (_instruments)
                    {
                        _instruments.Add((instrument.Name, instrument.Unit, instrument.Description));
                    }

                    listener.EnableMeasurementEvents(instrument);
                }
            };

            _listener.SetMeasurementEventCallback<long>((instrument, _, tags, _) => Record(instrument, tags));
            _listener.SetMeasurementEventCallback<double>((instrument, _, tags, _) => Record(instrument, tags));
            _listener.Start();
        }



        public (string Name, string? Unit, string? Description) Instrument(string name)
        {
            lock (_instruments)
            {
                return _instruments.Single(i => string.Equals(i.Name, name, StringComparison.Ordinal));
            }
        }



        public bool Measured(string instrument, string operation, string component)
        {
            lock (_measurements)
            {
                return _measurements.Any
                (
                    m => string.Equals(m.Instrument, instrument, StringComparison.Ordinal)
                        && m.Tags.TryGetValue("etl.operation", out var op) && Equals(op, operation)
                        && m.Tags.TryGetValue("etl.component", out var comp) && Equals(comp, component)
                );
            }
        }



        public void Dispose() => _listener.Dispose();



        private void Record(Instrument instrument, ReadOnlySpan<KeyValuePair<string, object?>> tags)
        {
            var copy = new Dictionary<string, object?>(StringComparer.Ordinal);
            foreach (var tag in tags)
            {
                copy[tag.Key] = tag.Value;
            }

            lock (_measurements)
            {
                _measurements.Add((instrument.Name, copy));
            }
        }
    }



    [Theory]
    [InlineData("wolfgang.etl.json.items.extracted", null, "Total items successfully extracted.")]
    [InlineData("wolfgang.etl.json.items.loaded", null, "Total items successfully loaded.")]
    [InlineData("wolfgang.etl.json.items.skipped", null, "Total items skipped via SkipItemCount.")]
    [InlineData("wolfgang.etl.json.operation.duration", "ms", "Duration of extract/load operations in milliseconds.")]
    public void Meter_when_a_listener_starts_publishes_each_instrument_with_its_unit_and_description
    (
        string name,
        string? unit,
        string description
    )
    {
        using var recorder = new MeterRecorder();

        var instrument = recorder.Instrument(name);

        Assert.Equal(unit, instrument.Unit);
        Assert.Equal(description, instrument.Description);
    }



    [Fact]
    public async Task JsonLineExtractor_when_a_listener_is_subscribed_tags_measurements_extract_and_JsonLine()
    {
        using var recorder = new MeterRecorder();
        var jsonl = string.Join("\n", People.Select(p => JsonSerializer.Serialize(p)));
        var sut = new JsonLineExtractor<PersonRecord>(new MemoryStream(Encoding.UTF8.GetBytes(jsonl)));

        await foreach (var _ in sut.ExtractAsync())
        {
        }

        Assert.True(recorder.Measured("wolfgang.etl.json.items.extracted", "extract", "JsonLine"));
    }



    [Fact]
    public async Task JsonSingleStreamLoader_when_a_listener_is_subscribed_tags_measurements_load_and_JsonSingleStream()
    {
        using var recorder = new MeterRecorder();
        var sut = new JsonSingleStreamLoader<PersonRecord>(new MemoryStream());

        await sut.LoadAsync(Source());

        Assert.True(recorder.Measured("wolfgang.etl.json.items.loaded", "load", "JsonSingleStream"));
    }



    [Fact]
    public async Task JsonMultiStreamExtractor_when_a_listener_is_subscribed_tags_measurements_extract_and_JsonMultiStream()
    {
        using var recorder = new MeterRecorder();
        var streams = People
            .Select(Stream (p) => new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(p))))
            .ToList();
        var sut = new JsonMultiStreamExtractor<PersonRecord>(streams);

        await foreach (var _ in sut.ExtractAsync())
        {
        }

        Assert.True(recorder.Measured("wolfgang.etl.json.items.extracted", "extract", "JsonMultiStream"));
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
