using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public class JsonSingleStreamLoaderTests
    : LoaderBaseContractTests
    <
        JsonSingleStreamLoader<PersonRecord>,
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



    protected override JsonSingleStreamLoader<PersonRecord> CreateSut(int itemCount)
    {
        var stream = new MemoryStream();
        return new JsonSingleStreamLoader<PersonRecord>
        (
            stream
        );
    }



    protected override IReadOnlyList<PersonRecord> CreateSourceItems() => SourceItems;



    protected override JsonSingleStreamLoader<PersonRecord> CreateSutWithTimer
    (
        IProgressTimer timer
    )
    {
        var stream = new MemoryStream();
        return new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            new JsonSerializerOptions(),
            NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance,
            timer
        );
    }



    [Fact]
    public async Task LoadAsync_writes_valid_json_array_to_stream()
    {
        var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
            new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        stream.Position = 0;
        var json = Encoding.UTF8.GetString(stream.ToArray());
        var deserialized = JsonSerializer.Deserialize<List<PersonRecord>>(json);

        Assert.NotNull(deserialized);
        Assert.Equal(2, deserialized.Count);
        Assert.Equal("Alice", deserialized[0].FirstName);
        Assert.Equal("Bob", deserialized[1].FirstName);
    }



    [Fact]
    public async Task LoadAsync_when_custom_JsonSerializerOptions_uses_options()
    {
        var stream = new MemoryStream();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            options,
            NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        stream.Position = 0;
        var json = Encoding.UTF8.GetString(stream.ToArray());

#pragma warning disable MA0074
        Assert.Contains("firstName", json);
        Assert.Contains("lastName", json);
        Assert.Contains("age", json);
#pragma warning restore MA0074
    }



    [Fact]
    public void Constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                null!
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
            new JsonSerializerOptions(),
            logger: null
        );

        Assert.NotNull(sut);
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                options: null!,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                stream: null!,
                new JsonSerializerOptions(),
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
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
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                new JsonSerializerOptions(),
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance,
                timer: null!
            )
        );
    }



    [Fact]
    public async Task LoadAsync_when_empty_sequence_writes_valid_empty_json_array()
    {
        var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream
        );

        await sut.LoadAsync(AsyncEnumerable.Empty<PersonRecord>());

        stream.Position = 0;
        var json = Encoding.UTF8.GetString(stream.ToArray());

        Assert.Equal("[]", json);
    }



    [Fact]
    public async Task LoadAsync_when_custom_options_output_round_trips_correctly()
    {
        var stream = new MemoryStream();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            options,
            NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        stream.Position = 0;
        var readOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
#pragma warning disable S6966, AsyncFixer02
        var deserialized = JsonSerializer.Deserialize<List<PersonRecord>>(stream, readOptions);
#pragma warning restore S6966, AsyncFixer02

        Assert.NotNull(deserialized);
        Assert.Single(deserialized);
        Assert.Equal("Alice", deserialized[0].FirstName);
        Assert.Equal("Smith", deserialized[0].LastName);
        Assert.Equal(30, deserialized[0].Age);
    }



    [Fact]
    public async Task LoadAsync_when_JsonPropertyName_attributes_writes_mapped_names()
    {
        var stream = new MemoryStream();

        var sut = new JsonSingleStreamLoader<SnakeCasePersonRecord>
        (
            stream
        );

        var items = new List<SnakeCasePersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        stream.Position = 0;
        var json = Encoding.UTF8.GetString(stream.ToArray());

#pragma warning disable MA0074
        Assert.Contains("first_name", json);
        Assert.Contains("last_name", json);
        Assert.DoesNotContain("FirstName", json);
        Assert.DoesNotContain("LastName", json);
#pragma warning restore MA0074
    }



    [Fact]
    public async Task LoadAsync_when_typeInfo_constructor_serializes_items()
    {
        var stream = new MemoryStream();

        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            TestJsonContext.Default.PersonRecord,
            NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        stream.Position = 0;
        var json = Encoding.UTF8.GetString(stream.ToArray());
        var deserialized = JsonSerializer.Deserialize<List<PersonRecord>>(json);

        Assert.NotNull(deserialized);
        Assert.Single(deserialized);
        Assert.Equal("Alice", deserialized[0].FirstName);
    }



    [Fact]
    public void Constructor_with_typeInfo_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                null!,
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                typeInfo: null!,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
            TestJsonContext.Default.PersonRecord,
            logger: null
        );

        Assert.NotNull(sut);
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                stream: null!,
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                typeInfo: null!,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
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
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance,
                timer: null!
            )
        );
    }
}
