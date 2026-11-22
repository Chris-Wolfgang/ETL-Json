using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public class JsonArrayElementReaderSeekableTests
{
    private const string Good = "{\"FirstName\":\"A\",\"LastName\":\"B\",\"Age\":1}";



    [Fact]
    public async Task ReadAsync_when_the_stream_can_seek_reads_every_element()
    {
        var json = "[" + string.Join(",", Enumerable.Range(0, 3000).Select(i => $"{{\"Age\":{i}}}")) + "]";

        var items = await ReadAllAsync(ToStream(json));

        Assert.Equal(Enumerable.Range(0, 3000), items.Select(i => i!.Age));
    }



    [Fact]
    public async Task ReadAsync_when_a_later_element_is_malformed_delivers_every_element_before_it_then_throws()
    {
        // Large enough that the serializer yields the early buffers before it reaches the malformed element.
        var json = "[" + string.Join(",", Enumerable.Range(0, 3000).Select(i => $"{{\"Age\":{i}}}")) + ",{bad}]";
        using var stream = ToStream(json);
        var sut = new JsonArrayElementReader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord);
        var delivered = new List<int>();

        await Assert.ThrowsAnyAsync<JsonException>
        (
            async () =>
            {
                await foreach (var item in sut.ReadAsync(CancellationToken.None))
                {
                    delivered.Add(item!.Age);
                }
            }
        );

        Assert.Equal(Enumerable.Range(0, 3000), delivered);
    }



    [Fact]
    public async Task ReadAsync_when_the_second_element_is_malformed_delivers_the_first_then_throws()
    {
        using var stream = ToStream("[" + Good + ",{bad}]");
        var sut = new JsonArrayElementReader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord);
        await using var enumerator = sut.ReadAsync(CancellationToken.None).GetAsyncEnumerator();

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal(1, enumerator.Current!.Age);
        await Assert.ThrowsAnyAsync<JsonException>(async () => await enumerator.MoveNextAsync());
    }



    [Fact]
    public async Task ReadAsync_when_the_stream_does_not_start_at_zero_rereads_from_where_it_started()
    {
        var prefix = Encoding.UTF8.GetBytes("ignored prefix");
        var body = Encoding.UTF8.GetBytes("[" + Good + ",{bad}]");
        using var stream = new MemoryStream(prefix.Concat(body).ToArray());
        stream.Position = prefix.Length;
        var sut = new JsonArrayElementReader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord);
        await using var enumerator = sut.ReadAsync(CancellationToken.None).GetAsyncEnumerator();

        Assert.True(await enumerator.MoveNextAsync());
        await Assert.ThrowsAnyAsync<JsonException>(async () => await enumerator.MoveNextAsync());
    }



    [Fact]
    public async Task ReadAsync_when_the_array_is_not_valid_JSON_throws_JsonException()
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(ToStream("[" + Good + " " + Good + "]")));
    }



    [Fact]
    public async Task ReadAsync_when_token_is_cancelled_throws_OperationCanceledException()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        using var stream = ToStream("[" + Good + "]");
        var sut = new JsonArrayElementReader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord);

        await Assert.ThrowsAnyAsync<OperationCanceledException>
        (
            async () => await sut.ReadAsync(cts.Token).ToListAsync()
        );
    }



    private static async Task<List<PersonRecord?>> ReadAllAsync(Stream stream)
    {
        using (stream)
        {
            var sut = new JsonArrayElementReader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord);
            return await sut.ReadAsync(CancellationToken.None).ToListAsync();
        }
    }



    private static MemoryStream ToStream(string text) => new(Encoding.UTF8.GetBytes(text));
}
