using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// Runs the TestKit per-item error-hook contract against every stage that opts into it: each is driven over
/// exactly one failing item plus items that succeed.
/// </summary>
public static class StageErrorHandlingScenarios
{
    internal const string Good = "{\"FirstName\":\"A\",\"LastName\":\"B\",\"Age\":1}";



    internal static List<LoaderErrorPolicyTests.Node> NodesWithOneCycle()
    {
        var cyclic = new LoaderErrorPolicyTests.Node { Id = 2 };
        cyclic.Next = cyclic;
        return [new LoaderErrorPolicyTests.Node { Id = 1 }, cyclic, new LoaderErrorPolicyTests.Node { Id = 3 }];
    }



    internal static MemoryStream ToStream(string text) => new(Encoding.UTF8.GetBytes(text));
}



public class JsonLineExtractorErrorHandlingContractTests : ErrorHandlingContractTests<JsonLineExtractor<PersonRecord>>
{
    protected override async Task<ErrorHandlingOutcome> RunSingleFaultScenarioAsync(ItemErrorAction policy)
    {
        var text = StageErrorHandlingScenarios.Good + "\n{bad\n" + StageErrorHandlingScenarios.Good + "\n";
        using var stream = StageErrorHandlingScenarios.ToStream(text);
        var sut = new JsonLineExtractor<PersonRecord>(stream, new JsonLineExtractorOptions { ErrorPolicy = _ => policy });

        var aborted = false;
        try
        {
            await sut.ExtractAsync().ToListAsync();
        }
        catch (Exception)
        {
            aborted = true;
        }

        return new ErrorHandlingOutcome(aborted, sut.CurrentItemCount, sut.CurrentErrorItemCount, sut.CurrentSkippedItemCount);
    }
}



public class JsonMultiStreamExtractorErrorHandlingContractTests : ErrorHandlingContractTests<JsonMultiStreamExtractor<PersonRecord>>
{
    protected override async Task<ErrorHandlingOutcome> RunSingleFaultScenarioAsync(ItemErrorAction policy)
    {
        var streams = new[]
        {
            StageErrorHandlingScenarios.ToStream(StageErrorHandlingScenarios.Good),
            StageErrorHandlingScenarios.ToStream("{bad"),
            StageErrorHandlingScenarios.ToStream(StageErrorHandlingScenarios.Good),
        };
        var sut = new JsonMultiStreamExtractor<PersonRecord>(streams, new JsonMultiStreamExtractorOptions { ErrorPolicy = _ => policy });

        var aborted = false;
        try
        {
            await sut.ExtractAsync().ToListAsync();
        }
        catch (Exception)
        {
            aborted = true;
        }

        return new ErrorHandlingOutcome(aborted, sut.CurrentItemCount, sut.CurrentErrorItemCount, sut.CurrentSkippedItemCount);
    }
}



public class JsonLineLoaderErrorHandlingContractTests : ErrorHandlingContractTests<JsonLineLoader<LoaderErrorPolicyTests.Node>>
{
    protected override async Task<ErrorHandlingOutcome> RunSingleFaultScenarioAsync(ItemErrorAction policy)
    {
        using var stream = new MemoryStream();
        var sut = new JsonLineLoader<LoaderErrorPolicyTests.Node>(stream, new JsonLineLoaderOptions { ErrorPolicy = _ => policy });

        var aborted = false;
        try
        {
            await sut.LoadAsync(StageErrorHandlingScenarios.NodesWithOneCycle().ToAsyncEnumerable());
        }
        catch (Exception)
        {
            aborted = true;
        }

        return new ErrorHandlingOutcome(aborted, sut.CurrentItemCount, sut.CurrentErrorItemCount, sut.CurrentSkippedItemCount);
    }
}



public class JsonSingleStreamLoaderErrorHandlingContractTests : ErrorHandlingContractTests<JsonSingleStreamLoader<LoaderErrorPolicyTests.Node>>
{
    protected override async Task<ErrorHandlingOutcome> RunSingleFaultScenarioAsync(ItemErrorAction policy)
    {
        using var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<LoaderErrorPolicyTests.Node>(stream, new JsonSingleStreamLoaderOptions { ErrorPolicy = _ => policy });

        var aborted = false;
        try
        {
            await sut.LoadAsync(StageErrorHandlingScenarios.NodesWithOneCycle().ToAsyncEnumerable());
        }
        catch (Exception)
        {
            aborted = true;
        }

        return new ErrorHandlingOutcome(aborted, sut.CurrentItemCount, sut.CurrentErrorItemCount, sut.CurrentSkippedItemCount);
    }
}



public class JsonMultiStreamLoaderErrorHandlingContractTests : ErrorHandlingContractTests<JsonMultiStreamLoader<LoaderErrorPolicyTests.Node>>
{
    protected override async Task<ErrorHandlingOutcome> RunSingleFaultScenarioAsync(ItemErrorAction policy)
    {
        var sut = new JsonMultiStreamLoader<LoaderErrorPolicyTests.Node>(_ => new MemoryStream(), new JsonMultiStreamLoaderOptions { ErrorPolicy = _ => policy });

        var aborted = false;
        try
        {
            await sut.LoadAsync(StageErrorHandlingScenarios.NodesWithOneCycle().ToAsyncEnumerable());
        }
        catch (Exception)
        {
            aborted = true;
        }

        return new ErrorHandlingOutcome(aborted, sut.CurrentItemCount, sut.CurrentErrorItemCount, sut.CurrentSkippedItemCount);
    }
}
