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
            stream,
            NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
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
            null,
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
            stream,
            NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
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
        Assert.Equal(2, deserialized!.Count);
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

        Assert.Contains("firstName", json);
        Assert.Contains("lastName", json);
        Assert.Contains("age", json);
    }



    [Fact]
    public void Constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                null!,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                (ILogger<JsonSingleStreamLoader<PersonRecord>>)null!
            )
        );
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                (JsonSerializerOptions)null!,
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
                null!,
                null,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                null,
                null!,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_timer_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                null,
                NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance,
                null!
            )
        );
    }



    [Fact]
    public async Task LoadAsync_when_empty_sequence_writes_valid_empty_json_array()
    {
        var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            stream,
            NullLogger<JsonSingleStreamLoader<PersonRecord>>.Instance
        );

        await sut.LoadAsync(AsyncEnumerable.Empty<PersonRecord>());

        stream.Position = 0;
        var json = Encoding.UTF8.GetString(stream.ToArray());

        Assert.Equal("[]", json);
    }
}
