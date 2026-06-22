using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Integration.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Integration;

/// <summary>
/// Round-trip tests for the multi-stream pair: the loader writes one file per
/// item (stream chosen by item content), the extractor reads one item back per
/// file. Order is not guaranteed across files, so comparisons are by content.
/// </summary>
public class MultiStreamRoundTripIntegrationTests
{
    private static List<Person> MakePeople(int count) =>
        Enumerable
            .Range(0, count)
            .Select
            (
                i => new Person
                {
                    Id = i,
                    FirstName = "First" + i,
                    LastName = "Last" + i,
                    Age = 20 + i,
                }
            )
            .ToList();



    [Fact]
    public async Task JsonMultiStream_when_round_tripped_one_file_per_item_preserves_all_items()
    {
        using var workspace = new TempWorkspace();
        var source = MakePeople(5);

        var loader = new JsonMultiStreamLoader<Person>
        (
            person => workspace.CreateFile($"person-{person.Id}.json")
        );
        await loader.LoadAsync(source.ToAsyncEnumerable());

        var files = Directory.GetFiles(workspace.Root, "person-*.json");
        var streams = files.Select(path => File.OpenRead(path));

        var result = new List<Person>();
        var extractor = new JsonMultiStreamExtractor<Person>(streams);
        await foreach (var person in extractor.ExtractAsync())
        {
            result.Add(person);
        }

        Assert.Equal
        (
            source.OrderBy(p => p.Id).ToList(),
            result.OrderBy(p => p.Id).ToList()
        );
    }



    [Fact]
    public async Task JsonMultiStreamLoader_when_loading_writes_exactly_one_file_per_item()
    {
        using var workspace = new TempWorkspace();
        var source = MakePeople(4);

        var loader = new JsonMultiStreamLoader<Person>
        (
            person => workspace.CreateFile($"person-{person.Id}.json")
        );
        await loader.LoadAsync(source.ToAsyncEnumerable());

        var fileCount = Directory.GetFiles(workspace.Root, "person-*.json").Length;

        Assert.Equal
        (
            source.Count,
            fileCount
        );
    }
}
