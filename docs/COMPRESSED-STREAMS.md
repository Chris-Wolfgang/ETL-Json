# Compressed Streams

All extractors and loaders in `Wolfgang.Etl.Json` accept any `Stream` — including compression wrappers from `System.IO.Compression`. No special API is needed: wrap the underlying stream in a `GZipStream`, `BrotliStream`, or `DeflateStream` before passing it to the constructor.

## Load to GZip-compressed JSONL

```csharp
using var fileStream = File.Create("output.jsonl.gz");
using var gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal, leaveOpen: true);
var loader = new JsonLineLoader<Record>(gzipStream);
await loader.LoadAsync(records);
```

> **Note:** Pass `leaveOpen: true` to `GZipStream` when the underlying stream is owned by the caller. `GZipStream` disposes the inner stream on its own dispose unless `leaveOpen` is set.

## Extract from GZip-compressed JSONL

```csharp
using var fileStream = File.OpenRead("data.jsonl.gz");
using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
var extractor = new JsonLineExtractor<Record>(gzipStream);

await foreach (var item in extractor.ExtractAsync())
{
    Console.WriteLine(item);
}
```

## Extract from GZip-compressed JSON array

```csharp
using var fileStream = File.OpenRead("data.json.gz");
using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
var extractor = new JsonSingleStreamExtractor<Record>(gzipStream);
```

## Brotli variant

```csharp
using var fileStream = File.OpenRead("data.jsonl.br");
using var brotliStream = new BrotliStream(fileStream, CompressionMode.Decompress);
var extractor = new JsonLineExtractor<Record>(brotliStream);
```

> **Note:** `BrotliStream` requires .NET Core 2.1 or later (`netcoreapp2.1` / `netstandard2.1` / `net5.0+`). It is not available on .NET Framework.

## Choosing a compression format

| Format | Class | Extension | Notes |
|---|---|---|---|
| GZip | `GZipStream` | `.gz` | Available on all TFMs including .NET Framework |
| Brotli | `BrotliStream` | `.br` | .NET Core 2.1+ only; best compression ratio |
| Deflate | `DeflateStream` | `.deflate` | Available on all TFMs; GZip is usually preferred |

## Runnable example

See `examples/Wolfgang.Etl.Json.Examples/Program.cs` — `CompressedStreamsExample()` for a complete load-then-extract round-trip using GZip.
