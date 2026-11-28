using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// Pins what <see cref="JsonSingleStreamLoader{TRecord}"/> logs and the item number it hands the error policy, so the
/// logger each constructor receives and the log calls in the load loop are observable.
/// </summary>
public class JsonSingleStreamLoaderLoggingTests
{
    private static readonly PersonRecord[] People =
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        new() { FirstName = "Carol", LastName = "White", Age = 35 },
    };



    public static TheoryData<string> ConstructorNames => new()
    {
        "stream",
        "stream+serializerOptions",
        "stream+options",
        "stream+typeInfo+options",
        "internal stream+options+timer",
        "stream+typeInfo",
        "internal stream+typeInfo+timer",
    };



    [Theory]
    [MemberData(nameof(ConstructorNames))]
    public async Task LoadAsync_logs_through_the_logger_each_constructor_receives(string constructor)
    {
        var logger = new RecordingLogger<JsonSingleStreamLoader<PersonRecord>>();
        var sut = Create(constructor, new MemoryStream(), logger);

        await sut.LoadAsync(People.ToAsyncEnumerable());

        Assert.Contains
        (
            "Starting JSON single-stream loading of PersonRecord.",
            logger.Messages
        );
    }



    [Fact]
    public async Task LoadAsync_logs_each_loaded_item_with_its_count()
    {
        var logger = new RecordingLogger<JsonSingleStreamLoader<PersonRecord>>();
        var sut = new JsonSingleStreamLoader<PersonRecord>(new MemoryStream(), logger);

        await sut.LoadAsync(People.ToAsyncEnumerable());

        Assert.Equal
        (
            new[]
            {
                "Loaded item 1.",
                "Loaded item 2.",
                "Loaded item 3.",
            },
            logger.Messages.Where(m => m.StartsWith("Loaded item", StringComparison.Ordinal)).ToArray()
        );
    }



    [Fact]
    public async Task LoadAsync_logs_the_completed_counts()
    {
        var logger = new RecordingLogger<JsonSingleStreamLoader<PersonRecord>>();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
            new JsonSingleStreamLoaderOptions { SkipItemCount = 1 },
            logger
        );

        await sut.LoadAsync(People.ToAsyncEnumerable());

        Assert.Equal
        (
            "JSON single-stream loading completed. Loaded: 2, skipped: 1.",
            logger.Messages.Last()
        );
    }



    [Fact]
    public async Task LoadAsync_when_items_are_skipped_logs_one_skipped_entry_per_item()
    {
        var logger = new RecordingLogger<JsonSingleStreamLoader<PersonRecord>>();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
            new JsonSingleStreamLoaderOptions { SkipItemCount = 2 },
            logger
        );

        await sut.LoadAsync(People.ToAsyncEnumerable());

        Assert.Equal
        (
            2,
            logger.Entries.Count(e => string.Equals(e.EventId.Name, "SkippedItem", StringComparison.Ordinal))
        );
    }



    [Fact]
    public async Task LoadAsync_when_MaximumItemCount_is_reached_logs_ReachedMaximumItemCount()
    {
        var logger = new RecordingLogger<JsonSingleStreamLoader<PersonRecord>>();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
            new JsonSingleStreamLoaderOptions { MaximumItemCount = 2 },
            logger
        );

        await sut.LoadAsync(People.ToAsyncEnumerable());

        Assert.Single
        (
            logger.Entries,
            e => string.Equals(e.EventId.Name, "ReachedMaximumItemCount", StringComparison.Ordinal)
        );
    }



    [Fact]
    public async Task LoadAsync_when_MaximumItemCount_is_not_reached_does_not_log_ReachedMaximumItemCount()
    {
        var logger = new RecordingLogger<JsonSingleStreamLoader<PersonRecord>>();
        var sut = new JsonSingleStreamLoader<PersonRecord>
        (
            new MemoryStream(),
            new JsonSingleStreamLoaderOptions { MaximumItemCount = 10 },
            logger
        );

        await sut.LoadAsync(People.ToAsyncEnumerable());

        Assert.DoesNotContain
        (
            logger.Entries,
            e => string.Equals(e.EventId.Name, "ReachedMaximumItemCount", StringComparison.Ordinal)
        );
    }



    [Fact]
    public async Task LoadAsync_when_a_record_fails_to_serialize_passes_its_one_based_position_to_the_error_policy()
    {
        var itemNumbers = new List<long>();
        var sut = new JsonSingleStreamLoader<Node>
        (
            new MemoryStream(),
            new JsonSingleStreamLoaderOptions
            {
                SkipItemCount = 1,
                ErrorPolicy = context =>
                {
                    itemNumbers.Add(context.ItemNumber);
                    return ItemErrorAction.Skip;
                },
            }
        );

        await sut.LoadAsync(Nodes().ToAsyncEnumerable());

        // Item 1 is skipped, item 2 loads, item 3 (the cycle) fails: 1 loaded + 1 skipped + 0 errors + 1.
        Assert.Equal
        (
            new long[] { 3 },
            itemNumbers.ToArray()
        );
    }



    [Fact]
    public void Constructor_with_typeInfo_and_options_when_options_set_SerializerOptions_names_the_conflict()
    {
        var exception = Assert.Throws<ArgumentException>
        (
            () => new JsonSingleStreamLoader<PersonRecord>
            (
                new MemoryStream(),
                TestJsonContext.Default.PersonRecord,
                new JsonSingleStreamLoaderOptions { SerializerOptions = new JsonSerializerOptions() }
            )
        );

        Assert.StartsWith
        (
            "SerializerOptions cannot be combined with a JsonTypeInfo; the type info carries its own serializer options.",
            exception.Message,
            StringComparison.Ordinal
        );
    }



    private static JsonSingleStreamLoader<PersonRecord> Create
    (
        string constructor,
        Stream stream,
        RecordingLogger<JsonSingleStreamLoader<PersonRecord>> logger
    ) =>
        constructor switch
        {
            "stream" => new JsonSingleStreamLoader<PersonRecord>(stream, logger),
            "stream+serializerOptions" => new JsonSingleStreamLoader<PersonRecord>(stream, (JsonSerializerOptions?)null, logger),
            "stream+options" => new JsonSingleStreamLoader<PersonRecord>(stream, new JsonSingleStreamLoaderOptions(), logger),
            "stream+typeInfo+options" => new JsonSingleStreamLoader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord, new JsonSingleStreamLoaderOptions(), logger),
            "internal stream+options+timer" => new JsonSingleStreamLoader<PersonRecord>(stream, new JsonSingleStreamLoaderOptions(), new ManualProgressTimer(), logger),
            "stream+typeInfo" => new JsonSingleStreamLoader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord, logger),
            // "internal stream+typeInfo+timer"; the theory data names every constructor, so no other value arrives here.
            _ => new JsonSingleStreamLoader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord, new ManualProgressTimer(), logger),
        };



    private static IEnumerable<Node> Nodes()
    {
        yield return new Node { Id = 1 };
        yield return new Node { Id = 2 };
        var cyclic = new Node { Id = 3 };
        cyclic.Next = cyclic;
        yield return cyclic;
    }



    /// <summary>A record whose self-reference makes the serializer throw a <see cref="JsonException"/>.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public sealed class Node
    {
        public int Id { get; set; }

        [JsonInclude]
        public Node? Next { get; set; }
    }
}
