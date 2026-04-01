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

public class JsonMultiStreamExtractorTests
    : ExtractorBaseContractTests
    <
        JsonMultiStreamExtractor<PersonRecord>,
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



    private static IEnumerable<Stream> CreateStreams(int itemCount)
    {
        return ExpectedItems
            .Take(itemCount)
            .Select(Stream (item) =>
            {
                var json = JsonSerializer.Serialize(item);
                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            });
    }



    protected override JsonMultiStreamExtractor<PersonRecord> CreateSut(int itemCount) =>
        new
        (
            CreateStreams(itemCount),
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
        );


    protected override IReadOnlyList<PersonRecord> CreateExpectedItems() => ExpectedItems;



    protected override JsonMultiStreamExtractor<PersonRecord> CreateSutWithTimer
    (
        IProgressTimer timer
    ) =>
        new
        (
            CreateStreams(ExpectedItems.Count),
            new JsonSerializerOptions(),
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
            timer
        );


    [Fact]
    public async Task ExtractAsync_disposes_each_stream_after_reading()
    {
        var streams = new List<MemoryStream>
        {
            new(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(ExpectedItems[0]))),
            new(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(ExpectedItems[1]))),
        };

        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            streams,
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
        );

        var results = new List<PersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            results.Add(item);
        }

        Assert.Equal(2, results.Count);

        foreach (var stream in streams)
        {
            Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
        }
    }



    [Fact]
    public async Task ExtractAsync_when_custom_JsonSerializerOptions_uses_options()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var json = JsonSerializer.Serialize(ExpectedItems[0], options);
        var streams = new List<Stream>
        {
            new MemoryStream(Encoding.UTF8.GetBytes(json)),
        };

        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            streams,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
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
    public void Constructor_when_streams_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                null!,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            streams: [],
            logger: null
        );

        Assert.NotNull(sut);
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                streams: [],
                options: null!,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_streams_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                streams: null!,
                new JsonSerializerOptions(),
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            streams: [],
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
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                streams: [],
                new JsonSerializerOptions(),
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
                timer: null!
            )
        );
    }



    [Fact]
    public async Task ExtractAsync_when_stream_deserializes_to_null_skips_item()
    {
        const string nullJson = "null";
        var validJson = JsonSerializer.Serialize(ExpectedItems[0]);
        var streams = new List<Stream>
        {
            new MemoryStream(Encoding.UTF8.GetBytes(nullJson)),
            new MemoryStream(Encoding.UTF8.GetBytes(validJson)),
        };

        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            streams,
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
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
        const string json = /*lang=json,strict*/ "{\"first_name\":\"Alice\",\"last_name\":\"Smith\",\"age\":30}";
        var streams = new List<Stream>
        {
            new MemoryStream(Encoding.UTF8.GetBytes(json)),
        };

        var sut = new JsonMultiStreamExtractor<SnakeCasePersonRecord>
        (
            streams,
            NullLogger<JsonMultiStreamExtractor<SnakeCasePersonRecord>>.Instance
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
        const string json = /*lang=json,strict*/ "{\"firstName\":\"Bob\",\"lastName\":\"Jones\",\"age\":25}";
        var streams = new List<Stream>
        {
            new MemoryStream(Encoding.UTF8.GetBytes(json)),
        };

        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            streams,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
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



    [Fact]
    public async Task ExtractAsync_when_typeInfo_constructor_deserializes_items()
    {
        var streams = ExpectedItems
            .Take(2)
            .Select(item =>
            {
                var json = JsonSerializer.Serialize(item, TestJsonContext.Default.PersonRecord);
                return (Stream)new MemoryStream(Encoding.UTF8.GetBytes(json));
            })
            .ToList();

        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            streams,
            TestJsonContext.Default.PersonRecord,
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
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
    public void Constructor_with_typeInfo_when_streams_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                null!,
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                streams: Array.Empty<Stream>(),
                typeInfo: null!,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            Array.Empty<Stream>(),
            TestJsonContext.Default.PersonRecord,
            logger: null
        );

        Assert.NotNull(sut);
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_streams_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                streams: null!,
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                streams: Array.Empty<Stream>(),
                typeInfo: null!,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonMultiStreamExtractor<PersonRecord>
        (
            Array.Empty<Stream>(),
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
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                Array.Empty<Stream>(),
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
                timer: null!
            )
        );
    }
}
