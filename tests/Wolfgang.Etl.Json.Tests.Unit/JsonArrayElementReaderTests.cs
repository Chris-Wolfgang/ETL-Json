using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public class JsonArrayElementReaderTests
{
    private const string Good = "{\"FirstName\":\"A\",\"LastName\":\"B\",\"Age\":1}";



    [Fact]
    public void Constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonArrayElementReader<PersonRecord>(null!, TestJsonContext.Default.PersonRecord)
        );
    }



    [Fact]
    public void Constructor_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        using var stream = new MemoryStream();

        Assert.Throws<ArgumentNullException>
        (
            () => new JsonArrayElementReader<PersonRecord>(stream, null!)
        );
    }



    [Fact]
    public async Task ReadAsync_when_elements_span_many_buffer_fills_yields_every_element_in_order()
    {
        var people = Enumerable
            .Range(0, 200)
            .Select(i => (PersonRecord?)new PersonRecord { FirstName = new string('x', i * 7), LastName = "L" + i, Age = i })
            .ToList();
        var json = "[" + string.Join(",", people.Select(p => JsonSerializer.Serialize(p!, TestJsonContext.Default.PersonRecord))) + "]";

        var items = await ReadAllAsync(new TrickleStream(Encoding.UTF8.GetBytes(json), 37));

        Assert.Equal(people, items);
    }



    [Fact]
    public async Task ReadAsync_when_an_element_is_larger_than_the_buffer_grows_the_buffer()
    {
        var big = new PersonRecord { FirstName = new string('y', 100_000), LastName = "Z", Age = 9 };
        var json = "[" + JsonSerializer.Serialize(big, TestJsonContext.Default.PersonRecord) + "]";

        var items = await ReadAllAsync(ToStream(json));

        Assert.Equal(big, Assert.Single(items));
    }



    [Fact]
    public async Task ReadAsync_when_input_starts_with_a_UTF8_BOM_skips_it()
    {
        var bytes = new byte[] { 0xEF, 0xBB, 0xBF }.Concat(Encoding.UTF8.GetBytes("[" + Good + "]")).ToArray();

        var items = await ReadAllAsync(new TrickleStream(bytes, 1));

        Assert.Single(items);
    }



    [Fact]
    public async Task ReadAsync_when_array_is_empty_yields_nothing()
    {
        var items = await ReadAllAsync(ToStream("  [ ]  "));

        Assert.Empty(items);
    }



    [Fact]
    public async Task ReadAsync_when_an_element_is_null_yields_null()
    {
        var items = await ReadAllAsync(ToStream("[null," + Good + "]"));

        Assert.Equal(2, items.Count);
        Assert.Null(items[0]);
    }



    [Fact]
    public async Task ReadAsync_when_options_allow_comments_and_trailing_commas_reads_the_array()
    {
        var options = new JsonSerializerOptions(TestJsonContext.Default.Options)
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };
        var typeInfo = (JsonTypeInfo<PersonRecord>)options.GetTypeInfo(typeof(PersonRecord));

        var items = await ReadAllAsync(ToStream("/* head */ [" + Good + ", // one\n" + Good + ",] // tail"), typeInfo);

        Assert.Equal(2, items.Count);
    }



    [Fact]
    public async Task ReadAsync_when_input_is_empty_throws_JsonException()
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(ToStream(string.Empty)));
    }



    [Fact]
    public async Task ReadAsync_when_root_is_not_an_array_throws_JsonException()
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(ToStream(Good)));
    }



    [Fact]
    public async Task ReadAsync_when_content_follows_the_array_throws_JsonException()
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(ToStream("[" + Good + "] [ ]")));
    }



    [Fact]
    public async Task ReadAsync_when_the_array_is_not_closed_throws_JsonException()
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(ToStream("[" + Good + ",")));
    }



    [Fact]
    public async Task ReadAsync_when_an_element_has_the_wrong_shape_throws_JsonException()
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(ToStream("[{\"Age\":\"not a number\"}]")));
    }



    [Fact]
    public async Task ReadAsync_does_not_parse_past_the_element_it_returns()
    {
        using var stream = ToStream("[" + Good + ",{bad}]");
        var sut = new JsonArrayElementReader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord);
        await using var enumerator = sut.ReadAsync(CancellationToken.None).GetAsyncEnumerator();

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal(1, enumerator.Current!.Age);
        await Assert.ThrowsAnyAsync<JsonException>(async () => await enumerator.MoveNextAsync());
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



    [Fact]
    public async Task TrickleStream_returns_at_most_the_chunk_size_from_every_read_overload()
    {
        // The reader calls only one ReadAsync overload per target framework; exercise both here.
        using var stream = new TrickleStream(new byte[10], 3);
        var buffer = new byte[10];

        Assert.Equal(3, await stream.ReadAsync(buffer, 0, buffer.Length, CancellationToken.None));
#if !NETFRAMEWORK
        Assert.Equal(3, await stream.ReadAsync(buffer.AsMemory(), CancellationToken.None));
#endif
    }



    private static Task<List<PersonRecord?>> ReadAllAsync(Stream stream) =>
        ReadAllAsync(stream, TestJsonContext.Default.PersonRecord);



    private static async Task<List<PersonRecord?>> ReadAllAsync(Stream stream, JsonTypeInfo<PersonRecord> typeInfo)
    {
        using (stream)
        {
            var sut = new JsonArrayElementReader<PersonRecord>(stream, typeInfo);
            return await sut.ReadAsync(CancellationToken.None).ToListAsync();
        }
    }



    // A stream that cannot seek is always read element by element; JsonArrayElementReaderSeekableTests
    // covers the seekable fast path.
    private static MemoryStream ToStream(string text) => new TrickleStream(Encoding.UTF8.GetBytes(text), int.MaxValue);



    /// <summary>
    /// A read-only stream that returns at most <c>chunk</c> bytes per read, to exercise refills.
    /// </summary>
    private sealed class TrickleStream : MemoryStream
    {
        private readonly int _chunk;



        public TrickleStream(byte[] bytes, int chunk)
            : base(bytes)
        {
            _chunk = chunk;
        }




        public override bool CanSeek => false;



        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
            base.ReadAsync(buffer, offset, Math.Min(count, _chunk), cancellationToken);



#if NETCOREAPP3_1 || NET5_0
        // MemoryStream names this parameter 'destination' up to net5.0 and 'buffer' from net6.0 (S927).
        public override ValueTask<int> ReadAsync(Memory<byte> destination, CancellationToken cancellationToken = default) =>
            base.ReadAsync(destination.Slice(0, Math.Min(destination.Length, _chunk)), cancellationToken);
#elif !NETFRAMEWORK
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) =>
            base.ReadAsync(buffer.Slice(0, Math.Min(buffer.Length, _chunk)), cancellationToken);
#endif
    }
}
