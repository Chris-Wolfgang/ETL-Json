using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Integration.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Integration;

/// <summary>
/// Round-trip tests that drive the source-generated <c>JsonTypeInfo&lt;T&gt;</c>
/// (reflection-free / AOT-friendly) constructors end-to-end against real files.
/// </summary>
public class SourceGenerationRoundTripIntegrationTests
{
    private static readonly List<Person> Source = new()
    {
        new Person { Id = 1, FirstName = "Alice", LastName = "Smith", Age = 30 },
        new Person { Id = 2, FirstName = "Bob", LastName = "Jones", Age = 25 },
        new Person { Id = 3, FirstName = "Carol", LastName = "White", Age = 35 },
    };



    [Fact]
    public async Task JsonSingleStream_when_using_source_generated_metadata_round_trips()
    {
        using var workspace = new TempWorkspace();

        using (var write = workspace.CreateFile("sg.json"))
        {
            var loader = new JsonSingleStreamLoader<Person>(write, PersonJsonContext.Default.Person);
            await loader.LoadAsync(Source.ToAsyncEnumerable());
        }

        var result = new List<Person>();
        using (var read = workspace.OpenFile("sg.json"))
        {
            var extractor = new JsonSingleStreamExtractor<Person>(read, PersonJsonContext.Default.Person);
            await foreach (var person in extractor.ExtractAsync())
            {
                result.Add(person);
            }
        }

        Assert.Equal
        (
            Source,
            result
        );
    }



    [Fact]
    public async Task JsonLine_when_using_source_generated_metadata_round_trips()
    {
        using var workspace = new TempWorkspace();

        using (var write = workspace.CreateFile("sg.jsonl"))
        {
            var loader = new JsonLineLoader<Person>(write, PersonJsonContext.Default.Person);
            await loader.LoadAsync(Source.ToAsyncEnumerable());
        }

        var result = new List<Person>();
        using (var read = workspace.OpenFile("sg.jsonl"))
        {
            var extractor = new JsonLineExtractor<Person>(read, PersonJsonContext.Default.Person);
            await foreach (var person in extractor.ExtractAsync())
            {
                result.Add(person);
            }
        }

        Assert.Equal
        (
            Source,
            result
        );
    }
}
