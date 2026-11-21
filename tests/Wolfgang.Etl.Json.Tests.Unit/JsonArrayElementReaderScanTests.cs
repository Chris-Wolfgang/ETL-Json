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

public class JsonArrayElementReaderScanTests
{
    private static readonly JsonTypeInfo<PersonRecord> PersonInfo = TestJsonContext.Default.PersonRecord;

    private static readonly JsonTypeInfo<PersonRecord> SkipCommentsPersonInfo = CreatePersonInfo
    (
        new JsonSerializerOptions(TestJsonContext.Default.Options)
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
        }
    );

    private static readonly JsonTypeInfo<int> SkipCommentsIntInfo = CreateIntInfo
    (
        new JsonSerializerOptions(TestJsonContext.Default.Options)
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
        }
    );



    [Theory]
    [InlineData(1)]
    [InlineData(4096)]
    public async Task ReadAsync_when_elements_are_numbers_reads_each_one(int chunk)
    {
        var items = await ReadAllAsync("[1, 22 ,\t333\r\n,4444]", TestJsonContext.Default.Int32, chunk);

        Assert.Equal(new[] { 1, 22, 333, 4444 }, items);
    }



    [Theory]
    [InlineData(1)]
    [InlineData(4096)]
    public async Task ReadAsync_when_elements_are_strings_reads_escaped_quotes_and_backslashes(int chunk)
    {
        var items = await ReadAllAsync("[\"a\\\"b\", \"c\\\\\", \"]\"]", TestJsonContext.Default.String, chunk);

        Assert.Equal(new[] { "a\"b", "c\\", "]" }, items);
    }



    [Theory]
    [InlineData(1)]
    [InlineData(4096)]
    public async Task ReadAsync_when_objects_contain_brackets_in_strings_and_nested_values_measures_each_object(int chunk)
    {
        var json = "[{\"FirstName\":\"a}]\\\"{[\",\"LastName\":\"x\",\"Age\":1,\"Extra\":[1,[2,{\"k\":\"}\"}]]},"
            + "{\"FirstName\":\"b\",\"LastName\":\"y\",\"Age\":2}]";

        var items = await ReadAllAsync(json, PersonInfo, chunk);

        Assert.Equal(new[] { "a}]\"{[", "b" }, items.Select(i => i!.FirstName));
    }



    [Fact]
    public async Task ReadAsync_when_an_element_is_a_null_literal_before_the_closing_bracket_yields_null()
    {
        var items = await ReadAllAsync("[null]", PersonInfo, 1);

        Assert.Null(Assert.Single(items));
    }



    [Fact]
    public async Task ReadAsync_when_the_UTF8_BOM_arrives_one_byte_at_a_time_skips_it()
    {
        var bytes = new byte[] { 0xEF, 0xBB, 0xBF }.Concat(Encoding.UTF8.GetBytes("[1]")).ToArray();

        using var stream = new ChunkedStream(bytes, 1);
        var sut = new JsonArrayElementReader<int>(stream, TestJsonContext.Default.Int32);

        Assert.Equal(new[] { 1 }, await sut.ReadAsync(CancellationToken.None).ToListAsync());
    }



    [Theory]
    [InlineData("[1,]")]
    [InlineData("[1 2]")]
    [InlineData("[1,,2]")]
    [InlineData("[,1]")]
    [InlineData("[1}")]
    [InlineData("[")]
    [InlineData("[1")]
    [InlineData("[\"abc")]
    [InlineData("[{\"Age\":1")]
    [InlineData("[1] x")]
    [InlineData("[1] // not allowed")]
    public async Task ReadAsync_when_the_array_is_malformed_throws_JsonException(string json)
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(json, TestJsonContext.Default.Int32, 1));
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(json, TestJsonContext.Default.Int32, 4096));
    }



    [Fact]
    public async Task ReadAsync_when_an_element_nests_deeper_than_MaxDepth_throws_JsonException()
    {
        var typeInfo = CreatePersonInfo(new JsonSerializerOptions(TestJsonContext.Default.Options) { MaxDepth = 3 });

        await Assert.ThrowsAnyAsync<JsonException>
        (
            () => ReadAllAsync("[{\"a\":{\"b\":{}}}]", typeInfo, 4096)
        );
    }



    [Fact]
    public async Task ReadAsync_when_an_element_is_within_MaxDepth_reads_it()
    {
        var typeInfo = CreatePersonInfo(new JsonSerializerOptions(TestJsonContext.Default.Options) { MaxDepth = 3 });

        var items = await ReadAllAsync("[{\"Age\":1,\"a\":{\"b\":1}}]", typeInfo, 4096);

        Assert.Equal(1, Assert.Single(items)!.Age);
    }



    [Theory]
    [InlineData(1)]
    [InlineData(4096)]
    public async Task ReadAsync_when_comments_are_skipped_reads_around_and_inside_elements(int chunk)
    {
        var json = "/* head */ [ // first\n{\"FirstName\":/* \" { */\"a\",\"LastName\":\"x\",\"Age\":1 // tail\n},\n"
            + "/**/ {\"Age\":2} /* between */ ] // end";

        var items = await ReadAllAsync(json, SkipCommentsPersonInfo, chunk);

        Assert.Equal(new[] { 1, 2 }, items.Select(i => i!.Age));
    }



    [Theory]
    [InlineData(1)]
    [InlineData(4096)]
    public async Task ReadAsync_when_a_line_comment_ends_the_input_completes(int chunk)
    {
        var items = await ReadAllAsync("[1] // runs to the end of the input", SkipCommentsIntInfo, chunk);

        Assert.Equal(new[] { 1 }, items);
    }



    [Theory]
    [InlineData("[1] /")]
    [InlineData("[1] /x")]
    [InlineData("[1] /* never closed")]
    [InlineData("[1] /* never closed *")]
    public async Task ReadAsync_when_a_comment_is_malformed_throws_JsonException(string json)
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(json, SkipCommentsIntInfo, 1));
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(json, SkipCommentsIntInfo, 4096));
    }



    [Theory]
    [InlineData("[{\"Age\":1 /* never closed")]
    [InlineData("[{\"Age\":1 /")]
    [InlineData("[{\"Age\":1 // never ends")]
    public async Task ReadAsync_when_the_input_ends_inside_an_elements_comment_throws_JsonException(string json)
    {
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(json, SkipCommentsPersonInfo, 1));
        await Assert.ThrowsAnyAsync<JsonException>(() => ReadAllAsync(json, SkipCommentsPersonInfo, 4096));
    }



    [Fact]
    public async Task ReadAsync_when_trailing_commas_are_allowed_accepts_one_before_the_closing_bracket()
    {
        var typeInfo = CreateIntInfo(new JsonSerializerOptions(TestJsonContext.Default.Options) { AllowTrailingCommas = true });

        var items = await ReadAllAsync("[1,2,]", typeInfo, 1);

        Assert.Equal(new[] { 1, 2 }, items);
    }



    private static JsonTypeInfo<PersonRecord> CreatePersonInfo(JsonSerializerOptions options) =>
        (JsonTypeInfo<PersonRecord>)options.GetTypeInfo(typeof(PersonRecord));



    private static JsonTypeInfo<int> CreateIntInfo(JsonSerializerOptions options) =>
        (JsonTypeInfo<int>)options.GetTypeInfo(typeof(int));



    private static async Task<List<T?>> ReadAllAsync<T>(string json, JsonTypeInfo<T> typeInfo, int chunk)
    {
        using var stream = new ChunkedStream(Encoding.UTF8.GetBytes(json), chunk);
        var sut = new JsonArrayElementReader<T>(stream, typeInfo);
        return await sut.ReadAsync(CancellationToken.None).ToListAsync();
    }



    /// <summary>
    /// A read-only stream that returns at most <c>chunk</c> bytes per read, to exercise refills.
    /// </summary>
    private sealed class ChunkedStream : MemoryStream
    {
        private readonly int _chunk;



        public ChunkedStream(byte[] bytes, int chunk)
            : base(bytes)
        {
            _chunk = chunk;
        }



        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
            base.ReadAsync(buffer, offset, Math.Min(count, _chunk), cancellationToken);



#if !NETFRAMEWORK
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) =>
            base.ReadAsync(buffer.Slice(0, Math.Min(buffer.Length, _chunk)), cancellationToken);
#endif
    }
}
