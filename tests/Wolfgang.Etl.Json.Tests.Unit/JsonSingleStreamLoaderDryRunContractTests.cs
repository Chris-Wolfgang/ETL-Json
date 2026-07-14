using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public class JsonSingleStreamLoaderDryRunContractTests
    : SupportsDryRunContractTests<JsonSingleStreamLoader<PersonRecord>>
{
    private static readonly IReadOnlyList<PersonRecord> SourceItems = new List<PersonRecord>
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
    };



    protected override JsonSingleStreamLoader<PersonRecord> CreateSut() =>
        new(new MemoryStream());



    protected override async Task<bool> RunAndReportSideEffectAsync(bool isDryRun)
    {
        var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>(stream) { IsDryRun = isDryRun };
        await sut.LoadAsync(SourceItems.ToAsyncEnumerable());
        return stream.Length > 0;
    }
}
