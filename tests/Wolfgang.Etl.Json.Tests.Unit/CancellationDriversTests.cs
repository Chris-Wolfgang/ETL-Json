using System.Threading;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>The cancellation drivers must also report a clean run when nothing cancels.</summary>
public class CancellationDriversTests
{
    [Fact]
    public async Task DrainExtractorAsync_when_nothing_cancels_reports_all_items_and_no_cancellation()
    {
        var sut = new JsonLineExtractor<PersonRecord>(CancellationDrivers.ToStream(CancellationDrivers.Lines(3)));

        var outcome = await CancellationDrivers.DrainExtractorAsync
        (
            t => sut.ExtractAsync(t),
            cancelAfter: -1,
            CancellationToken.None,
            cts: null
        );

        Assert.False(outcome.Canceled);
        Assert.Equal(3, outcome.ProcessedItemCount);
    }



    [Fact]
    public async Task LoadAsync_when_nothing_cancels_reports_all_items_and_no_cancellation()
    {
        var sut = new JsonLineLoader<PersonRecord>(new System.IO.MemoryStream());

        var outcome = await CancellationDrivers.LoadAsync
        (
            (items, t) => sut.LoadAsync(items, t),
            () => sut.CurrentItemCount,
            count: 3,
            cancelAt: -1,
            preCancelled: false
        );

        Assert.False(outcome.Canceled);
        Assert.Equal(3, outcome.ProcessedItemCount);
    }
}
