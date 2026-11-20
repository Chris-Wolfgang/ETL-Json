using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>Shared drivers for the TestKit cancellation contract classes below.</summary>
internal static class CancellationDrivers
{
    internal static string Record(int i) => "{\"FirstName\":\"A\",\"LastName\":\"B\",\"Age\":" + i + "}";



    internal static string Lines(int count) => string.Join(NewLine, Enumerable.Range(0, count).Select(Record));



    private static readonly string NewLine = new string((char)10, 1);



    internal static MemoryStream ToStream(string text) => new(Encoding.UTF8.GetBytes(text));



    internal static async Task<CancellationOutcome> DrainExtractorAsync
    (
        Func<CancellationToken, IAsyncEnumerable<PersonRecord>> extract,
        int cancelAfter,
        CancellationToken token,
        CancellationTokenSource? cts
    )
    {
        var processed = 0;
        var canceled = false;
        try
        {
            await foreach (var _ in extract(token))
            {
                processed++;
                if (processed == cancelAfter)
                {
                    cts?.Cancel();
                }
            }
        }
        catch (OperationCanceledException)
        {
            canceled = true;
        }

        return new CancellationOutcome(canceled, processed);
    }



    internal static async IAsyncEnumerable<PersonRecord> Source(int count, int cancelAt, CancellationTokenSource? cts)
    {
        for (var i = 0; i < count; i++)
        {
            if (i == cancelAt)
            {
                cts?.Cancel();
            }

            yield return new PersonRecord { FirstName = "A", LastName = "B", Age = i };
            await Task.Yield();
        }
    }



    internal static async Task<CancellationOutcome> LoadAsync
    (
        Func<IAsyncEnumerable<PersonRecord>, CancellationToken, Task> load,
        Func<int> processedCount,
        int count,
        int cancelAt,
        bool preCancelled
    )
    {
        using var cts = new CancellationTokenSource();
        if (preCancelled)
        {
            cts.Cancel();
        }

        var canceled = false;
        try
        {
            await load(Source(count, cancelAt, preCancelled ? null : cts), cts.Token);
        }
        catch (OperationCanceledException)
        {
            canceled = true;
        }

        return new CancellationOutcome(canceled, processedCount());
    }
}



public class JsonLineExtractorCancellationContractTests : CancellationContractTests<JsonLineExtractor<PersonRecord>>
{
    protected override async Task<CancellationOutcome> RunAndCancelMidStreamAsync(int itemCount, int cancelAfter)
    {
        var sut = new JsonLineExtractor<PersonRecord>(CancellationDrivers.ToStream(CancellationDrivers.Lines(itemCount)));
        using var cts = new CancellationTokenSource();
        return await CancellationDrivers.DrainExtractorAsync(t => sut.ExtractAsync(t), cancelAfter, cts.Token, cts);
    }



    protected override async Task<CancellationOutcome> RunWithPreCancelledTokenAsync(int itemCount)
    {
        var sut = new JsonLineExtractor<PersonRecord>(CancellationDrivers.ToStream(CancellationDrivers.Lines(itemCount)));
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        return await CancellationDrivers.DrainExtractorAsync(t => sut.ExtractAsync(t), cancelAfter: -1, cts.Token, cts: null);
    }
}



public class JsonSingleStreamExtractorCancellationContractTests : CancellationContractTests<JsonSingleStreamExtractor<PersonRecord>>
{
    protected override async Task<CancellationOutcome> RunAndCancelMidStreamAsync(int itemCount, int cancelAfter)
    {
        var sut = new JsonSingleStreamExtractor<PersonRecord>(CancellationDrivers.ToStream("[" + string.Join(",", Enumerable.Range(0, itemCount).Select(CancellationDrivers.Record)) + "]"));
        using var cts = new CancellationTokenSource();
        return await CancellationDrivers.DrainExtractorAsync(t => sut.ExtractAsync(t), cancelAfter, cts.Token, cts);
    }



    protected override async Task<CancellationOutcome> RunWithPreCancelledTokenAsync(int itemCount)
    {
        var sut = new JsonSingleStreamExtractor<PersonRecord>(CancellationDrivers.ToStream("[" + string.Join(",", Enumerable.Range(0, itemCount).Select(CancellationDrivers.Record)) + "]"));
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        return await CancellationDrivers.DrainExtractorAsync(t => sut.ExtractAsync(t), cancelAfter: -1, cts.Token, cts: null);
    }
}



public class JsonMultiStreamExtractorCancellationContractTests : CancellationContractTests<JsonMultiStreamExtractor<PersonRecord>>
{
    protected override async Task<CancellationOutcome> RunAndCancelMidStreamAsync(int itemCount, int cancelAfter)
    {
        var sut = new JsonMultiStreamExtractor<PersonRecord>(Enumerable.Range(0, itemCount).Select(i => (Stream)CancellationDrivers.ToStream(CancellationDrivers.Record(i))));
        using var cts = new CancellationTokenSource();
        return await CancellationDrivers.DrainExtractorAsync(t => sut.ExtractAsync(t), cancelAfter, cts.Token, cts);
    }



    protected override async Task<CancellationOutcome> RunWithPreCancelledTokenAsync(int itemCount)
    {
        var sut = new JsonMultiStreamExtractor<PersonRecord>(Enumerable.Range(0, itemCount).Select(i => (Stream)CancellationDrivers.ToStream(CancellationDrivers.Record(i))));
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        return await CancellationDrivers.DrainExtractorAsync(t => sut.ExtractAsync(t), cancelAfter: -1, cts.Token, cts: null);
    }
}



public class JsonLineLoaderCancellationContractTests : CancellationContractTests<JsonLineLoader<PersonRecord>>
{
    protected override Task<CancellationOutcome> RunAndCancelMidStreamAsync(int itemCount, int cancelAfter)
    {
        var sut = new JsonLineLoader<PersonRecord>(new MemoryStream());
        return CancellationDrivers.LoadAsync((items, t) => sut.LoadAsync(items, t), () => sut.CurrentItemCount, itemCount, cancelAfter, preCancelled: false);
    }



    protected override Task<CancellationOutcome> RunWithPreCancelledTokenAsync(int itemCount)
    {
        var sut = new JsonLineLoader<PersonRecord>(new MemoryStream());
        return CancellationDrivers.LoadAsync((items, t) => sut.LoadAsync(items, t), () => sut.CurrentItemCount, itemCount, cancelAt: -1, preCancelled: true);
    }
}



public class JsonSingleStreamLoaderCancellationContractTests : CancellationContractTests<JsonSingleStreamLoader<PersonRecord>>
{
    protected override Task<CancellationOutcome> RunAndCancelMidStreamAsync(int itemCount, int cancelAfter)
    {
        var sut = new JsonSingleStreamLoader<PersonRecord>(new MemoryStream());
        return CancellationDrivers.LoadAsync((items, t) => sut.LoadAsync(items, t), () => sut.CurrentItemCount, itemCount, cancelAfter, preCancelled: false);
    }



    protected override Task<CancellationOutcome> RunWithPreCancelledTokenAsync(int itemCount)
    {
        var sut = new JsonSingleStreamLoader<PersonRecord>(new MemoryStream());
        return CancellationDrivers.LoadAsync((items, t) => sut.LoadAsync(items, t), () => sut.CurrentItemCount, itemCount, cancelAt: -1, preCancelled: true);
    }
}



public class JsonMultiStreamLoaderCancellationContractTests : CancellationContractTests<JsonMultiStreamLoader<PersonRecord>>
{
    protected override Task<CancellationOutcome> RunAndCancelMidStreamAsync(int itemCount, int cancelAfter)
    {
        var sut = new JsonMultiStreamLoader<PersonRecord>(_ => new MemoryStream());
        return CancellationDrivers.LoadAsync((items, t) => sut.LoadAsync(items, t), () => sut.CurrentItemCount, itemCount, cancelAfter, preCancelled: false);
    }



    protected override Task<CancellationOutcome> RunWithPreCancelledTokenAsync(int itemCount)
    {
        var sut = new JsonMultiStreamLoader<PersonRecord>(_ => new MemoryStream());
        return CancellationDrivers.LoadAsync((items, t) => sut.LoadAsync(items, t), () => sut.CurrentItemCount, itemCount, cancelAt: -1, preCancelled: true);
    }
}
