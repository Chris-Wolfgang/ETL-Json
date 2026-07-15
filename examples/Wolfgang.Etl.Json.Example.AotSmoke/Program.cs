// AOT smoke test for Wolfgang.Etl.Json.
// Exercises the JsonTypeInfo<T> overloads (AOT-safe) to verify the library's
// source-generated path works under a native-AOT publish.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wolfgang.Etl.Json;
using Wolfgang.Etl.Json.Example.AotSmoke;

await RunSingleStreamRoundtrip();
await RunJsonLineRoundtrip();
await RunMultiStreamRoundtrip();
Console.WriteLine("AOT smoke: all paths OK.");



static async Task RunSingleStreamRoundtrip()
{
    var people = new List<AotPerson>
    {
        new("Alice", "Smith", 30),
        new("Bob", "Jones", 25),
    };

    var typeInfo = AotPersonContext.Default.AotPerson;
    var stream = new MemoryStream();

    var loader = new JsonSingleStreamLoader<AotPerson>(stream, typeInfo);
    await loader.LoadAsync(people.ToAsyncEnumerable());

    stream.Position = 0;
    var extractor = new JsonSingleStreamExtractor<AotPerson>(stream, typeInfo);
    var extracted = await extractor.ExtractAsync().ToListAsync();

    if (extracted.Count != people.Count)
    {
        throw new InvalidOperationException($"Single-stream: expected {people.Count} items, got {extracted.Count}");
    }
}



static async Task RunJsonLineRoundtrip()
{
    var people = new List<AotPerson>
    {
        new("Carol", "White", 35),
        new("Dave", "Black", 40),
    };

    var typeInfo = AotPersonContext.Default.AotPerson;
    var stream = new MemoryStream();

    var loader = new JsonLineLoader<AotPerson>(stream, typeInfo);
    await loader.LoadAsync(people.ToAsyncEnumerable());

    stream.Position = 0;
    var extractor = new JsonLineExtractor<AotPerson>(stream, typeInfo);
    var extracted = await extractor.ExtractAsync().ToListAsync();

    if (extracted.Count != people.Count)
    {
        throw new InvalidOperationException($"JSONL: expected {people.Count} items, got {extracted.Count}");
    }
}



static async Task RunMultiStreamRoundtrip()
{
    var people = new List<AotPerson>
    {
        new("Eve", "Green", 28),
        new("Frank", "Blue", 33),
    };

    var typeInfo = AotPersonContext.Default.AotPerson;

    var streams = people
        .Select(p =>
        {
            var ms = new MemoryStream();
            // Write each person to its own MemoryStream synchronously via Span
            var bytes = System.Text.Encoding.UTF8.GetBytes(
                System.Text.Json.JsonSerializer.Serialize(p, typeInfo));
            ms.Write(bytes);
            ms.Position = 0;
            return (Stream)ms;
        })
        .Select((s, i) => new JsonNamedStream(s, $"person-{i}.json"))
        .ToList();

    var extractor = new JsonMultiStreamExtractor<AotPerson>(streams, typeInfo);
    var extracted = await extractor.ExtractAsync().ToListAsync();

    if (extracted.Count != people.Count)
    {
        throw new InvalidOperationException($"Multi-stream: expected {people.Count} items, got {extracted.Count}");
    }
}



namespace Wolfgang.Etl.Json.Example.AotSmoke
{
    internal static class AsyncEnumerableExtensions
    {
        internal static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
        {
            var list = new List<T>();
            await foreach (var item in source)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
