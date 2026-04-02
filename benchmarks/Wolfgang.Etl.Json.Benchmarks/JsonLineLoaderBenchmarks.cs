using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;


namespace Wolfgang.Etl.Json.Benchmarks;

[MemoryDiagnoser]
public class JsonLineLoaderBenchmarks
{
    private List<BenchmarkPerson> _items = null!;



    [Params(10, 100, 1000)]
    public int ItemCount { get; set; }



    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(0, ItemCount).Select(i => new BenchmarkPerson
        {
            FirstName = $"First{i}",
            LastName = $"Last{i}",
            Age = 20 + (i % 50),
            Email = $"person{i}@example.com",
            City = $"City{i % 20}",
        }).ToList();
    }



    [Benchmark]
    public async Task LoadAsync()
    {
        var stream = new MemoryStream();
        var loader = new JsonLineLoader<BenchmarkPerson>(stream);

        await loader.LoadAsync(_items.ToAsyncEnumerable());
    }
}
