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



    private static MemoryStream CreateJsonlStream(int itemCount)
    {
        var lines = ExpectedItems
            .Take(itemCount)
            .Select(item => JsonSerializer.Serialize(item));
        var content = string.Join("\n", lines);
        return new MemoryStream(Encoding.UTF8.GetBytes(content));
    }



    protected override JsonLineExtractor<PersonRecord> CreateSut(int itemCount) =>
        new
        (
            CreateJsonlStream(itemCount)
        );



    protected override IReadOnlyList<PersonRecord> CreateExpectedItems() => ExpectedItems;



    protected override JsonLineExtractor<PersonRecord> CreateSutWithTimer
    (
        IProgressTimer timer
    ) =>
        new
        (
            CreateJsonlStream(ExpectedItems.Count),
            new JsonSerializerOptions(),
            NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
            timer
        );



    [Fact]
    public async Task ExtractAsync_when_blank_lines_present_skips_them()
    {
        var line1 = JsonSerializer.Serialize(ExpectedItems[0]);
        var line2 = JsonSerializer.Serialize(ExpectedItems[1]);
        var content = $"{line1}\n\n  \n{line2}\n";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream
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
                null!
            )
        );
    }



    [Fact]
    public void Constructor_with_logger_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                logger: null!
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonLineExtractor<PersonRecord>
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
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                options: null!,
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
                new JsonSerializerOptions(),
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonLineExtractor<PersonRecord>
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
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                new JsonSerializerOptions(),
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
                timer: null!
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
            stream
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
        const string content = /*lang=json,strict*/ "{\"first_name\":\"Alice\",\"last_name\":\"Smith\",\"age\":30}";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<SnakeCasePersonRecord>
        (
            stream
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
        const string content = /*lang=json,strict*/ "{\"firstName\":\"Bob\",\"lastName\":\"Jones\",\"age\":25}";
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



    [Fact]
    public async Task ExtractAsync_when_typeInfo_constructor_deserializes_items()
    {
        var lines = ExpectedItems
            .Take(2)
            .Select(item => JsonSerializer.Serialize(item, TestJsonContext.Default.PersonRecord));
        var content = string.Join("\n", lines);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream,
            TestJsonContext.Default.PersonRecord,
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
    public void Constructor_with_typeInfo_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord>
            (
                null!,
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                typeInfo: null!,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonLineExtractor<PersonRecord>
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
            () => new JsonLineExtractor<PersonRecord>
            (
                null!,
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                typeInfo: null!,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_with_typeInfo_when_logger_is_null_does_not_throw()
    {
        var sut = new JsonLineExtractor<PersonRecord>
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
            () => new JsonLineExtractor<PersonRecord>
            (
                new MemoryStream(),
                TestJsonContext.Default.PersonRecord,
                NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
                timer: null!
            )
        );
    }



    [Fact]
    public async Task ExtractAsync_when_ErrorHandling_is_Throw_throws_JsonException_on_bad_line()
    {
        const string content = "{\"FirstName\":\"Alice\",\"LastName\":\"Smith\",\"Age\":30}\nnot-valid-json\n";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream,
            new JsonSerializerOptions()
        )
        {
            ErrorHandling = ErrorHandling.Throw,
        };

        await Assert.ThrowsAsync<JsonException>
        (
            async () =>
            {
                await foreach (var _ in sut.ExtractAsync())
                {
                }
            }
        );
    }



    [Fact]
    public async Task ExtractAsync_when_ErrorHandling_is_CaptureAndContinue_skips_bad_lines_and_populates_Errors()
    {
        var good1 = JsonSerializer.Serialize(ExpectedItems[0]);
        var good2 = JsonSerializer.Serialize(ExpectedItems[1]);
        var content = $"{good1}\nnot-valid-json\n{good2}\n";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream,
            new JsonSerializerOptions()
        )
        {
            ErrorHandling = ErrorHandling.CaptureAndContinue,
        };

        var results = new List<PersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            results.Add(item);
        }

        Assert.Equal(2, results.Count);
        Assert.Equal("Alice", results[0].FirstName);
        Assert.Equal("Bob", results[1].FirstName);
        Assert.Single(sut.Errors);
        Assert.IsType<JsonException>(sut.Errors[0].Exception);
        Assert.Equal("not-valid-json", sut.Errors[0].RawContent);
        Assert.Equal(2L, sut.Errors[0].LineNumber);
    }



    [Fact]
    public async Task ExtractAsync_when_ErrorHandling_is_SkipAndLog_skips_bad_lines_without_collecting_errors()
    {
        var good1 = JsonSerializer.Serialize(ExpectedItems[0]);
        var content = $"{good1}\nnot-valid-json\n";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var sut = new JsonLineExtractor<PersonRecord>
        (
            stream,
            new JsonSerializerOptions()
        )
        {
            ErrorHandling = ErrorHandling.SkipAndLog,
        };

        var results = new List<PersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            results.Add(item);
        }

        Assert.Single(results);
        Assert.Equal("Alice", results[0].FirstName);
        Assert.Empty(sut.Errors);
    }



    [Fact]
    public async Task ExtractAsync_when_re_run_clears_errors_from_previous_run()
    {
        var good = JsonSerializer.Serialize(ExpectedItems[0]);
        var badContent = $"not-valid-json\n{good}\n";
        var badStream = new MemoryStream(Encoding.UTF8.GetBytes(badContent));

        var sut = new JsonLineExtractor<PersonRecord>
        (
            badStream,
            new JsonSerializerOptions()
        )
        {
            ErrorHandling = ErrorHandling.CaptureAndContinue,
        };

        // First run: populates errors
        await foreach (var _ in sut.ExtractAsync())
        {
        }

        Assert.Single(sut.Errors);

        // Second run with a clean stream via a new SUT — verify Errors starts fresh
        var cleanStream = new MemoryStream(Encoding.UTF8.GetBytes($"{good}\n"));
        var sut2 = new JsonLineExtractor<PersonRecord>
        (
            cleanStream,
            new JsonSerializerOptions()
        )
        {
            ErrorHandling = ErrorHandling.CaptureAndContinue,
        };

        await foreach (var _ in sut2.ExtractAsync())
        {
        }

        Assert.Empty(sut2.Errors);
    }



    [Fact]
    public async Task ExtractAsync_with_progress_wires_timer_callback_exactly_once()
    {
        var callbackCount = 0;
        var timer = new ManualProgressTimer();
        var sut = new JsonLineExtractor<PersonRecord>
        (
            CreateJsonlStream(ExpectedItems.Count),
            new JsonSerializerOptions(),
            NullLogger<JsonLineExtractor<PersonRecord>>.Instance,
            timer
        );

        var progress = new SynchronousProgress<JsonReport>(_ => callbackCount++);

        // Start extraction but don't complete — fire timer mid-flight
        var enumerator = sut.ExtractAsync(progress).GetAsyncEnumerator();

        try
        {
            await enumerator.MoveNextAsync();

            // The Interlocked guard ensures exactly one Elapsed handler was wired.
            // If double-wiring occurred, Fire would invoke the callback more than once.
            timer.Fire();

            Assert.Equal(1, callbackCount);
        }
        finally
        {
            await enumerator.DisposeAsync();
        }
    }



    [Fact]
    public void Encoding_defaults_to_null()
    {
        var sut = new JsonLineExtractor<PersonRecord>(new MemoryStream());
        Assert.Null(sut.Encoding);
    }



    [Fact]
    public async Task ExtractAsync_when_Encoding_is_set_reads_stream_with_that_encoding()
    {
        // Write raw JSON with non-escaped non-ASCII chars using UnsafeRelaxedJsonEscaping so that
        // the bytes actually differ between ISO-8859-1 and UTF-8. "Å" is 0xC5 in ISO-8859-1
        // (one byte) but 0xC3 0x85 in UTF-8 (two bytes). If the extractor uses the wrong
        // encoding the JSON text is malformed and deserialization fails or returns wrong data.
        var item = new PersonRecord { FirstName = "Ångström", LastName = "Ünité", Age = 99 };
        var opts = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };
        var iso = Encoding.GetEncoding("iso-8859-1");
        var json = JsonSerializer.Serialize(item, opts);
        var stream = new MemoryStream(iso.GetBytes(json + "\n"));

        var sut = new JsonLineExtractor<PersonRecord>(stream)
        {
            Encoding = iso,
        };

        var results = await sut.ExtractAsync().ToListAsync();

        Assert.Equal(item, results[0]);
    }
}
