using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public class JsonMultiStreamLoaderTests
    : LoaderBaseContractTests
    <
        JsonMultiStreamLoader<PersonRecord>,
        PersonRecord,
        JsonReport
    >
{
    private static readonly IReadOnlyList<PersonRecord> SourceItems = new List<PersonRecord>
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        new() { FirstName = "Carol", LastName = "White", Age = 35 },
        new() { FirstName = "Dave", LastName = "Brown", Age = 40 },
        new() { FirstName = "Eve", LastName = "Davis", Age = 28 },
    };



    protected override JsonMultiStreamLoader<PersonRecord> CreateSut(int itemCount)
    {
        return new JsonMultiStreamLoader<PersonRecord>
        (
            _ => new MemoryStream(),
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );
    }



    protected override IReadOnlyList<PersonRecord> CreateSourceItems() => SourceItems;



    protected override JsonMultiStreamLoader<PersonRecord> CreateSutWithTimer
    (
        IProgressTimer timer
    )
    {
        return new JsonMultiStreamLoader<PersonRecord>
        (
            _ => new MemoryStream(),
            new JsonSerializerOptions(),
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance,
            timer
        );
    }



    [Fact]
    public async Task LoadAsync_writes_one_object_per_stream()
    {
        var streams = new List<MemoryStream>();

        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ =>
            {
                var ms = new MemoryStream();
                streams.Add(ms);
                return ms;
            },
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
            new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        Assert.Equal(2, streams.Count);
    }



    [Fact]
    public async Task LoadAsync_when_custom_JsonSerializerOptions_uses_options()
    {
        var streams = new List<MemoryStream>();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ =>
            {
                var ms = new MemoryStream();
                streams.Add(ms);
                return ms;
            },
            options,
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        Assert.Single(streams);

        var json = Encoding.UTF8.GetString(streams[0].ToArray());
        Assert.Contains("firstName", json);
        Assert.Contains("lastName", json);
    }



    [Fact]
    public async Task LoadAsync_when_stream_factory_returns_null_throws_InvalidOperationException()
    {
        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ => null!,
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await Assert.ThrowsAsync<InvalidOperationException>
        (
            () => sut.LoadAsync(items.ToAsyncEnumerable())
        );
    }



    [Fact]
    public async Task LoadAsync_disposes_each_stream_after_writing()
    {
        var streams = new List<MemoryStream>();

        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ =>
            {
                var ms = new MemoryStream();
                streams.Add(ms);
                return ms;
            },
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
            new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        Assert.Equal(2, streams.Count);

        foreach (var stream in streams)
        {
            Assert.Throws<ObjectDisposedException>(() => stream.WriteByte(0));
        }
    }



    [Fact]
    public void Constructor_when_streamFactory_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                null!,
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ => new MemoryStream(),
            logger: null
        );

        Assert.NotNull(sut);
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                _ => new MemoryStream(),
                options: null!,
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_streamFactory_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                null!,
                new JsonSerializerOptions(),
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ => new MemoryStream(),
            new JsonSerializerOptions(),
            logger: null,
            new ManualProgressTimer()
        );

        Assert.NotNull(sut);
    }



    [Fact]
    public void Internal_constructor_when_timer_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                _ => new MemoryStream(),
                new JsonSerializerOptions(),
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance,
                null!
            )
        );
    }



    [Fact]
    public async Task LoadAsync_when_empty_sequence_creates_no_streams()
    {
        var streamCount = 0;

        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ =>
            {
                streamCount++;
                return new MemoryStream();
            },
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );

        await sut.LoadAsync(AsyncEnumerable.Empty<PersonRecord>());

        Assert.Equal(0, streamCount);
    }



    [Fact]
    public async Task LoadAsync_when_custom_options_output_round_trips_correctly()
    {
        var streams = new List<MemoryStream>();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ =>
            {
                var ms = new MemoryStream();
                streams.Add(ms);
                return ms;
            },
            options,
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        Assert.Single(streams);
        var readOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var deserialized = JsonSerializer.Deserialize<PersonRecord>(streams[0].ToArray(), readOptions);

        Assert.NotNull(deserialized);
        Assert.Equal("Alice", deserialized.FirstName);
        Assert.Equal("Smith", deserialized.LastName);
        Assert.Equal(30, deserialized.Age);
    }



    [Fact]
    public async Task LoadAsync_when_JsonPropertyName_attributes_writes_mapped_names()
    {
        var streams = new List<MemoryStream>();

        var sut = new JsonMultiStreamLoader<SnakeCasePersonRecord>
        (
            _ =>
            {
                var ms = new MemoryStream();
                streams.Add(ms);
                return ms;
            },
            NullLogger<JsonMultiStreamLoader<SnakeCasePersonRecord>>.Instance
        );

        var items = new List<SnakeCasePersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        Assert.Single(streams);
        var json = Encoding.UTF8.GetString(streams[0].ToArray());

        Assert.Contains("first_name", json);
        Assert.Contains("last_name", json);
        Assert.DoesNotContain("FirstName", json);
        Assert.DoesNotContain("LastName", json);
    }



    [Fact]
    public async Task LoadAsync_when_typeInfo_constructor_serializes_items()
    {
        var streams = new List<MemoryStream>();

        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ =>
            {
                var ms = new MemoryStream();
                streams.Add(ms);
                return ms;
            },
            TestJsonContext.Default.PersonRecord,
            NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        Assert.Single(streams);
        var deserialized = JsonSerializer.Deserialize<PersonRecord>(streams[0].ToArray());
        Assert.NotNull(deserialized);
        Assert.Equal("Alice", deserialized.FirstName);
    }



    [Fact]
    public void Constructor_with_typeInfo_when_streamFactory_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                null!,
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                _ => new MemoryStream(),
                typeInfo: null!,
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ => new MemoryStream(),
            TestJsonContext.Default.PersonRecord,
            logger: null
        );

        Assert.NotNull(sut);
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_streamFactory_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                streamFactory: null!,
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                _ => new MemoryStream(),
                typeInfo: null!,
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonMultiStreamLoader<PersonRecord>
        (
            _ => new MemoryStream(),
            TestJsonContext.Default.PersonRecord,
            logger: null,
            new ManualProgressTimer()
        );

        Assert.NotNull(sut);
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_timer_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamLoader<PersonRecord>
            (
                _ => new MemoryStream(),
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonMultiStreamLoader<PersonRecord>>.Instance,
                timer: null!
            )
        );
    }
}
