using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

// Snapshot tests that assert the exact serialized output of each loader.
// Any format drift (property order, whitespace, separators) will break these
// tests — use that as the signal to update the expected strings deliberately.
public class JsonOutputSnapshotTests
{
    private static readonly PersonRecord Alice = new() { FirstName = "Alice", LastName = "Smith", Age = 30 };
    private static readonly PersonRecord Bob = new() { FirstName = "Bob", LastName = "Jones", Age = 25 };

    private static string ReadLineLoaderOutput(MemoryStream stream)
    {
        stream.Position = 0;
        return Encoding.UTF8.GetString(stream.ToArray());
    }



    [Fact]
    public async Task JsonLineLoader_single_item_snapshot()
    {
        var stream = new MemoryStream();
        var sut = new JsonLineLoader<PersonRecord>(stream);

        await sut.LoadAsync(new[] { Alice }.ToAsyncEnumerable());

        var actual = ReadLineLoaderOutput(stream);
        Assert.Equal("{\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30}" + Environment.NewLine, actual);
    }



    [Fact]
    public async Task JsonLineLoader_multiple_items_snapshot()
    {
        var stream = new MemoryStream();
        var sut = new JsonLineLoader<PersonRecord>(stream);

        await sut.LoadAsync(new[] { Alice, Bob }.ToAsyncEnumerable());

        var actual = ReadLineLoaderOutput(stream);
        Assert.Equal
        (
            "{\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30}" + Environment.NewLine +
            "{\"FirstName\":\"Bob\",\"LastName\":\"Jones\",\"Age\":25}" + Environment.NewLine,
            actual
        );
    }



    [Fact]
    public async Task JsonSingleStreamLoader_single_item_snapshot()
    {
        var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>(stream);

        await sut.LoadAsync(new[] { Alice }.ToAsyncEnumerable());

        stream.Position = 0;
        var actual = Encoding.UTF8.GetString(stream.ToArray());
        Assert.Equal("[{\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30}]", actual);
    }



    [Fact]
    public async Task JsonSingleStreamLoader_multiple_items_snapshot()
    {
        var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>(stream);

        await sut.LoadAsync(new[] { Alice, Bob }.ToAsyncEnumerable());

        stream.Position = 0;
        var actual = Encoding.UTF8.GetString(stream.ToArray());
        Assert.Equal
        (
            "[{\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30}," +
            "{\"FirstName\":\"Bob\",\"LastName\":\"Jones\",\"Age\":25}]",
            actual
        );
    }



    [Fact]
    public async Task JsonMultiStreamLoader_single_item_snapshot()
    {
        var streams = new List<MemoryStream>();
        var sut = new JsonMultiStreamLoader<PersonRecord>(_ =>
        {
            var ms = new MemoryStream();
            streams.Add(ms);
            return ms;
        });

        await sut.LoadAsync(new[] { Alice }.ToAsyncEnumerable());

        Assert.Single(streams);
        var actual = Encoding.UTF8.GetString(streams[0].ToArray());
        Assert.Equal("{\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30}", actual);
    }



    [Fact]
    public async Task JsonMultiStreamLoader_multiple_items_each_stream_snapshot()
    {
        var streams = new List<MemoryStream>();
        var sut = new JsonMultiStreamLoader<PersonRecord>(_ =>
        {
            var ms = new MemoryStream();
            streams.Add(ms);
            return ms;
        });

        await sut.LoadAsync(new[] { Alice, Bob }.ToAsyncEnumerable());

        Assert.Equal(2, streams.Count);
        Assert.Equal("{\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30}", Encoding.UTF8.GetString(streams[0].ToArray()));
        Assert.Equal("{\"FirstName\":\"Bob\",\"LastName\":\"Jones\",\"Age\":25}", Encoding.UTF8.GetString(streams[1].ToArray()));
    }



    [Fact]
    public async Task JsonSingleStreamLoader_when_WriteIndented_is_true_writes_indented_output()
    {
        var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            new JsonSingleStreamLoaderOptions
            {
                SerializerOptions = IndentedOptions(),
            }
        );

        await sut.LoadAsync(new[] { Alice }.ToAsyncEnumerable());

        Assert.Equal(IndentedAlice, Encoding.UTF8.GetString(stream.ToArray()));
    }



    [Fact]
    public async Task JsonSingleStreamLoader_with_JsonTypeInfo_when_WriteIndented_is_true_writes_indented_output()
    {
        var stream = new MemoryStream();
        var context = new TestJsonContext(IndentedOptions());
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            context.PersonRecord,
            new JsonSingleStreamLoaderOptions()
        );

        await sut.LoadAsync(new[] { Alice }.ToAsyncEnumerable());

        Assert.Equal(IndentedAlice, Encoding.UTF8.GetString(stream.ToArray()));
    }



    [Fact]
    public async Task JsonSingleStreamLoader_when_Encoder_is_UnsafeRelaxedJsonEscaping_writes_non_ascii_unescaped()
    {
        var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            new JsonSingleStreamLoaderOptions
            {
                SerializerOptions = RelaxedOptions(),
            }
        );

        await sut.LoadAsync(new[] { Zoe }.ToAsyncEnumerable());

        Assert.Equal(RelaxedZoe, Encoding.UTF8.GetString(stream.ToArray()));
    }



    [Fact]
    public async Task JsonSingleStreamLoader_with_JsonTypeInfo_when_Encoder_is_UnsafeRelaxedJsonEscaping_writes_non_ascii_unescaped()
    {
        var stream = new MemoryStream();
        var context = new TestJsonContext(RelaxedOptions());
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            context.PersonRecord,
            new JsonSingleStreamLoaderOptions()
        );

        await sut.LoadAsync(new[] { Zoe }.ToAsyncEnumerable());

        Assert.Equal(RelaxedZoe, Encoding.UTF8.GetString(stream.ToArray()));
    }



    private static readonly PersonRecord Zoe = new() { FirstName = "Zoë", LastName = "Smith & Co", Age = 30 };

    private const string IndentedAlice =
        "[\n  {\n    \"FirstName\": \"Alice\",\n    \"LastName\": \"Smith\",\n    \"Age\": 30\n  }\n]";

    // Default escaping would write \u00EB and \u0026; the relaxed encoder writes them as is.
    private const string RelaxedZoe = "[{\"FirstName\":\"Zoë\",\"LastName\":\"Smith & Co\",\"Age\":30}]";



    private static JsonSerializerOptions IndentedOptions() =>
        new()
        {
            WriteIndented = true,
            NewLine = "\n",
        };



    private static JsonSerializerOptions RelaxedOptions() =>
        new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };
}
