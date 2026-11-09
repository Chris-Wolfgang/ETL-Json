# Introduction

Welcome to Wolfgang.Etl.Json!

## Overview

Extractors and loaders for reading and writing JSON when building ETLs with the
[Wolfgang.Etl](https://github.com/Chris-Wolfgang/ETL-Abstractions) pattern. Every stage is built on
`System.Text.Json`, streams its input and output asynchronously, and plugs into an `EtlPipeline`.

## Key Features

- **Three formats, six stages**: JSON arrays (`JsonSingleStreamExtractor` / `JsonSingleStreamLoader`),
  JSON Lines / NDJSON (`JsonLineExtractor` / `JsonLineLoader`), and one object per stream or file
  (`JsonMultiStreamExtractor` / `JsonMultiStreamLoader`).
- **Options records**: one record per stage carries the serializer options and the shared settings
  (`SkipItemCount`, `MaximumItemCount`, `ReportingInterval`, `ErrorPolicy`).
- **AOT-friendly**: every stage has a constructor that takes a source-generated `JsonTypeInfo<T>`.
- **Checkpoint and resume** for JSON Lines via `EnableCheckpointing`, `CurrentByteOffset` and `StartByteOffset`.
- **Error policies**: skip or dead-letter malformed records instead of failing the run.
- **Diagnostics**: `ILogger<T>` logging, progress reporting, and the `Wolfgang.Etl.Json` metrics meter.
- **Wide reach**: .NET Framework 4.6.2 through .NET 10, plus .NET Standard 2.0.

## Getting Help

If you need help with Wolfgang.Etl.Json, please:

- Check the [Getting Started](getting-started.md) guide
- Review the [API Reference](../api/index.md)
- Visit the [GitHub repository](https://github.com/Chris-Wolfgang/ETL-Json)
- Open an issue on [GitHub Issues](https://github.com/Chris-Wolfgang/ETL-Json/issues)
