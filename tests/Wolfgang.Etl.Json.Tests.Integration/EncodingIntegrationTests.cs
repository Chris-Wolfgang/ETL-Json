using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Integration.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Integration;

/// <summary>
/// Encoding-focused integration tests: non-ASCII content must survive a UTF-8
/// round-trip, and a file carrying a leading UTF-8 byte-order mark must still
/// extract cleanly.
/// </summary>
public class EncodingIntegrationTests
{
    private static readonly List<Person> UnicodePeople = new()
    {
        new Person { Id = 1, FirstName = "José", LastName = "Ångström", Age = 41 },
        new Person { Id = 2, FirstName = "测试", LastName = "用户", Age = 33 },
        new Person { Id = 3, FirstName = "Renée", LastName = "Müller", Age = 29 },
    };



    [Fact]
    public async Task JsonSingleStream_when_items_contain_non_ascii_text_round_trips_unchanged()
    {
        using var workspace = new TempWorkspace();

        using (var write = workspace.CreateFile("unicode.json"))
        {
            var loader = new JsonSingleStreamLoader<Person>(write);
            await loader.LoadAsync(UnicodePeople.ToAsyncEnumerable());
        }

        var result = new List<Person>();
        using (var read = workspace.OpenFile("unicode.json"))
        {
            var extractor = new JsonSingleStreamExtractor<Person>(read);
            await foreach (var person in extractor.ExtractAsync())
            {
                result.Add(person);
            }
        }

        Assert.Equal
        (
            UnicodePeople,
            result
        );
    }



    [Fact]
    public async Task JsonLine_when_file_has_a_utf8_bom_extracts_all_items()
    {
        using var workspace = new TempWorkspace();
        var path = workspace.PathFor("bom.jsonl");

        // Write a JSONL file that begins with a UTF-8 BOM, then two records.
        var bom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
        var content =
            "{\"Id\":1,\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30}\n" +
            "{\"Id\":2,\"FirstName\":\"Bob\",\"LastName\":\"Jones\",\"Age\":25}\n";
#if NET5_0_OR_GREATER
        await File.WriteAllTextAsync(path, content, bom);
#else
        var preamble = bom.GetPreamble();
        var body = new UTF8Encoding(false).GetBytes(content);
        using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
        {
            await fs.WriteAsync(preamble, 0, preamble.Length);
            await fs.WriteAsync(body, 0, body.Length);
        }
#endif

        var result = new List<Person>();
        using (var read = workspace.OpenFile("bom.jsonl"))
        {
            var extractor = new JsonLineExtractor<Person>(read);
            await foreach (var person in extractor.ExtractAsync())
            {
                result.Add(person);
            }
        }

        Assert.Equal(2, result.Count);
        Assert.Equal("Alice", result[0].FirstName);
    }
}
