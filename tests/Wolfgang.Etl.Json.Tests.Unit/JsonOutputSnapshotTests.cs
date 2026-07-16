using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

    // net462/netstandard2.0: StreamWriter with Encoding.UTF8 writes a UTF-8 BOM preamble.
    // Strip it so the snapshot covers JSON content, not encoding metadata.
    private static string ReadLineLoaderOutput(MemoryStream stream)
    {
        stream.Position = 0;
        return Encoding.UTF8.GetString(stream.ToArray()).TrimStart('﻿');
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
}
