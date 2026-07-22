using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public sealed class EtlPipelineJsonExtensionsTests
{
    private static readonly PersonRecord[] People =
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        new() { FirstName = "Carol", LastName = "White", Age = 35 },
    };


    [Fact]
    public async Task JsonLine_stream_round_trip_via_pipeline()
    {
        // Arrange: a source JSONL stream.
        var source = new MemoryStream(Encoding.UTF8.GetBytes(
            string.Join("\n", People.Select(p => System.Text.Json.JsonSerializer.Serialize(p)))));
        var destination = new MemoryStream();

        // Act: JsonLineExtractor -> JsonLineLoader through the fluent EtlPipeline.
        await EtlPipeline
            .Create()
            .JsonLineExtractor<PersonRecord>(source)
            .JsonLineLoader<PersonRecord>(destination)
            .RunAsync();

        // Assert: reading the destination back yields the same records.
        destination.Position = 0;
        var readBack = new List<PersonRecord>();
        await foreach (var p in new JsonLineExtractor<PersonRecord>(destination).ExtractAsync())
        {
            readBack.Add(p);
        }

        Assert.Equal(People, readBack);
    }


    [Fact]
    public async Task JsonLine_file_round_trip_via_pipeline_disposes_streams()
    {
        var inPath = Path.GetTempFileName();
        var outPath = Path.GetTempFileName();
        try
        {
            File.WriteAllText(inPath,
                string.Join("\n", People.Select(p => System.Text.Json.JsonSerializer.Serialize(p))));

            await EtlPipeline
                .Create()
                .JsonLineExtractor<PersonRecord>(inPath)
                .JsonLineLoader<PersonRecord>(outPath)
                .RunAsync();

            // If the factory-owned streams were not disposed, these opens would throw IOException.
            var readBack = new List<PersonRecord>();
            using (var outStream = File.OpenRead(outPath))
            {
                await foreach (var p in new JsonLineExtractor<PersonRecord>(outStream).ExtractAsync())
                {
                    readBack.Add(p);
                }
            }

            Assert.Equal(People, readBack);
        }
        finally
        {
            File.Delete(inPath);
            File.Delete(outPath);
        }
    }


    [Fact]
    public async Task JsonSingleStream_source_to_JsonLine_sink_cross_shape()
    {
        // JSON array in, JSONL out — exercises cross-shape composition.
        var source = new MemoryStream(Encoding.UTF8.GetBytes(
            System.Text.Json.JsonSerializer.Serialize(People)));
        var destination = new MemoryStream();

        await EtlPipeline
            .Create()
            .JsonSingleStreamExtractor<PersonRecord>(source)
            .JsonLineLoader<PersonRecord>(destination)
            .RunAsync();

        destination.Position = 0;
        var lines = Encoding.UTF8.GetString(destination.ToArray())
            .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        Assert.Equal(3, lines.Length);
    }


    [Fact]
    public async Task Through_operator_applies_between_json_source_and_sink()
    {
        var source = new MemoryStream(Encoding.UTF8.GetBytes(
            string.Join("\n", People.Select(p => System.Text.Json.JsonSerializer.Serialize(p)))));
        var destination = new MemoryStream();

        // Keep only people aged 30+ via a stream-to-stream Through stage.
        await EtlPipeline
            .Create()
            .JsonLineExtractor<PersonRecord>(source)
            .Through<PersonRecord>(items => Filter(items, p => p.Age >= 30))
            .JsonLineLoader<PersonRecord>(destination)
            .RunAsync();

        destination.Position = 0;
        var readBack = new List<PersonRecord>();
        await foreach (var p in new JsonLineExtractor<PersonRecord>(destination).ExtractAsync())
        {
            readBack.Add(p);
        }

        Assert.Equal(new[] { "Alice", "Carol" }, readBack.Select(p => p.FirstName).ToArray());
    }


    [Fact]
    public void JsonLineExtractor_null_pipeline_throws()
    {
        Assert.Throws<ArgumentNullException>(
            () => ((EtlPipeline)null!).JsonLineExtractor<PersonRecord>(new MemoryStream()));
    }


    [Fact]
    public void JsonLineLoader_null_path_throws()
    {
        var pipeline = EtlPipeline.Create().JsonLineExtractor<PersonRecord>(new MemoryStream());
        Assert.Throws<ArgumentNullException>(() => pipeline.JsonLineLoader<PersonRecord>((string)null!));
    }


    [Fact]
    public async Task JsonSingleStream_stream_round_trip_via_pipeline()
    {
        var source = new MemoryStream(Encoding.UTF8.GetBytes(
            System.Text.Json.JsonSerializer.Serialize(People)));
        var destination = new MemoryStream();

        await EtlPipeline
            .Create()
            .JsonSingleStreamExtractor<PersonRecord>(source)
            .JsonSingleStreamLoader<PersonRecord>(destination)
            .RunAsync();

        destination.Position = 0;
        var readBack = await Collect(new JsonSingleStreamExtractor<PersonRecord>(destination).ExtractAsync());

        Assert.Equal(People, readBack);
    }


    [Fact]
    public async Task JsonSingleStream_file_round_trip_via_pipeline_disposes_streams()
    {
        var inPath = Path.GetTempFileName();
        var outPath = Path.GetTempFileName();
        try
        {
            File.WriteAllText(inPath, System.Text.Json.JsonSerializer.Serialize(People));

            await EtlPipeline
                .Create()
                .JsonSingleStreamExtractor<PersonRecord>(inPath)
                .JsonSingleStreamLoader<PersonRecord>(outPath)
                .RunAsync();

            // Re-opening the output proves the factory-owned streams were disposed.
            List<PersonRecord> readBack;
            using (var outStream = File.OpenRead(outPath))
            {
                readBack = await Collect(new JsonSingleStreamExtractor<PersonRecord>(outStream).ExtractAsync());
            }

            Assert.Equal(People, readBack);
        }
        finally
        {
            File.Delete(inPath);
            File.Delete(outPath);
        }
    }


    [Fact]
    public async Task Factories_accept_explicit_serializer_options()
    {
        var options = new System.Text.Json.JsonSerializerOptions();
        var source = new MemoryStream(Encoding.UTF8.GetBytes(
            string.Join("\n", People.Select(p => System.Text.Json.JsonSerializer.Serialize(p, options)))));
        var destination = new MemoryStream();

        await EtlPipeline
            .Create()
            .JsonLineExtractor<PersonRecord>(source, options)
            .JsonLineLoader<PersonRecord>(destination, options)
            .RunAsync();

        destination.Position = 0;
        var readBack = await Collect(new JsonLineExtractor<PersonRecord>(destination).ExtractAsync());

        Assert.Equal(People, readBack);
    }


    [Fact]
    public void All_factory_null_arguments_throw()
    {
        var pipeline = EtlPipeline.Create().JsonLineExtractor<PersonRecord>(new MemoryStream());

        // Source factories.
        Assert.Throws<ArgumentNullException>(() => ((EtlPipeline)null!).JsonLineExtractor<PersonRecord>("x"));
        Assert.Throws<ArgumentNullException>(() => EtlPipeline.Create().JsonLineExtractor<PersonRecord>((Stream)null!));
        Assert.Throws<ArgumentNullException>(() => EtlPipeline.Create().JsonLineExtractor<PersonRecord>((string)null!));
        Assert.Throws<ArgumentNullException>(() => ((EtlPipeline)null!).JsonSingleStreamExtractor<PersonRecord>("x"));
        Assert.Throws<ArgumentNullException>(() => EtlPipeline.Create().JsonSingleStreamExtractor<PersonRecord>((Stream)null!));
        Assert.Throws<ArgumentNullException>(() => EtlPipeline.Create().JsonSingleStreamExtractor<PersonRecord>((string)null!));

        // Sink terminators.
        Assert.Throws<ArgumentNullException>(() => ((IEtlPipeline<PersonRecord>)null!).JsonLineLoader<PersonRecord>("x"));
        Assert.Throws<ArgumentNullException>(() => pipeline.JsonLineLoader<PersonRecord>((Stream)null!));
        Assert.Throws<ArgumentNullException>(() => ((IEtlPipeline<PersonRecord>)null!).JsonSingleStreamLoader<PersonRecord>("x"));
        Assert.Throws<ArgumentNullException>(() => pipeline.JsonSingleStreamLoader<PersonRecord>((string)null!));
        Assert.Throws<ArgumentNullException>(() => pipeline.JsonSingleStreamLoader<PersonRecord>((Stream)null!));
    }


    private static async Task<List<T>> Collect<T>(IAsyncEnumerable<T> items)
    {
        var list = new List<T>();
        await foreach (var item in items)
        {
            list.Add(item);
        }

        return list;
    }


    private static async IAsyncEnumerable<T> Filter<T>(IAsyncEnumerable<T> items, Func<T, bool> predicate)
    {
        await foreach (var item in items)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }
}
