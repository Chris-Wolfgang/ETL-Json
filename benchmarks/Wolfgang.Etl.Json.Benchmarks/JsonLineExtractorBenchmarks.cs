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
public class JsonLineExtractorBenchmarks
{
    private byte[] _jsonlData = null!;



    [Params(10, 100, 1000)]
    public int ItemCount { get; set; }



    [GlobalSetup]
    public void Setup()
    {
        var lines = Enumerable.Range(0, ItemCount).Select(i =>
            JsonSerializer.Serialize(new BenchmarkPerson
            {
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                Age = 20 + (i % 50),
                Email = $"person{i}@example.com",
                City = $"City{i % 20}",
            }));

        _jsonlData = Encoding.UTF8.GetBytes(string.Join("\n", lines));
    }



    [Benchmark]
    public async Task<int> ExtractAsync()
    {
        var stream = new MemoryStream(_jsonlData);
        var extractor = new JsonLineExtractor<BenchmarkPerson>
        (
            stream,
            NullLogger<JsonLineExtractor<BenchmarkPerson>>.Instance
        );

        var count = 0;
        await foreach (var _ in extractor.ExtractAsync())
        {
            count++;
        }

        return count;
    }
}
