using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wolfgang.Etl.Abstractions;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// A record that fails to serialize goes through the loader's <c>ErrorPolicy</c>: skip counts it and carries
/// on without a partial record in the output, abort (the default) re-throws the original exception.
/// </summary>
public class LoaderErrorPolicyTests
{
    private static readonly Func<ItemErrorContext, ItemErrorAction> Skip = _ => ItemErrorAction.Skip;



    [Fact]
    public async Task JsonLineLoader_when_policy_skips_a_failing_record_writes_the_others_and_counts_the_error()
    {
        using var stream = new MemoryStream();
        var sut = new JsonLineLoader<Node>(stream, new JsonLineLoaderOptions { ErrorPolicy = Skip });

        await sut.LoadAsync(Nodes().ToAsyncEnumerable());

        Assert.Equal("{\"Id\":1,\"Next\":null}\n{\"Id\":3,\"Next\":null}\n", Text(stream).Replace("\r\n", "\n"));
        Assert.Equal(2, sut.CurrentItemCount);
        Assert.Equal(1, sut.CurrentErrorItemCount);
    }



    [Fact]
    public async Task JsonLineLoader_when_policy_skips_with_a_non_UTF8_encoding_writes_the_others()
    {
        using var stream = new MemoryStream();
        var sut = new JsonLineLoader<Node>(stream, new JsonLineLoaderOptions { ErrorPolicy = Skip, Encoding = Encoding.Unicode });

        await sut.LoadAsync(Nodes().ToAsyncEnumerable());

        Assert.Equal(2, sut.CurrentItemCount);
        Assert.Equal(1, sut.CurrentErrorItemCount);
    }



    [Fact]
    public async Task JsonLineLoader_by_default_aborts_with_the_original_exception_and_writes_no_partial_line()
    {
        using var stream = new MemoryStream();
        var sut = new JsonLineLoader<Node>(stream);

        await Assert.ThrowsAsync<JsonException>(() => sut.LoadAsync(Nodes().ToAsyncEnumerable()));

        Assert.Equal("{\"Id\":1,\"Next\":null}\n", Text(stream).Replace("\r\n", "\n"));
    }



    [Fact]
    public async Task JsonSingleStreamLoader_when_policy_skips_a_failing_record_writes_a_valid_array_of_the_others()
    {
        using var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<Node>(stream, new JsonSingleStreamLoaderOptions { ErrorPolicy = Skip });

        await sut.LoadAsync(Nodes().ToAsyncEnumerable());

        Assert.Equal("[{\"Id\":1,\"Next\":null},{\"Id\":3,\"Next\":null}]", Text(stream));
        Assert.Equal(2, sut.CurrentItemCount);
        Assert.Equal(1, sut.CurrentErrorItemCount);
    }



    [Fact]
    public async Task JsonSingleStreamLoader_when_policy_skips_and_output_is_indented_keeps_the_indentation()
    {
        using var stream = new MemoryStream();
        var options = new JsonSingleStreamLoaderOptions
        {
            ErrorPolicy = Skip,
            SerializerOptions = new JsonSerializerOptions { WriteIndented = true, NewLine = "\n" },
        };
        var sut = new JsonSingleStreamLoader<Node>(stream, options);

        await sut.LoadAsync(Nodes().ToAsyncEnumerable());

        var expected = "[\n  {\n    \"Id\": 1,\n    \"Next\": null\n  },\n  {\n    \"Id\": 3,\n    \"Next\": null\n  }\n]";
        Assert.Equal(expected, Text(stream));
    }



    [Fact]
    public async Task JsonMultiStreamLoader_when_policy_skips_a_failing_record_creates_no_destination_for_it()
    {
        var destinations = new Dictionary<int, MemoryStream>();
        var sut = new JsonMultiStreamLoader<Node>
        (
            n =>
            {
                var s = new MemoryStream();
                destinations[n.Id] = s;
                return s;
            },
            new JsonMultiStreamLoaderOptions { ErrorPolicy = Skip }
        );

        await sut.LoadAsync(Nodes().ToAsyncEnumerable());

        Assert.Equal(new[] { 1, 3 }, destinations.Keys.OrderBy(k => k));
        Assert.Equal("{\"Id\":1,\"Next\":null}", Encoding.UTF8.GetString(destinations[1].ToArray()));
        Assert.Equal(2, sut.CurrentItemCount);
        Assert.Equal(1, sut.CurrentErrorItemCount);
    }



    private static List<Node> Nodes()
    {
        var cyclic = new Node { Id = 2 };
        cyclic.Next = cyclic;
        return [new Node { Id = 1 }, cyclic, new Node { Id = 3 }];
    }



    private static string Text(MemoryStream stream) => Encoding.UTF8.GetString(stream.ToArray());



    /// <summary>A node that can reference itself, which the serializer rejects.</summary>
    public sealed class Node
    {
        public int Id { get; set; }



        [JsonInclude]
        public Node? Next;
    }
}
