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
            null,
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
                null,
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
            () => new JsonSingleStreamExtractor<PersonRecord>
            (
                new MemoryStream(),
                null,
                NullLogger<JsonSingleStreamExtractor<PersonRecord>>.Instance,
                null!
            )
        );
    }
}
