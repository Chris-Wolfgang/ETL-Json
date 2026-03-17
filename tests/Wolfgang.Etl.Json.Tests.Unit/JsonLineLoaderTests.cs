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

public class JsonLineLoaderTests
    : LoaderBaseContractTests
    <
        JsonLineLoader<PersonRecord, JsonReport>,
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



    protected override JsonLineLoader<PersonRecord, JsonReport> CreateSut(int itemCount)
    {
        var stream = new MemoryStream();
        return new JsonLineLoader<PersonRecord, JsonReport>
        (
            stream,
            NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance
        );
    }



    protected override IReadOnlyList<PersonRecord> CreateSourceItems() => SourceItems;



    protected override JsonLineLoader<PersonRecord, JsonReport> CreateSutWithTimer
    (
        IProgressTimer timer
    )
    {
        var stream = new MemoryStream();
        return new JsonLineLoader<PersonRecord, JsonReport>
        (
            stream,
            null,
            NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance,
            timer
        );
    }



    [Fact]
    public async Task LoadAsync_writes_one_json_object_per_line()
    {
        var stream = new MemoryStream();
        var sut = new JsonLineLoader<PersonRecord, JsonReport>
        (
            stream,
            NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
            new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        stream.Position = 0;
        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();
        var lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        Assert.Equal(2, lines.Length);

        var person1 = JsonSerializer.Deserialize<PersonRecord>(lines[0]);
        var person2 = JsonSerializer.Deserialize<PersonRecord>(lines[1]);

        Assert.NotNull(person1);
        Assert.Equal("Alice", person1!.FirstName);
        Assert.NotNull(person2);
        Assert.Equal("Bob", person2!.FirstName);
    }



    [Fact]
    public async Task LoadAsync_when_custom_JsonSerializerOptions_uses_options()
    {
        var stream = new MemoryStream();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var sut = new JsonLineLoader<PersonRecord, JsonReport>
        (
            stream,
            options,
            NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance
        );

        var items = new List<PersonRecord>
        {
            new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        };

        await sut.LoadAsync(items.ToAsyncEnumerable());

        stream.Position = 0;
        var content = Encoding.UTF8.GetString(stream.ToArray());

        Assert.Contains("firstName", content);
        Assert.Contains("lastName", content);
        Assert.Contains("age", content);
    }



    [Fact]
    public void Constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineLoader<PersonRecord, JsonReport>
            (
                null!,
                NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance
            )
        );
    }



    [Fact]
    public void Constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineLoader<PersonRecord, JsonReport>
            (
                new MemoryStream(),
                (ILogger<JsonLineLoader<PersonRecord, JsonReport>>)null!
            )
        );
    }



    [Fact]
    public void Constructor_with_options_when_options_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineLoader<PersonRecord, JsonReport>
            (
                new MemoryStream(),
                (JsonSerializerOptions)null!,
                NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_stream_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineLoader<PersonRecord, JsonReport>
            (
                null!,
                null,
                NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance,
                new ManualProgressTimer()
            )
        );
    }



    [Fact]
    public void Internal_constructor_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => new JsonLineLoader<PersonRecord, JsonReport>
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
            () => new JsonLineLoader<PersonRecord, JsonReport>
            (
                new MemoryStream(),
                null,
                NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance,
                null!
            )
        );
    }



    [Fact]
    public async Task LoadAsync_when_empty_sequence_writes_no_json_lines()
    {
        var stream = new MemoryStream();
        var sut = new JsonLineLoader<PersonRecord, JsonReport>
        (
            stream,
            NullLogger<JsonLineLoader<PersonRecord, JsonReport>>.Instance
        );

        await sut.LoadAsync(AsyncEnumerable.Empty<PersonRecord>());

        stream.Position = 0;
        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd().Trim();

        Assert.Equal(string.Empty, content);
    }
}
