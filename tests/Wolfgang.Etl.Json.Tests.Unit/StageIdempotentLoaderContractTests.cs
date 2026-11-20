using System.Collections.Generic;
using System.IO;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>Shared source items for the idempotency contract classes below.</summary>
internal static class IdempotencySource
{
    internal static IReadOnlyList<PersonRecord> Items()
    {
        var items = new List<PersonRecord>();
        for (var i = 0; i < 5; i++)
        {
            items.Add(new PersonRecord { FirstName = "A" + i, LastName = "B", Age = i });
        }

        return items;
    }
}



public class JsonLineLoaderIdempotentContractTests
    : IdempotentLoaderContractTests<JsonLineLoader<PersonRecord>, PersonRecord, JsonReport>
{
    protected override JsonLineLoader<PersonRecord> CreateSut(int itemCount) => new(new MemoryStream());



    protected override IReadOnlyList<PersonRecord> CreateSourceItems() => IdempotencySource.Items();



    protected override IReadOnlyList<PersonRecord>? TryGetLoadedItems(JsonLineLoader<PersonRecord> sut) => null;
}



public class JsonSingleStreamLoaderIdempotentContractTests
    : IdempotentLoaderContractTests<JsonSingleStreamLoader<PersonRecord>, PersonRecord, JsonReport>
{
    protected override JsonSingleStreamLoader<PersonRecord> CreateSut(int itemCount) => new(new MemoryStream());



    protected override IReadOnlyList<PersonRecord> CreateSourceItems() => IdempotencySource.Items();



    protected override IReadOnlyList<PersonRecord>? TryGetLoadedItems(JsonSingleStreamLoader<PersonRecord> sut) => null;
}



public class JsonMultiStreamLoaderIdempotentContractTests
    : IdempotentLoaderContractTests<JsonMultiStreamLoader<PersonRecord>, PersonRecord, JsonReport>
{
    protected override JsonMultiStreamLoader<PersonRecord> CreateSut(int itemCount) => new(_ => new MemoryStream());



    protected override IReadOnlyList<PersonRecord> CreateSourceItems() => IdempotencySource.Items();



    protected override IReadOnlyList<PersonRecord>? TryGetLoadedItems(JsonMultiStreamLoader<PersonRecord> sut) => null;
}
