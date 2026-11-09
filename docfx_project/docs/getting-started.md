# Getting Started

This guide will help you quickly get up and running with Wolfgang.Etl.Json.

## Prerequisites

- A project targeting .NET Framework 4.6.2+, .NET Standard 2.0+, or .NET 5+.
- Familiarity with `IAsyncEnumerable<T>` (`await foreach`) helps; on .NET Framework the
  `Microsoft.Bcl.AsyncInterfaces` dependency provides it.

## Installation

### Via NuGet Package Manager

```bash
dotnet add package Wolfgang.Etl.Json
```

### Via Package Manager Console

```powershell
Install-Package Wolfgang.Etl.Json
```

## Quick Start

Read a JSON Lines file and write the same records as a JSON array:

```csharp
using Wolfgang.Etl.Json;

public record Person(string FirstName, string LastName, int Age);

await using var input = File.OpenRead("people.jsonl");
await using var output = File.Create("people.json");

var extractor = new JsonLineExtractor<Person>(input);
var loader = new JsonSingleStreamLoader<Person>(output);

await loader.LoadAsync(extractor.ExtractAsync());
```

Each stage takes an options record for its settings, for example
`new JsonLineExtractorOptions { MaximumItemCount = 1_000 }`; see the README on GitHub for every option.

## Next Steps

- Explore the [API Reference](../api/index.md) for detailed documentation
- Read the [Introduction](introduction.md) to learn more about Wolfgang.Etl.Json
- Check out example projects in the [GitHub repository](https://github.com/Chris-Wolfgang/ETL-Json)

## Common Issues

- **`JsonException` on the first record**: the input format does not match the stage. A JSON array
  needs `JsonSingleStreamExtractor`, one object per line needs `JsonLineExtractor`.
- **Trimming / Native AOT warnings**: use the constructors that take a source-generated
  `JsonTypeInfo<T>` instead of the reflection-based ones.
- **A malformed record stops the run**: set an `ErrorPolicy` on the options record to skip or
  dead-letter bad records. A JSON array stops at the first malformed element under every policy.

## Additional Resources

- [GitHub Repository](https://github.com/Chris-Wolfgang/ETL-Json)
- [Contributing Guidelines](https://github.com/Chris-Wolfgang/ETL-Json/blob/main/CONTRIBUTING.md)
- [Report an Issue](https://github.com/Chris-Wolfgang/ETL-Json/issues)
