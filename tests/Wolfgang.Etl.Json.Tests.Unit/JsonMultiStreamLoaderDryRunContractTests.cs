using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public class JsonMultiStreamLoaderDryRunContractTests
    : SupportsDryRunContractTests<JsonMultiStreamLoader<PersonRecord>>
{
    private static readonly IReadOnlyList<PersonRecord> SourceItems = new List<PersonRecord>
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
    };



    protected override JsonMultiStreamLoader<PersonRecord> CreateSut() =>
        new(_ => new MemoryStream());



    protected override async Task<bool> RunAndReportSideEffectAsync(bool isDryRun)
    {
        var factoryCalled = false;
        var sut = new JsonMultiStreamLoader<PersonRecord>(_ =>
        {
            factoryCalled = true;
            return new MemoryStream();
        })
        {
            IsDryRun = isDryRun,
        };

        await sut.LoadAsync(SourceItems.ToAsyncEnumerable());
        return factoryCalled;
    }
}
