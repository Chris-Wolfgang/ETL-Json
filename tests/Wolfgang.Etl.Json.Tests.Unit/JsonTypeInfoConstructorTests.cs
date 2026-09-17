using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// The source-generated (<c>JsonTypeInfo&lt;T&gt;</c>) constructor overloads that take an options record:
/// they apply the record, run the stage through the type info, and reject a record that also carries
/// <c>SerializerOptions</c> (the type info owns the serializer configuration). Also the record overloads
/// that take a path or a named destination / named sources.
/// </summary>
public class JsonTypeInfoConstructorTests
{
    private static readonly PersonRecord[] People =
    [
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
        new() { FirstName = "Carol", LastName = "White", Age = 41 },
    ];

    private static MemoryStream ArrayOf(params PersonRecord[] people) =>
        new(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(people.ToList(), TestJsonContext.Default.ListPersonRecord)));

    private static MemoryStream ObjectOf(PersonRecord person) =>
        new(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(person, TestJsonContext.Default.PersonRecord)));

    private static async Task<List<PersonRecord>> ToListAsync(IAsyncEnumerable<PersonRecord> items)
    {
        var list = new List<PersonRecord>();
        await foreach (var item in items)
        {
            list.Add(item);
        }

        return list;
    }

    private static async IAsyncEnumerable<PersonRecord> SourceAsync()
    {
        foreach (var person in People)
        {
            yield return person;
            await Task.Yield();
        }
    }



    // ------------------------------------------------------------------ single-stream extractor

    [Fact]
    public async Task JsonSingleStreamExtractor_typeInfo_ctor_applies_the_record_and_extracts_through_the_type_info()
    {
        using var stream = ArrayOf(People);
        var options = new JsonSingleStreamExtractorOptions { SkipItemCount = 1, MaximumItemCount = 1 };

        var sut = new JsonSingleStreamExtractor<PersonRecord>(stream, TestJsonContext.Default.PersonRecord, options);
        var items = await ToListAsync(sut.ExtractAsync());

        Assert.Equal(1, sut.SkipItemCount);
        Assert.Equal(1, sut.MaximumItemCount);
        Assert.Equal([People[1]], items);
    }



    [Fact]
    public void JsonSingleStreamExtractor_typeInfo_ctor_rejects_a_record_carrying_SerializerOptions()
    {
        using var stream = ArrayOf(People);
        var options = new JsonSingleStreamExtractorOptions { SerializerOptions = new JsonSerializerOptions() };

        var ex = Assert.Throws<ArgumentException>(() => new JsonSingleStreamExtractor<PersonRecord>(stream, TestJsonContext.Default.PersonRecord, options));

        Assert.Equal("options", ex.ParamName);
    }



    [Fact]
    public async Task JsonSingleStreamExtractor_path_ctor_with_a_record_reads_the_file()
    {
        var path = Path.GetTempFileName();
        File.WriteAllText(path, JsonSerializer.Serialize(People.ToList(), TestJsonContext.Default.ListPersonRecord));
        try
        {
            var sut = new JsonSingleStreamExtractor<PersonRecord>(path, new JsonSingleStreamExtractorOptions { SkipItemCount = 2 });
            var items = await ToListAsync(sut.ExtractAsync());

            Assert.Equal([People[2]], items);
        }
        finally
        {
            File.Delete(path);
        }
    }



    // ------------------------------------------------------------------ single-stream loader

    [Fact]
    public async Task JsonSingleStreamLoader_typeInfo_ctor_applies_the_record_and_writes_through_the_type_info()
    {
        using var stream = new MemoryStream();
        var options = new JsonSingleStreamLoaderOptions { SkipItemCount = 1 };

        var sut = new JsonSingleStreamLoader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord, options);
        await sut.LoadAsync(SourceAsync());

        Assert.Equal(1, sut.SkipItemCount);
        stream.Position = 0;
        var written = JsonSerializer.Deserialize(stream, TestJsonContext.Default.ListPersonRecord);
        Assert.Equal(People.Skip(1), written);
    }



    [Fact]
    public void JsonSingleStreamLoader_typeInfo_ctor_rejects_a_record_carrying_SerializerOptions()
    {
        using var stream = new MemoryStream();
        var options = new JsonSingleStreamLoaderOptions { SerializerOptions = new JsonSerializerOptions() };

        var ex = Assert.Throws<ArgumentException>(() => new JsonSingleStreamLoader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord, options));

        Assert.Equal("options", ex.ParamName);
    }



    // ------------------------------------------------------------------ multi-stream extractor

    [Fact]
    public async Task JsonMultiStreamExtractor_typeInfo_ctor_applies_the_record_and_extracts_one_object_per_stream()
    {
        var streams = People.Select(ObjectOf).ToList();
        var options = new JsonMultiStreamExtractorOptions { SkipItemCount = 1 };

        var sut = new JsonMultiStreamExtractor<PersonRecord>(streams, TestJsonContext.Default.PersonRecord, options);
        var items = await ToListAsync(sut.ExtractAsync());

        Assert.Equal(1, sut.SkipItemCount);
        Assert.Equal(People.Skip(1), items);
    }



    [Fact]
    public async Task JsonMultiStreamExtractor_named_sources_typeInfo_ctor_applies_the_record_and_extracts_every_source()
    {
        var sources = People.Select((p, i) => new JsonNamedStream(ObjectOf(p), $"person-{i}")).ToList();

        var sut = new JsonMultiStreamExtractor<PersonRecord>(sources, TestJsonContext.Default.PersonRecord, new JsonMultiStreamExtractorOptions { ReportingInterval = 1 });
        var items = await ToListAsync(sut.ExtractAsync());

        Assert.Equal(1, sut.ReportingInterval);
        Assert.Equal(People, items);
    }



    [Fact]
    public void JsonMultiStreamExtractor_typeInfo_ctors_reject_a_record_carrying_SerializerOptions()
    {
        var options = new JsonMultiStreamExtractorOptions { SerializerOptions = new JsonSerializerOptions() };

        var fromStreams = Assert.Throws<ArgumentException>(() => new JsonMultiStreamExtractor<PersonRecord>([new MemoryStream()], TestJsonContext.Default.PersonRecord, options));
        var fromSources = Assert.Throws<ArgumentException>(() => new JsonMultiStreamExtractor<PersonRecord>([new JsonNamedStream(new MemoryStream(), "a")], TestJsonContext.Default.PersonRecord, options));

        Assert.Equal("options", fromStreams.ParamName);
        Assert.Equal("options", fromSources.ParamName);
    }



    // ------------------------------------------------------------------ multi-stream loader

    [Fact]
    public async Task JsonMultiStreamLoader_typeInfo_ctor_applies_the_record_and_writes_one_stream_per_record()
    {
        var written = new List<MemoryStream>();
        var options = new JsonMultiStreamLoaderOptions { SkipItemCount = 1 };

        var sut = new JsonMultiStreamLoader<PersonRecord>(_ => { var s = new MemoryStream(); written.Add(s); return s; }, TestJsonContext.Default.PersonRecord, options);
        await sut.LoadAsync(SourceAsync());

        Assert.Equal(1, sut.SkipItemCount);
        Assert.Equal(2, written.Count);
        Assert.Equal(People[1], JsonSerializer.Deserialize(written[0].ToArray(), TestJsonContext.Default.PersonRecord));
    }



    [Fact]
    public async Task JsonMultiStreamLoader_named_destination_ctors_with_a_record_write_and_report_the_destination_name()
    {
        var written = new List<MemoryStream>();
        JsonNamedDestination Factory(PersonRecord p) { var s = new MemoryStream(); written.Add(s); return new JsonNamedDestination(s, p.FirstName); }

        var reflection = new JsonMultiStreamLoader<PersonRecord>(Factory, new JsonMultiStreamLoaderOptions { MaximumItemCount = 1 });
        await reflection.LoadAsync(SourceAsync());
        var typed = new JsonMultiStreamLoader<PersonRecord>(Factory, TestJsonContext.Default.PersonRecord, new JsonMultiStreamLoaderOptions { MaximumItemCount = 1 });
        await typed.LoadAsync(SourceAsync());

        Assert.Equal(1, reflection.MaximumItemCount);
        Assert.Equal(1, typed.MaximumItemCount);
        Assert.Equal(2, written.Count);
        Assert.All(written, s => Assert.Equal(People[0], JsonSerializer.Deserialize(s.ToArray(), TestJsonContext.Default.PersonRecord)));
    }



    [Fact]
    public void JsonMultiStreamLoader_typeInfo_ctors_reject_a_record_carrying_SerializerOptions()
    {
        var options = new JsonMultiStreamLoaderOptions { SerializerOptions = new JsonSerializerOptions() };

        var fromFactory = Assert.Throws<ArgumentException>(() => new JsonMultiStreamLoader<PersonRecord>(_ => new MemoryStream(), TestJsonContext.Default.PersonRecord, options));
        var fromNamed = Assert.Throws<ArgumentException>(() => new JsonMultiStreamLoader<PersonRecord>(_ => new JsonNamedDestination(new MemoryStream()), TestJsonContext.Default.PersonRecord, options));

        Assert.Equal("options", fromFactory.ParamName);
        Assert.Equal("options", fromNamed.ParamName);
    }
}
