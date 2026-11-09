using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// The injected-timer branch of <c>CreateProgressTimer</c> on the stages whose own test classes do not
/// drive it: the timer's <c>Elapsed</c> handler must be wired, and wired once, so one tick produces
/// exactly one progress callback.
/// </summary>
public class ProgressTimerWiringTests
{
    private static readonly PersonRecord[] People =
    [
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
    ];



    [Fact]
    public async Task JsonLineLoader_LoadAsync_with_progress_wires_timer_callback_exactly_once()
    {
        var timer = new ManualProgressTimer();
        var probe = new CallbackProbe();
        var sut = new JsonLineLoader<PersonRecord>
        (
            new MemoryStream(),
            new JsonLineLoaderOptions(),
            timer,
            NullLogger<JsonLineLoader<PersonRecord>>.Instance
        );

        await sut.LoadAsync(ItemsThatTickMidStream(timer, probe), probe.Progress);

        Assert.Equal(1, probe.CallbacksDuringTick);
    }



    [Fact]
    public async Task JsonSingleStreamLoader_LoadAsync_with_progress_wires_timer_callback_exactly_once()
    {
        var timer = new ManualProgressTimer();
        var probe = new CallbackProbe();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
            new JsonSingleStreamLoaderOptions(),
            timer,
            NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
        );

        await sut.LoadAsync(ItemsThatTickMidStream(timer, probe), probe.Progress);

        Assert.Equal(1, probe.CallbacksDuringTick);
    }



    [Fact]
    public async Task JsonMultiStreamLoader_LoadAsync_with_progress_wires_timer_callback_exactly_once()
    {
        var timer = new ManualProgressTimer();
        var probe = new CallbackProbe();
        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ => new MemoryStream(),
            new JsonMultiStreamLoaderOptions(),
            timer,
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );

        await sut.LoadAsync(ItemsThatTickMidStream(timer, probe), probe.Progress);

        Assert.Equal(1, probe.CallbacksDuringTick);
    }



    [Fact]
    public async Task JsonSingleStreamExtractor_ExtractAsync_with_progress_wires_timer_callback_exactly_once()
    {
        var timer = new ManualProgressTimer();
        var probe = new CallbackProbe();
        var sut = new JsonSingleStreamExtractor<PersonRecord>
        (
            new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new List<PersonRecord>(People), TestJsonContext.Default.ListPersonRecord))),
            new JsonSingleStreamExtractorOptions(),
            timer,
            NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance
        );

        var enumerator = sut.ExtractAsync(probe.Progress).GetAsyncEnumerator();
        try
        {
            await enumerator.MoveNextAsync();
            probe.Tick(timer);
        }
        finally
        {
            await enumerator.DisposeAsync();
        }

        Assert.Equal(1, probe.CallbacksDuringTick);
    }



    private static async IAsyncEnumerable<PersonRecord> ItemsThatTickMidStream(ManualProgressTimer timer, CallbackProbe probe)
    {
        yield return People[0];
        await Task.Yield();
        probe.Tick(timer);
        yield return People[1];
    }



    /// <summary>Counts progress callbacks and how many of them one timer tick produced.</summary>
    private sealed class CallbackProbe
    {
        private int _callbacks;



        public CallbackProbe()
        {
            Progress = new SynchronousProgress<JsonReport>(_ => _callbacks++);
        }



        public IProgress<JsonReport> Progress { get; }



        public int CallbacksDuringTick { get; private set; } = -1;



        public void Tick(ManualProgressTimer timer)
        {
            var before = _callbacks;
            timer.Fire();
            CallbacksDuringTick = _callbacks - before;
        }
    }
}
