using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wolfgang.Etl.Json;
using Wolfgang.Etl.Json.Examples;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Information);
});

await SingleStreamExample(loggerFactory);
Console.WriteLine();
await JsonLineExample(loggerFactory);
Console.WriteLine();
await MultiStreamExample(loggerFactory);
Console.WriteLine();
await MultiStreamLoaderExample(loggerFactory);
Console.WriteLine();
await CustomOptionsExample(loggerFactory);
Console.WriteLine();
await SkipAndMaxExample(loggerFactory);



static async Task SingleStreamExample(ILoggerFactory loggerFactory)
{
    Console.WriteLine("=== JSON Array (Single Stream) Example ===");
    Console.WriteLine();

    var people = new List<Person>
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30, Email = "alice@example.com" },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25, Email = "bob@example.com" },
        new() { FirstName = "Carol", LastName = "White", Age = 35, Email = "carol@example.com" },
    };

    // --- Load to JSON array ---
    var stream = new MemoryStream();
    var loader = new JsonSingleStreamLoader<Person>
    (
        stream,
        loggerFactory.CreateLogger<JsonSingleStreamLoader<Person>>()
    );

    await loader.LoadAsync(people.ToAsyncEnumerable());
    Console.WriteLine($"Loaded {loader.CurrentItemCount} items to JSON array.");

    // Show the JSON
    stream.Position = 0;
    using var reader = new StreamReader(stream);
    var json = await reader.ReadToEndAsync();
    Console.WriteLine(json);

    // --- Extract back ---
    stream.Position = 0;
    var extractor = new JsonSingleStreamExtractor<Person>
    (
        stream,
        loggerFactory.CreateLogger<JsonSingleStreamExtractor<Person>>()
    );

    Console.WriteLine("Extracted items:");
    await foreach (var person in extractor.ExtractAsync())
    {
        Console.WriteLine($"  {person.FirstName} {person.LastName}, age {person.Age}");
    }
}



static async Task JsonLineExample(ILoggerFactory loggerFactory)
{
    Console.WriteLine("=== JSONL (JSON Lines) Example ===");
    Console.WriteLine();

    var people = new List<Person>
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30, Email = "alice@example.com" },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25, Email = "bob@example.com" },
    };

    // --- Load as JSONL (one JSON object per line) ---
    var stream = new MemoryStream();
    var loader = new JsonLineLoader<Person>
    (
        stream,
        loggerFactory.CreateLogger<JsonLineLoader<Person>>()
    );

    await loader.LoadAsync(people.ToAsyncEnumerable());
    Console.WriteLine($"Loaded {loader.CurrentItemCount} items as JSONL.");

    stream.Position = 0;
    using var reader = new StreamReader(stream);
    var content = await reader.ReadToEndAsync();
    Console.WriteLine(content);

    // --- Extract back ---
    stream.Position = 0;
    var extractor = new JsonLineExtractor<Person>
    (
        stream,
        loggerFactory.CreateLogger<JsonLineExtractor<Person>>()
    );

    Console.WriteLine("Extracted items:");
    await foreach (var person in extractor.ExtractAsync())
    {
        Console.WriteLine($"  {person.FirstName} {person.LastName}, age {person.Age}");
    }
}



static async Task MultiStreamExample(ILoggerFactory loggerFactory)
{
    Console.WriteLine("=== Multi-Stream Extractor Example ===");
    Console.WriteLine("Reads one JSON object per stream — like reading one .json file per record.");
    Console.WriteLine();

    // Simulate 3 separate JSON files, each containing one person
    var jsonFiles = new Dictionary<string, byte[]>
    {
        ["alice.json"] = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(new Person { FirstName = "Alice", LastName = "Smith", Age = 30 })),
        ["bob.json"] = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(new Person { FirstName = "Bob", LastName = "Jones", Age = 25 })),
        ["carol.json"] = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(new Person { FirstName = "Carol", LastName = "White", Age = 35 })),
    };

    Console.WriteLine($"Simulated {jsonFiles.Count} JSON files:");
    foreach (var (name, data) in jsonFiles)
    {
        Console.WriteLine($"  {name}: {Encoding.UTF8.GetString(data)}");
    }

    Console.WriteLine();

    // Create streams from the simulated files
    var streams = jsonFiles.Values.Select(data => (Stream)new MemoryStream(data));
    var extractor = new JsonMultiStreamExtractor<Person>
    (
        streams,
        loggerFactory.CreateLogger<JsonMultiStreamExtractor<Person>>()
    );

    Console.WriteLine("Extracted items:");
    await foreach (var person in extractor.ExtractAsync())
    {
        Console.WriteLine($"  {person.FirstName} {person.LastName}, age {person.Age}");
    }
}



static async Task MultiStreamLoaderExample(ILoggerFactory loggerFactory)
{
    Console.WriteLine("=== Multi-Stream Loader Example ===");
    Console.WriteLine("Writes one JSON object per stream — like creating one .json file per record.");
    Console.WriteLine("The stream factory receives the item, so you can name files based on properties.");
    Console.WriteLine();

    var people = new List<Person>
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30, Email = "alice@example.com" },
        new() { FirstName = "Bob", LastName = "Jones", Age = 25, Email = "bob@example.com" },
    };

    // Capture the streams so we can inspect them after (normally you'd use File.Create)
    var files = new Dictionary<string, MemoryStream>();
    var loader = new JsonMultiStreamLoader<Person>
    (
        person =>
        {
            // In production: return File.Create($"output/{person.FirstName}.json");
            var fileName = $"{person.FirstName}_{person.LastName}.json";
            var ms = new MemoryStream();
            files[fileName] = ms;
            return ms;
        },
        loggerFactory.CreateLogger<JsonMultiStreamLoader<Person>>()
    );

    await loader.LoadAsync(people.ToAsyncEnumerable());
    Console.WriteLine($"Loaded {loader.CurrentItemCount} items to {files.Count} streams.");

    Console.WriteLine();
    Console.WriteLine("Generated files:");
    foreach (var (name, ms) in files)
    {
        Console.WriteLine($"  {name}: {Encoding.UTF8.GetString(ms.ToArray())}");
    }
}



static async Task CustomOptionsExample(ILoggerFactory loggerFactory)
{
    Console.WriteLine("=== Custom JsonSerializerOptions Example ===");
    Console.WriteLine();

    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    var people = new List<Person>
    {
        new() { FirstName = "Alice", LastName = "Smith", Age = 30, Email = "alice@example.com" },
    };

    var stream = new MemoryStream();
    var loader = new JsonSingleStreamLoader<Person>
    (
        stream,
        options,
        loggerFactory.CreateLogger<JsonSingleStreamLoader<Person>>()
    );

    await loader.LoadAsync(people.ToAsyncEnumerable());

    stream.Position = 0;
    using var reader = new StreamReader(stream);
    Console.WriteLine("Output with camelCase + indented:");
    Console.WriteLine(await reader.ReadToEndAsync());
}



static async Task SkipAndMaxExample(ILoggerFactory loggerFactory)
{
    Console.WriteLine("=== Skip and Maximum Item Count Example ===");
    Console.WriteLine();

    // Create JSONL with 20 items
    var people = Enumerable.Range(1, 20).Select(i => new Person
    {
        FirstName = $"Person{i}",
        LastName = $"Last{i}",
        Age = 20 + i,
    }).ToList();

    var stream = new MemoryStream();
    var loader = new JsonLineLoader<Person>
    (
        stream,
        loggerFactory.CreateLogger<JsonLineLoader<Person>>()
    );

    await loader.LoadAsync(people.ToAsyncEnumerable());
    Console.WriteLine($"Loaded {loader.CurrentItemCount} items as JSONL.");

    // Extract with skip and max
    stream.Position = 0;
    var extractor = new JsonLineExtractor<Person>
    (
        stream,
        loggerFactory.CreateLogger<JsonLineExtractor<Person>>()
    );
    extractor.SkipItemCount = 5;      // Skip first 5
    extractor.MaximumItemCount = 3;   // Then take 3

    Console.WriteLine("Extracting with SkipItemCount=5, MaximumItemCount=3:");
    await foreach (var person in extractor.ExtractAsync(CancellationToken.None))
    {
        Console.WriteLine($"  {person.FirstName} {person.LastName}");
    }

    Console.WriteLine($"Extracted: {extractor.CurrentItemCount}, Skipped: {extractor.CurrentSkippedItemCount}");
}
