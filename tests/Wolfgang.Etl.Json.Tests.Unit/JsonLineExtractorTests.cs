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
        JsonLineExtractor<PersonRecord>,
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



    protected override JsonLineExtractor<PersonRecord> CreateSut(int itemCount)
    {
        return new JsonLineExtractor<PersonRecord>
        (
            CreateJsonlStream(itemCount),
            NullLogger<JsonLineExtractor<PersonRecord>>.Instance
        );
    }



    protected override IReadOnlyList<PersonRecord> CreateExpectedItems() => ExpectedItems;



    protected override JsonLineExtractor<PersonRecord> CreateSutWithTimer
    (
        IProgressTimer timer
    )
    {
        return new JsonLineExtractor<PersonRecord>
        (
            CreateJsonlStream(ExpectedItems.Count),
            null,
            NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
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

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream,
            NullLogger<JsonLineExtractor<PersonRecord>>.Instance
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

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
            NullLogger<JsonLineExtractor<PersonRecord>>.Instance
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
            () => new JsonLineExtractor<PersonRecord>
            (
                null!,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                (ILogger<JsonLineExtractor<PersonRecord>>)null!
            )
        );
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                (JsonSerializerOptions)null!,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord>
            (
                null!,
                null,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord>
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
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                null,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
                null!
            )
        );
    }



    [Fact]
    public async Task ExtractAsync_when_line_deserializes_to_null_skips_it()
    {
        var content = "null\n" + JsonSerializer.Serialize(ExpectedItems[0]);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream,
            NullLogger<JsonLineExtractor<PersonRecord>>.Instance
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
    public async Task ExtractAsync_when_property_names_differ_with_JsonPropertyName_maps_correctly()
    {
        var content = "{\"first_name\":\"Alice\",\"last_name\":\"Smith\",\"age\":30}";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<SnakeCasePersonRecord>
        (
            stream,
            NullLogger<JsonLineExtractor<SnakeCasePersonRecord>>.Instance
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
        var content = "{\"firstName\":\"Bob\",\"lastName\":\"Jones\",\"age\":25}";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
            NullLogger<JsonLineExtractor<PersonRecord>>.Instance
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
