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
/// The path constructors open the file themselves and must close it on <c>Dispose</c>; the
/// record overloads must also apply the record's stage-specific settings. A file still held open
/// cannot be reopened with <see cref="FileShare.None"/>, which is what the disposal facts check.
/// </summary>
public class OwnedStreamTests
{
    private static readonly PersonRecord[] People =
    [
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25 },
    ];



    [Fact]
    public async Task JsonLineExtractor_path_ctor_with_a_record_reads_the_file_and_applies_the_record()
    {
        var path = WriteTempFile(string.Join("\n", People.Select(p => JsonSerializer.Serialize(p, TestJsonContext.Default.PersonRecord))));
        try
        {
            var options = new JsonLineExtractorOptions { Encoding = Encoding.UTF8 };
            using var sut = new JsonLineExtractor<PersonRecord>(path, options);

            var items = await ToListAsync(sut.ExtractAsync());

            Assert.Equal(People, items);
            Assert.Same(Encoding.UTF8, sut.Encoding);
        }
        finally
        {
            File.Delete(path);
        }
    }



    [Fact]
    public void JsonLineExtractor_path_ctor_with_a_record_closes_the_file_on_Dispose()
    {
        var path = WriteTempFile(JsonSerializer.Serialize(People[0], TestJsonContext.Default.PersonRecord));
        try
        {
            var sut = new JsonLineExtractor<PersonRecord>(path, new JsonLineExtractorOptions());

            sut.Dispose();

            AssertFileIsClosed(path);
        }
        finally
        {
            File.Delete(path);
        }
    }



    [Fact]
    public void JsonSingleStreamExtractor_path_ctor_closes_the_file_on_Dispose()
    {
        var path = WriteTempFile(JsonSerializer.Serialize(People.ToList(), TestJsonContext.Default.ListPersonRecord));
        try
        {
            var sut = new JsonSingleStreamExtractor<PersonRecord>(path, (JsonSerializerOptions?)null);

            sut.Dispose();

            AssertFileIsClosed(path);
        }
        finally
        {
            File.Delete(path);
        }
    }



    [Fact]
    public async Task JsonLineLoader_typeInfo_record_ctor_applies_the_record()
    {
        using var stream = new MemoryStream();
        var sut = new JsonLineLoader<PersonRecord>(stream, TestJsonContext.Default.PersonRecord, new JsonLineLoaderOptions { IsDryRun = true });

        await sut.LoadAsync(People.ToAsyncEnumerable());

        Assert.True(sut.IsDryRun);
        Assert.Equal(0, stream.Length);
    }



    private static string WriteTempFile(string content)
    {
        var path = Path.GetTempFileName();
        File.WriteAllText(path, content);
        return path;
    }



    private static void AssertFileIsClosed(string path)
    {
        // Throws IOException while another handle (the extractor's) still has the file open.
        using var exclusive = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

        Assert.True(exclusive.CanWrite);
    }



    private static async Task<List<PersonRecord>> ToListAsync(IAsyncEnumerable<PersonRecord> items)
    {
        var list = new List<PersonRecord>();
        await foreach (var item in items)
        {
            list.Add(item);
        }

        return list;
    }
}
