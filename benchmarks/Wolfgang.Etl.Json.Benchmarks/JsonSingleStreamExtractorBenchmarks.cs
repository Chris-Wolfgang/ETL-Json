using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging.Abstractions;

namespace Wolfgang.Etl.Json.Benchmarks;

[MemoryDiagnoser]
public class JsonSingleStreamExtractorBenchmarks
{
    private byte[] _jsonData = null!;



    [Params(10, 100, 1000)]
    public int ItemCount { get; set; }



    [GlobalSetup]
    public void Setup()
    {
        var items = Enumerable.Range(0, ItemCount).Select(i => new BenchmarkPerson
        {
            FirstName = $"First{i}",
            LastName = $"Last{i}",
            Age = 20 + (i % 50),
            Email = $"person{i}@example.com",
            City = $"City{i % 20}",
        });

        _jsonData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(items));
    }



    [Benchmark]
    public async Task<int> ExtractAsync()
    {
        var stream = new MemoryStream(_jsonData);
        var extractor = new JsonSingleStreamExtractor<BenchmarkPerson>
        (
            stream,
            NullLogger<JsonSingleStreamExtractor<BenchmarkPerson>>.Instance
        );

        var count = 0;
        await foreach (var _ in extractor.ExtractAsync())
        {
            count++;
        }

        return count;
    }
}
