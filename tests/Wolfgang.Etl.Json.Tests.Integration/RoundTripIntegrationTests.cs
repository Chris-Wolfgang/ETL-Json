using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Integration.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Integration;

/// <summary>
/// End-to-end round-trip tests: load items to a real file with a loader, then
/// extract them back from that file with the matching extractor, and assert the
/// sequence survives the trip unchanged. Exercises real <c>FileStream</c> I/O
/// and real <c>System.Text.Json</c> serialization — not in-memory doubles.
/// </summary>
public class RoundTripIntegrationTests
{
    private static List<Person> MakePeople(int count)
    {
        var people = new List<Person>(count);
        for (var i = 0; i < count; i++)
        {
            people.Add
            (
                new Person
                {
                    Id = i,
                    FirstName = "First" + i,
                    LastName = "Last" + i,
                    Age = 20 + (i % 50),
                }
            );
        }

        return people;
    }



    [Fact]
    public async Task JsonSingleStream_when_round_tripped_through_a_file_preserves_all_items_in_order()
    {
        using var workspace = new TempWorkspace();
        var source = MakePeople(5);

        using (var write = workspace.CreateFile("people.json"))
        {
            var loader = new JsonSingleStreamLoader<Person>(write);
            await loader.LoadAsync(source.ToAsyncEnumerable());
        }

        var result = new List<Person>();
        using (var read = workspace.OpenFile("people.json"))
        {
            var extractor = new JsonSingleStreamExtractor<Person>(read);
            await foreach (var person in extractor.ExtractAsync())
            {
                result.Add(person);
            }
        }

        Assert.Equal
        (
            source,
            result
        );
    }



    [Fact]
    public async Task JsonLine_when_round_tripped_through_a_file_preserves_all_items_in_order()
    {
        using var workspace = new TempWorkspace();
        var source = MakePeople(5);

        using (var write = workspace.CreateFile("people.jsonl"))
        {
            var loader = new JsonLineLoader<Person>(write);
            await loader.LoadAsync(source.ToAsyncEnumerable());
        }

        var result = new List<Person>();
        using (var read = workspace.OpenFile("people.jsonl"))
        {
            var extractor = new JsonLineExtractor<Person>(read);
            await foreach (var person in extractor.ExtractAsync())
            {
                result.Add(person);
            }
        }

        Assert.Equal
        (
            source,
            result
        );
    }



    [Fact]
    public async Task JsonLine_when_file_has_one_object_per_line_is_genuine_jsonl()
    {
        using var workspace = new TempWorkspace();
        var source = MakePeople(3);

        using (var write = workspace.CreateFile("people.jsonl"))
        {
            var loader = new JsonLineLoader<Person>(write);
            await loader.LoadAsync(source.ToAsyncEnumerable());
        }

        var lines = await ReadAllNonBlankLinesAsync(workspace.PathFor("people.jsonl"));

        Assert.Equal
        (
            source.Count,
            lines.Count
        );
    }



    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10_000)]
    public async Task JsonSingleStream_when_round_tripped_at_various_sizes_preserves_count(int count)
    {
        using var workspace = new TempWorkspace();
        var source = MakePeople(count);

        using (var write = workspace.CreateFile("bulk.json"))
        {
            var loader = new JsonSingleStreamLoader<Person>(write);
            await loader.LoadAsync(source.ToAsyncEnumerable());
        }

        var result = new List<Person>();
        using (var read = workspace.OpenFile("bulk.json"))
        {
            var extractor = new JsonSingleStreamExtractor<Person>(read);
            await foreach (var person in extractor.ExtractAsync())
            {
                result.Add(person);
            }
        }

        Assert.Equal
        (
            source,
            result
        );
    }



    private static async Task<List<string>> ReadAllNonBlankLinesAsync(string path)
    {
        var lines = new List<string>();
        using var reader = new System.IO.StreamReader(path);
        string? line;
        while ((line = await reader.ReadLineAsync()) is not null)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                lines.Add(line);
            }
        }

        return lines;
    }
}
