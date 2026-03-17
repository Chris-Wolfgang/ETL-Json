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
            .Select(item =>
            {
                var json = JsonSerializer.Serialize(item);
                return (Stream)new MemoryStream(Encoding.UTF8.GetBytes(json));
            });
    }



    protected override JsonMultiStreamExtractor<PersonRecord> CreateSut(int itemCount)
    {
        return new JsonMultiStreamExtractor<PersonRecord>
        (
            CreateStreams(itemCount),
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance
        );
    }



    protected override IReadOnlyList<PersonRecord> CreateExpectedItems() => ExpectedItems;



    protected override JsonMultiStreamExtractor<PersonRecord> CreateSutWithTimer
    (
        IProgressTimer timer
    )
    {
        return new JsonMultiStreamExtractor<PersonRecord>
        (
            CreateStreams(ExpectedItems.Count),
            null,
            NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
            timer
        );
    }



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
    public void Constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                Array.Empty<Stream>(),
                (ILogger<JsonMultiStreamExtractor<PersonRecord>>)null!
            )
        );
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                Array.Empty<Stream>(),
                (JsonSerializerOptions)null!,
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
                null!,
                null,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                Array.Empty<Stream>(),
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
            () => new JsonMultiStreamExtractor<PersonRecord>
            (
                Array.Empty<Stream>(),
                null,
                NullLogger<JsonMultiStreamExtractor<PersonRecord>>.Instance,
                null!
            )
        );
    }



    [Fact]
    public async Task ExtractAsync_when_stream_deserializes_to_null_skips_item()
    {
        var nullJson = "null";
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
}
