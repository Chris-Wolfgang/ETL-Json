# Copilot Coding Agent Instructions

## Repository Summary

**ETL-Json** ships the `Wolfgang.Etl.Json` NuGet package: extractors and loaders that read and write
JSON for pipelines built on `Wolfgang.Etl.Abstractions`.

- **Six stages**: `JsonSingleStreamExtractor<T>` / `JsonSingleStreamLoader<T>` (one JSON array per stream),
  `JsonLineExtractor<T>` / `JsonLineLoader<T>` (JSON Lines / NDJSON), and
  `JsonMultiStreamExtractor<T>` / `JsonMultiStreamLoader<T>` (one object per stream or file).
- **Supporting types**: an options record per stage (`Json*Options`), `JsonReport` (progress),
  `JsonNamedStream` / `JsonNamedDestination`, the fluent `EtlPipelineJsonSourceExtensions` /
  `EtlPipelineJsonSinkExtensions`, `JsonMetrics` (the `Wolfgang.Etl.Json` meter) and `JsonLogMessages`
  (all `LoggerMessage` delegates, grouped by event-id range).
- **Language**: C# (`LangVersion` latest), `System.Text.Json` only.

## Build and Validation

### Prerequisites
- .NET 10 SDK (the test projects also need the .NET Framework 4.6.2-4.8.1 targeting packs on Windows).
- PowerShell 7+ for the scripts in `scripts/`.

### Commands
```bash
dotnet restore "ETL Json.slnx"
dotnet build "ETL Json.slnx" -c Release
dotnet test "ETL Json.slnx" -c Release
pwsh ./scripts/build-pr.ps1      # what pr.yaml runs: build, all-TFM tests, coverage gate, DevSkim, gitleaks
pwsh ./scripts/format.ps1        # dotnet format
```

### Target frameworks
- `src/Wolfgang.Etl.Json`: `net462;net481;netstandard2.0;net5.0;net6.0;net7.0;net8.0;net10.0`.
  Old-TFM code paths use `#if NETSTANDARD2_0 || NET462 || NET481`.
- Test projects: `net462` through `net10.0` (13 TFMs). Keep `<TargetFrameworks>` on a single line; CI greps it.

### Gates that fail the build or the PR
- Warnings are errors; analyzers (NetAnalyzers, Roslynator, AsyncFixer, VS Threading, BannedApiAnalyzers,
  Meziantou, SonarAnalyzer, PublicApiAnalyzers) are configured in `.editorconfig` and `Directory.Build.props`.
- All I/O is async; synchronous stream APIs are banned through `BannedSymbols.txt`.
- Coverage: production code and test assemblies have separate floors (`CODECOV_MINIMUM` and
  `CODECOV_TEST_MINIMUM` in `pr.yaml`, mirrored by `build-pr.ps1`).
- Public API: every public change must be recorded in `src/Wolfgang.Etl.Json/PublicAPI.Unshipped.txt`
  (TFM-invariant surface) or in `src/Wolfgang.Etl.Json/PublicApi/{modern,legacy}/PublicAPI.Unshipped.txt`
  (members that differ by TFM, such as covariant record `<Clone>$` returns).
- Package validation compares against `PackageValidationBaselineVersion` in the src csproj.
- Every PR that changes `src/` adds one fragment in `changelog/unreleased/` (see its README) or carries
  the `no-changelog` label. `CHANGELOG.md` is assembled at release time, not edited by hand.

## Project Layout

| Path | Contents |
|------|----------|
| `src/Wolfgang.Etl.Json/` | The library |
| `tests/Wolfgang.Etl.Json.Tests.Unit/` | Unit tests; stage tests inherit the TestKit.Xunit contract bases |
| `tests/Wolfgang.Etl.Json.Tests.Integration/` | File-system and encoding integration tests |
| `tests/Wolfgang.Etl.Json.Tests.Docs/` | Compiles every `<example><code>` block in the XML docs |
| `tests/Wolfgang.Etl.Json.Tests.SourceLink/` | Checks the PDB's SourceLink against GitHub |
| `benchmarks/Wolfgang.Etl.Json.Benchmarks/` | BenchmarkDotNet benchmarks (`pr-benchmarks.yaml` gates regressions) |
| `examples/` | Runnable examples and the Native AOT smoke app (`aot-smoke.yaml`) |
| `docs/` | Hand-written guides and ADRs (`docs/adr/`) |
| `docfx_project/` | DocFX site published to GitHub Pages |
| `changelog/unreleased/` | Changelog fragments |

## Conventions

- **Configuration goes through the options records** passed to the constructor
  (`new JsonLineExtractor<T>(stream, new JsonLineExtractorOptions { ... })`). The obsolete property setters
  and the `(source, JsonSerializerOptions?, ILogger?)` constructors remain for compatibility only.
- **Two constructor families per stage**: reflection-based (annotated `[RequiresUnreferencedCode]` /
  `[RequiresDynamicCode]`) and source-generated (`JsonTypeInfo<T>`, AOT-safe). Keep both in step.
- **Test injection**: each stage has an `internal` constructor taking an `IProgressTimer`;
  `InternalsVisibleTo` exposes it to the unit tests.
- **Counting**: call `IncrementCurrentItemCount()` exactly once per yielded or loaded item.
- **Style**: Allman braces, three blank lines between members, file-scoped namespaces, `var` when the type
  is obvious, multi-line argument lists with the parentheses on their own lines.
- **Tests**: `MethodUnderTest_when_condition_expected_result` naming, real types rather than mocks,
  `record` test models for value equality.

## Protected Files

`.editorconfig`, `Directory.Build.props`, `Directory.Build.targets`, `BannedSymbols.txt`, `*.globalconfig`,
`*.ruleset` and `.github/workflows/*` are guarded by `protected-files.yaml`. Change them in a separate,
single-purpose PR.

## Agent Guidelines

- Trust these instructions; search the code only when something here is missing or proves wrong.
- Run `scripts/build-pr.ps1` (or at least build + test on `net10.0` and `net462`) before proposing a change.
- Do not edit `PublicAPI.Shipped.txt` files except during a release.
