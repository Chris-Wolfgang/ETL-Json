using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;


namespace Wolfgang.Etl.Json.Benchmarks;

[MemoryDiagnoser]
public class JsonMultiStreamExtractorBenchmarks
{
    private byte[][] _jsonBuffers = null!;



    [Params(10, 100, 1000)]
    public int ItemCount { get; set; }



    [GlobalSetup]
    public void Setup()
    {
        _jsonBuffers = Enumerable.Range(0, ItemCount).Select(i =>
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new BenchmarkPerson
            {
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                Age = 20 + (i % 50),
                Email = $"person{i}@example.com",
                City = $"City{i % 20}",
            }))).ToArray();
    }



    [Benchmark]
    public async Task<int> ExtractAsync()
    {
        var streams = _jsonBuffers.Select(b => (Stream)new MemoryStream(b));
        var extractor = new JsonMultiStreamExtractor<BenchmarkPerson>(streams);

        var count = 0;
        await foreach (var _ in extractor.ExtractAsync())
        {
            count++;
        }

        return count;
    }
}
