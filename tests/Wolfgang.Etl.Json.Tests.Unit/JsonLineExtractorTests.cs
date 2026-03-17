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

public class JsonLineExtractorTests
    : ExtractorBaseContractTests
    <
        JsonLineExtractor<PersonRecord, JsonReport>,
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



    private static Stream CreateJsonlStream(int itemCount)
    {
        var lines = ExpectedItems
            .Take(itemCount)
            .Select(item => JsonSerializer.Serialize(item));
        var content = string.Join("\n", lines);
        return new MemoryStream(Encoding.UTF8.GetBytes(content));
    }



    protected override JsonLineExtractor<PersonRecord, JsonReport> CreateSut(int itemCount)
    {
        return new JsonLineExtractor<PersonRecord, JsonReport>
        (
            CreateJsonlStream(itemCount),
            NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance
        );
    }



    protected override IReadOnlyList<PersonRecord> CreateExpectedItems() => ExpectedItems;



    protected override JsonLineExtractor<PersonRecord, JsonReport> CreateSutWithTimer
    (
        IProgressTimer timer
    )
    {
        return new JsonLineExtractor<PersonRecord, JsonReport>
        (
            CreateJsonlStream(ExpectedItems.Count),
            null,
            NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance,
            timer
        );
    }



    [Fact]
    public async Task ExtractAsync_when_blank_lines_present_skips_them()
    {
        var line1 = JsonSerializer.Serialize(ExpectedItems[0]);
        var line2 = JsonSerializer.Serialize(ExpectedItems[1]);
        var content = $"{line1}\n\n  \n{line2}\n";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord, JsonReport>
        (
            stream,
            NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance
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
    public async Task ExtractAsync_when_custom_JsonSerializerOptions_uses_options()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var json = JsonSerializer.Serialize(ExpectedItems[0], options);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        var sut = new JsonLineExtractor<PersonRecord, JsonReport>
        (
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
            NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance
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
    public void Constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord, JsonReport>
            (
                null!,
                NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord, JsonReport>
            (
                new MemoryStream(),
                (ILogger<JsonLineExtractor<PersonRecord, JsonReport>>)null!
            )
        );
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord, JsonReport>
            (
                new MemoryStream(),
                (JsonSerializerOptions)null!,
                NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord, JsonReport>
            (
                null!,
                null,
                NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord, JsonReport>
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
            () => new JsonLineExtractor<PersonRecord, JsonReport>
            (
                new MemoryStream(),
                null,
                NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance,
                null!
            )
        );
    }



    [Fact]
    public async Task ExtractAsync_when_line_deserializes_to_null_skips_it()
    {
        var content = "null\n" + JsonSerializer.Serialize(ExpectedItems[0]);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord, JsonReport>
        (
            stream,
            NullLogger<JsonLineExtractor<PersonRecord, JsonReport>>.Instance
        );

        var results = new List<PersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            results.Add(item);
        }

        Assert.Single(results);
        Assert.Equal("Alice", results[0].FirstName);
    }
}
