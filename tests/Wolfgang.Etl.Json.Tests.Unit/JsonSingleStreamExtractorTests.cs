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

public class JsonSingleStreamExtractorTests
    : ExtractorBaseContractTests
    <
        JsonSingleStreamExtractor<PersonRecord>,
        PersonRecord,
        JsonReport
    >
{
    private static readonly IReadOnlyList<PersonRecord> ExpectedItems = new List<PersonRecord>
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        new() { FirstName = "Carol", LastName = "White", Age = 35 },
        new() { FirstName = "Dave", LastName = "Brown", Age = 40 },
        new() { FirstName = "Eve", LastName = "Davis", Age = 28 },
    };



    protected override JsonSingleStreamExtractor<PersonRecord> CreateSut(int itemCount)
    {
        var items = ExpectedItems.Take(itemCount).ToList();
        var json = JsonSerializer.Serialize(items);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        return new JsonSingleStreamExtractor<PersonRecord>
        (
            stream,
            NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance
        );
    }



    protected override IReadOnlyList<PersonRecord> CreateExpectedItems() => ExpectedItems;



    protected override JsonSingleStreamExtractor<PersonRecord> CreateSutWithTimer
    (
        IProgressTimer timer
    )
    {
        var json = JsonSerializer.Serialize(ExpectedItems);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        return new JsonSingleStreamExtractor<PersonRecord>
        (
            stream,
            new JsonSerializerOptions(),
            NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance,
            timer
        );
    }



    [Fact]
    public async Task ExtractAsync_when_custom_JsonSerializerOptions_uses_options()
    {
        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var json = JsonSerializer.Serialize(items, options);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        var sut = new JsonSingleStreamExtractor<PersonRecord>
        (
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
            NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance
        );

        var results = new List<PersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            results.Add(item);
        }

        Assert.Single(results);
        Assert.Equal("Alice", results[0].FirstName);
    }



    [Fact]
    public async Task ExtractAsync_when_stream_contains_null_elements_skips_them()
    {
        var json = "[{\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30},null,{\"FirstName\":\"Bob\",\"LastName\":\"Jones\",\"Age\":25}]";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        var sut = new JsonSingleStreamExtractor<PersonRecord>
        (
            stream,
            NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance
        );

        var results = new List<PersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            results.Add(item);
        }

        Assert.Equal(2, results.Count);
        Assert.Equal("Alice", results[0].FirstName);
        Assert.Equal("Bob", results[1].FirstName);
    }



    [Fact]
    public void Constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamExtractor<PersonRecord>
            (
                null!,
                NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamExtractor<PersonRecord>
            (
                new MemoryStream(),
                (ILogger<JsonSingleStreamExtractor<PersonRecord>>)null!
            )
        );
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamExtractor<PersonRecord>
            (
                new MemoryStream(),
                (JsonSerializerOptions)null!,
                NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamExtractor<PersonRecord>
            (
                null!,
                new JsonSerializerOptions(),
                NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonSingleStreamExtractor<PersonRecord>
            (
                new MemoryStream(),
                new JsonSerializerOptions(),
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
            () => new JsonSingleStreamExtractor<PersonRecord>
            (
                new MemoryStream(),
                new JsonSerializerOptions(),
                NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance,
                null!
            )
        );
    }



    [Fact]
    public async Task ExtractAsync_when_property_names_differ_with_JsonPropertyName_maps_correctly()
    {
        var json = "[{\"first_name\":\"Alice\",\"last_name\":\"Smith\",\"age\":30}]";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        var sut = new JsonSingleStreamExtractor<SnakeCasePersonRecord>
        (
            stream,
            NullLogger<JsonSingleStreamExtractor<SnakeCasePersonRecord>>.Instance
        );

        var results = new List<SnakeCasePersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            results.Add(item);
        }

        Assert.Single(results);
        Assert.Equal("Alice", results[0].FirstName);
        Assert.Equal("Smith", results[0].LastName);
        Assert.Equal(30, results[0].Age);
    }



    [Fact]
    public async Task ExtractAsync_when_camelCase_json_with_case_insensitive_option_maps_correctly()
    {
        var json = "[{\"firstName\":\"Bob\",\"lastName\":\"Jones\",\"age\":25}]";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        var sut = new JsonSingleStreamExtractor<PersonRecord>
        (
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
            NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance
        );

        var results = new List<PersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            results.Add(item);
        }

        Assert.Single(results);
        Assert.Equal("Bob", results[0].FirstName);
        Assert.Equal("Jones", results[0].LastName);
        Assert.Equal(25, results[0].Age);
    }
}
