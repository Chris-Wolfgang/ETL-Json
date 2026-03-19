# Wolfgang.Etl.Json

Extractors and Loaders for reading and writing JSON, JSONL, and multi-stream JSON files, built on [Wolfgang.Etl.Abstractions](https://github.com/Chris-Wolfgang/ETL-Abstractions).

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-Multi--Targeted-purple.svg)](https://dotnet.microsoft.com/)
[![GitHub](https://img.shields.io/badge/GitHub-Repository-181717?logo=github)](https://github.com/Chris-Wolfgang/ETL-Json)

---

## Installation

```bash
dotnet add package Wolfgang.Etl.Json
```

**NuGet Package:** Coming soon to NuGet.org

---

## Features

| Component | Description |
|-----------|-------------|
| `JsonSingleStreamExtractor` | Extracts items from a JSON array (`[{...},{...}]`) in a single stream |
| `JsonSingleStreamLoader` | Writes items as a JSON array to a single stream |
| `JsonMultiStreamExtractor` | Extracts one item per stream (e.g., one JSON object per file) |
| `JsonMultiStreamLoader` | Writes one item per stream, with stream creation driven by item properties |
| `JsonLineExtractor` | Extracts items from JSONL/NDJSON (one JSON object per line) |
| `JsonLineLoader` | Writes items as JSONL/NDJSON (one JSON object per line) |

All components support:
- `System.Text.Json` serialization with optional `JsonSerializerOptions`
- `ILogger<T>` for structured diagnostic logging at Debug, Information, Warning, and Error levels
- `SkipItemCount` and `MaximumItemCount` for pagination
- Progress reporting via `IProgress<TProgress>` with configurable `ReportingInterval`
- Cancellation via `CancellationToken`

---

## Quick Start

### Extract from a JSON array

```csharp
using var stream = File.OpenRead("people.json");
var extractor = new JsonSingleStreamExtractor<Person>(stream, logger);

await foreach (var person in extractor.ExtractAsync(cancellationToken))
{
    Console.WriteLine(person.Name);
}
```

### Load to a JSON array

```csharp
using var stream = File.Create("output.json");
var loader = new JsonSingleStreamLoader<Person>(stream, logger);

await loader.LoadAsync(items, cancellationToken);
```

### Extract from multiple files (one object per file)

```csharp
var streams = Directory.GetFiles("data/", "*.json").Select(File.OpenRead);
var extractor = new JsonMultiStreamExtractor<Person>(streams, logger);

await foreach (var person in extractor.ExtractAsync(cancellationToken))
{
    Console.WriteLine(person.Name);
}
```

### Load to multiple files (one object per file)

```csharp
var loader = new JsonMultiStreamLoader<Person>
(
    person => File.Create($"output/{person.Id}.json"),
    logger
);

await loader.LoadAsync(items, cancellationToken);
```

### Extract from JSONL/NDJSON

```csharp
using var stream = File.OpenRead("data.jsonl");
var extractor = new JsonLineExtractor<Person>(stream, logger);

await foreach (var person in extractor.ExtractAsync(cancellationToken))
{
    Console.WriteLine(person.Name);
}
```

### Load to JSONL/NDJSON

```csharp
using var stream = File.Create("output.jsonl");
var loader = new JsonLineLoader<Person>(stream, logger);

await loader.LoadAsync(items, cancellationToken);
```

### Custom serialization options

All extractors and loaders accept an optional `JsonSerializerOptions`:

```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
};

var extractor = new JsonSingleStreamExtractor<Person>(stream, options, logger);
```

---

## Target Frameworks

| Platform | Versions |
|----------|----------|
| .NET Framework | 4.6.2, 4.8.1 |
| .NET Standard | 2.0 |
| .NET | 8.0, 10.0 |

Test coverage spans: .NET Framework 4.6.2 through 4.8.1, .NET Core 3.1, .NET 5.0 through 10.0.

---

## Code Quality & Static Analysis

This project enforces strict code quality standards through 7 specialized analyzers and custom async-first rules:

### Analyzers in Use

1. **Microsoft.CodeAnalysis.NetAnalyzers** - Built-in .NET analyzers for correctness and performance
2. **Roslynator.Analyzers** - Advanced refactoring and code quality rules
3. **AsyncFixer** - Async/await best practices and anti-pattern detection
4. **Microsoft.VisualStudio.Threading.Analyzers** - Thread safety and async patterns
5. **Microsoft.CodeAnalysis.BannedApiAnalyzers** - Prevents usage of banned synchronous APIs
6. **Meziantou.Analyzer** - Comprehensive code quality rules
7. **SonarAnalyzer.CSharp** - Industry-standard code analysis

### Async-First Enforcement

This library uses `BannedSymbols.txt` to prohibit synchronous APIs:

- `Task.Wait()`, `Task.Result` - Use `await` instead
- `Thread.Sleep()` - Use `await Task.Delay()` instead
- Synchronous file I/O (`File.ReadAllText`) - Use async versions
- Synchronous stream operations - Use `ReadAsync()`, `WriteAsync()`
- Obsolete APIs (`WebClient`, `BinaryFormatter`)

---

## Building from Source

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later
- Optional: [PowerShell Core](https://github.com/PowerShell/PowerShell) for formatting scripts

### Build Steps

```bash
# Clone the repository
git clone https://github.com/Chris-Wolfgang/ETL-Json.git
cd ETL-Json

# Restore dependencies
dotnet restore

# Build the solution
dotnet build --configuration Release

# Run tests
dotnet test --configuration Release
```

### Code Formatting

This project uses `.editorconfig` and `dotnet format`:

```bash
dotnet format
dotnet format --verify-no-changes
```

### Building Documentation

This project uses [DocFX](https://dotnet.github.io/docfx/) to generate API documentation:

```bash
# Install DocFX (one-time setup)
dotnet tool install -g docfx

# Generate API metadata and build documentation
cd docfx_project
docfx metadata  # Extract API metadata from source code
docfx build     # Build HTML documentation

# Documentation is generated in the docs/ folder at the repository root
```

The documentation is automatically built and deployed to GitHub Pages when changes are pushed to the `main` branch.

**Local Preview:**
```bash
# Serve documentation locally (with live reload)
cd docfx_project
docfx build --serve

# Open http://localhost:8080 in your browser
```

---

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for:
- Code quality standards
- Build and test instructions
- Pull request guidelines
- Analyzer configuration details

---

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## Documentation

- **GitHub Repository:** [https://github.com/Chris-Wolfgang/ETL-Json](https://github.com/Chris-Wolfgang/ETL-Json)
- **API Documentation:** https://Chris-Wolfgang.github.io/ETL-Json/
- **Contributing Guide:** [CONTRIBUTING.md](CONTRIBUTING.md)
