# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

### Changed

### Deprecated

### Removed

### Fixed

### Security

## [0.8.1] - 2026-08-22

Patch release. **Zero source or public-API changes** — consumers of `Wolfgang.Etl.Json`
0.8.1 receive byte-identical assemblies to 0.8.0. The delta is entirely maintainer-facing
code-scanning hygiene.

### Security

- **Scorecard SARIF layer suppression.** `.github/workflows/scorecard.yaml` now filters
  `results.sarif` before upload, dropping five whole rules (`DangerousWorkflowID`,
  `BranchProtectionID`, `CodeReviewID`, `CIIBestPracticesID`, `FuzzingID`) and the
  `nugetCommand` / `pipCommand` sub-checks of `PinnedDependenciesID` that are structurally
  won't-fix on this repo. `gitHubAction` pin regressions still fire — the filter is scoped to
  structurally-not-actionable findings. Applies the pattern from
  Chris-Wolfgang/Extensions-Logging-Data#188; brings open Scorecard alerts from 23 down to
  1 (SASTID, kept as real signal). Refs #267.
- **zizmor `dangerous-triggers` waiver.** New `.github/zizmor.yml` waives
  `dangerous-triggers` on `pr.yaml`, where `pull_request_target` is intentional (see the
  `pr.yaml` SECURITY NOTE header — workflow YAML runs from trusted `main`, PR head is scan
  input only). Auto-discovered by zizmor 1.5.2 per the fleet standard. Also updated
  `workflow-security.yaml`'s path trigger from the ignored root `.zizmor.yml` to the
  discovered `.github/zizmor.yml` so edits actually re-run zizmor.

## [0.8.0] - 2026-08-17

Minor release. Two consumer-visible changes: (1) `JsonSerializerOptions` is now optional on every
public stream/source constructor so a logger can be supplied without also building an
`JsonSerializerOptions`; (2) the ETL core dependency floor moves to `Wolfgang.Etl.Abstractions`
0.23.2. The rest of the release is repo hygiene — code-scanning cleanup that killed the ~600-alert
InspectCode flood tracked in #267, plus workflow-security hardening.

### Added

- Optional `options` parameter on the public `(stream / source, JsonSerializerOptions, ILogger?)`
  constructors of every extractor and loader. `null` now means "use the `System.Text.Json`
  serializer default" (matches the framework's own `JsonSerializer.Serialize<T>` /
  `Deserialize<T>` convention). Consumers can now write, for example,
  `new JsonSingleStreamLoader<Person>(stream, logger: myLogger)` instead of
  `new JsonSingleStreamLoader<Person>(stream, new JsonSerializerOptions(), myLogger)`.
  The stored `_options` field was already nullable and every downstream `JsonSerializer.*` call
  site already handled it, so no other behaviour changes. Closes #266.

### Changed

- Adopted **ETL core 0.23.2** — `Wolfgang.Etl.Abstractions` / `.TestKit` / `.TestKit.Xunit` /
  `.ErrorPolicies` all bumped 0.22.0 -> 0.23.2. The Abstractions 0.23.2 dependency floor
  (`Microsoft.Bcl.AsyncInterfaces >= 10.0.5`) is already satisfied by the existing 10.0.10 pins,
  so no transitive cascade.
- Hoisted per-`TRecord`-instantiation constants (`_operationTag`, `_componentTag`,
  `_newLineUtf8`, `DefaultOptions`) from every generic `Json*Extractor` / `Json*Loader<T>` up to
  non-generic `JsonMetrics`. The CLR now allocates each constant once instead of duplicating it
  per closed generic type. Internal refactor; no consumer-visible change.

### Fixed

- **Fleet-wide fix — `PublicApiAnalyzer` false-positive flood.** `Directory.Build.props` now
  gates the `Microsoft.CodeAnalysis.PublicApiAnalyzers` `PackageReference` on
  `Exists('PublicAPI.Shipped.txt')`. Without this gate the analyzer loaded in every project
  (src, tests, benchmarks, examples) and emitted `RS0016` / `RS0037` / `RS0036` for every public
  member in projects without PublicAPI tracking, producing ~524 false-positive alerts. Zero
  behaviour change for src/ projects that already track PublicAPI. Closes #267.
- **`PublicAPI.Shipped.txt` catch-up.** Fourteen members that shipped in 0.7.0 but were never
  tracked (record `init` setters + `override ToString` / `EqualityContract` on
  `JsonNamedStream` / `JsonNamedDestination` / `JsonReport`, the `string!`-path constructors on
  `JsonSingleStreamExtractor` / `JsonLineExtractor`, and the checkpointing surface on
  `JsonLineExtractor`) plus nullability annotations on 40 pre-existing entries. Also closes #254.

### Security

- `aot-smoke.yaml` migrated from `pull_request_target` to `pull_request` (safer default for a
  workflow that only builds and runs example code). Added a top-level `permissions: contents: read`
  block.
- `pr-benchmarks.yaml` moved its `pull-requests: write` grant from workflow-level down to the
  single job that needs it, and now sinks `github.event.pull_request.*.sha` into env vars before
  `git checkout` to eliminate the template-injection surface.
- `codeql.yaml`'s "Complete Security Scan" step sinks four `steps.*.outcome` template expansions
  into a step-level `env:` block.
- `scorecard.yaml` replaced `permissions: read-all` with the explicit `permissions: contents: read`.
- Hash-pinned `pip install zizmor==1.5.2` in `workflow-security.yaml` via a new
  `.github/workflows/zizmor-requirements.txt` listing every 1.5.2 wheel + sdist SHA-256 from PyPI.

Not addressed in this release (intentional / deferred): the `pull_request_target` trigger on
`pr.yaml` (deliberate — see its 15-line security-note; carries the protected-file guard for
`.editorconfig` / `Directory.Build.props` / `BannedSymbols.txt` / workflow files), the
`dotnet restore` `PinnedDependenciesID` alerts (needs a fleet-wide `packages.lock.json` +
`--locked-mode` initiative), and the `contents: write` grants on `benchmarks` / `docfx` / `release`
(legitimate — required to publish to `gh-pages` and upload release assets).

## [0.7.0] - 2026-08-13

### Changed

- Adopted **ETL core 0.22.0** — `Wolfgang.Etl.Abstractions` 0.21.0 -> 0.22.0, along with the
  test-only `Wolfgang.Etl.TestKit` / `Wolfgang.Etl.TestKit.Xunit` references. 0.22.0 is the release in
  which the TestKit packages were folded into the ETL-Abstractions repository and now build and ship
  from there. The public API of all four core packages is unchanged.
- Inherited from Abstractions 0.22.0: the `await foreach` sites in `ExtractorBase` and
  `TransformerBase` now use `ConfigureAwait(false)`, removing a sync-over-async deadlock risk for
  consumers on the `net462` and `netstandard2.0` targets that drive the pipeline from a
  synchronization context. No behavioural change on the modern targets.

## [0.6.0] - 2026-08-10

Minor release (pre-1.0 breaking): adopts the per-item error-handling model from
`Wolfgang.Etl.Abstractions` 0.21+ and retires the parallel local error surface, so a failed record is
handled and counted consistently with every other stage in a pipeline. Validates against the 0.5.0
baseline with the intentional removals waived in `CompatibilitySuppressions.xml`.

### Added

- The extractors now inherit the base **`ErrorPolicy`** property (from `Wolfgang.Etl.Abstractions`
  0.21+): assign a `Func<ItemErrorContext, ItemErrorAction>` to skip, log, and/or dead-letter records
  that fail to deserialize. Ready-made policies live in the new **`Wolfgang.Etl.ErrorPolicies`** package
  (`ItemErrorPolicy.Skip` / `Abort` / `SkipAndLog(logger)` / `SkipAndDeadLetter(...)` /
  `SkipDeadLetterAndLog(..., logger)`, the dead-letter factories overloaded for an
  `ICollection<ItemErrorContext>` or a `System.Threading.Channels.ChannelWriter<ItemErrorContext>`).
  Unset is fail-fast; failed records flow through the base `HandleItemError` and are counted by
  `CurrentErrorItemCount`, so a skip surfaces in the pipeline's aggregate like every other stage.

### Changed

- **Breaking:** upgraded to `Wolfgang.Etl.Abstractions` 0.21.0 (the base `ErrorPolicy` +
  `Wolfgang.Etl.ErrorPolicies` 0.21.0) and `Wolfgang.Etl.TestKit` 0.14.0.

### Removed

- **Breaking:** the local `ErrorHandling` enum, the `Errors` collection on each extractor, and the
  `JsonDeserializationError` type — a parallel error mechanism that did not report through the
  pipeline. Replaced by the inherited base `ErrorPolicy` hook + the shared `ItemErrorPolicy` factory
  (capture the raw content and exception via a dead-letter policy). Breaking is acceptable pre-1.0; the
  base names keep one vocabulary across the ETL family.

## [0.5.0] - 2026-07-22

### Added

- Fluent `EtlPipeline` JSON factories, composing on the generic pipeline in
  `Wolfgang.Etl.Abstractions` 0.16.0 so JSON reads/writes flow through cross-format pipelines —
  `EtlPipeline.Create().JsonLineExtractor<Person>("in.jsonl").JsonLineLoader<Person>("out.jsonl").RunAsync()`.
  Source factories `JsonLineExtractor<T>` / `JsonSingleStreamExtractor<T>` (extension methods on
  `EtlPipeline`) and sink terminators `JsonLineLoader<T>` / `JsonSingleStreamLoader<T>` (extension
  methods on `IEtlPipeline<T>`), each with path, `Stream`, and optional `JsonSerializerOptions`
  overloads. Path-based factories own the file stream they open and dispose it when the run
  completes; stream-based overloads leave the stream's lifetime to the caller. Closes #62.
- Path-based constructors on `JsonLineExtractor<TRecord>` and `JsonSingleStreamExtractor<TRecord>`
  (`(string path, JsonSerializerOptions? = null, ILogger? = null)`) that open and own the file,
  closing it when extraction completes or the extractor is disposed.
- Checkpoint/resume support on `JsonLineExtractor<TRecord>`: new `EnableCheckpointing` (settable,
  default `false`), `StartByteOffset` (settable) and `CurrentByteOffset` (read-only) properties. Set
  `EnableCheckpointing` to `true`, then capture `CurrentByteOffset` after each yielded item to record
  a byte-position checkpoint; set `StartByteOffset` on a fresh extractor to resume from that position.
  Byte tracking is opt-in because it adds per-line overhead on the extraction hot path — with
  `EnableCheckpointing` left `false` (the default) there is no cost, and reading `CurrentByteOffset`
  throws `InvalidOperationException`. Resuming via `StartByteOffset` does not itself require the flag.
  The stream must be seekable when `StartByteOffset` is greater than zero. Line endings (`\n` vs
  `\r\n`) are detected automatically. Closes #16.
- Built-in OpenTelemetry metrics via `System.Diagnostics.Metrics`. All extractors and loaders emit
  counters (`wolfgang.etl.json.items.extracted`, `.items.loaded`, `.items.skipped`) and an operation
  duration histogram (`wolfgang.etl.json.operation.duration`) under the `Wolfgang.Etl.Json` meter,
  tagged with `etl.operation`, `etl.component`, and `etl.record_type`. Closes #14.

### Changed

- Bumped the `Wolfgang.Etl.Abstractions` dependency from 0.15.0 to 0.16.0, required by the fluent
  `EtlPipeline` JSON factories.

## [0.4.0] - 2026-07-15

### Changed

- `JsonLineLoader<TRecord>` now writes directly to the output stream instead of wrapping it in a
  `StreamWriter`, avoiding an unnecessary buffering layer.

### Fixed

- Suppressed the UTF-8 byte-order mark (BOM) emitted by `JsonLineLoader<TRecord>` on older target
  frameworks so JSONL output is byte-identical across all TFMs.

## [0.3.0] - 2026-07-13

### Added

- `JsonLineExtractor<TRecord>.Encoding` and `JsonLineLoader<TRecord>.Encoding` settable properties
  (`System.Text.Encoding?`) that control the character encoding used when reading or writing the
  JSONL stream. When `null` (the default), existing behavior is preserved: the extractor infers
  encoding from the stream's BOM (falling back to UTF-8) and the loader writes UTF-8. Closes #12.
- `ISupportDryRun` implemented on `JsonLineLoader<TRecord>`, `JsonSingleStreamLoader<TRecord>`,
  and `JsonMultiStreamLoader<TRecord>`. When `IsDryRun` is `true`, the loader runs the full
  pipeline (enumeration, `SkipItemCount`/`MaximumItemCount`, progress counters, logging) but
  skips all writes to output stream(s). `JsonMultiStreamLoader` additionally skips calling the
  stream factory. Closes #178.
- `JsonNamedStream` record: pairs a `Stream` with an optional name for use with `JsonMultiStreamExtractor`.
- `JsonNamedDestination` record: pairs a `Stream` with an optional name for use with `JsonMultiStreamLoader`.
- `JsonMultiStreamExtractor<TRecord>` now accepts `IEnumerable<JsonNamedStream>` sources; the current source name surfaces in `JsonReport.CurrentSourceName` during progress reporting.
- `JsonMultiStreamLoader<TRecord>` now accepts `Func<TRecord, JsonNamedDestination>` factory; the current destination name surfaces in `JsonReport.CurrentSourceName` during progress reporting.
- `JsonReport.CurrentSourceName` property: the name of the stream currently being processed (`null` when not supplied or for non-multi-stream operations).
- `JsonReport(int, int, string?)` constructor overload exposing `currentSourceName`.

### Performance

- On the `ErrorHandling.Throw` path in `JsonLineExtractor` and `JsonMultiStreamExtractor`,
  the `JsonDeserializationError` object is no longer allocated before re-throwing the original
  `JsonException` (the object was constructed but never used on that path).

## [0.2.2] - 2026-07-06

### Changed

- Dependabot bump: dotnet-dependencies group (7 packages).

## [0.2.1] - 2026-06-26

> Library public API is unchanged from `0.2.0`. This release is canonical
> CI/docs/metadata work plus a binding-stability fix and an async-context fix.

### Added

- `Microsoft.CodeAnalysis.PublicApiAnalyzers` with a `PublicAPI.Shipped.txt`
  baseline for breaking-change detection.
- Canonical NuGet package metadata: SourceLink, deterministic CI builds, and
  `.snupkg` symbol packages.
- CodeQL `security-extended` query pack, and benchmark and mutation-testing
  (Stryker) workflows.

### Changed

- SHA-pinned all GitHub-owned Actions to commit hashes (least-privilege
  `permissions:` blocks already in place).
- Consolidated `<Nullable>` and analyzer package references into
  `Directory.Build.props`.
- Documentation site now uses an inline meta-refresh root page plus the in-page
  version picker.

### Fixed

- Pinned `<AssemblyVersion>` to `1.0.0.0` (with prerelease-safe `<FileVersion>`)
  for .NET Framework binding stability.
- `scripts/build-pr.ps1` now pipes the gitleaks tarball to `tar -xz -f -` to
  avoid a silent CI hang on GNU tar's default `/dev/tape`.
- `JsonSingleStreamExtractor` now suppresses the synchronization context on its
  `await foreach` (`ConfigureAwait(false)`), avoiding a potential deadlock for
  consumers on the `net462` / `netstandard2.0` targets.

### Removed

- Post-setup bootstrap files that were one-time template carry-overs.

## [0.2.0] - 2026-04-27

### Changed

- `ILogger<T>` is now optional rather than required on all extractors and loaders.
- Bumped to `0.2.0` and upgraded the `Wolfgang.Etl.Abstractions` dependency.

### Fixed

- Made the `JsonSerializerOptions` constructor parameter nullable and fixed a
  progress-timer race condition.

## [0.1.0] - 2026-03-24

### Added

- Initial release of `Wolfgang.Etl.Json`: `JsonSingleStreamExtractor` /
  `JsonSingleStreamLoader`, `JsonMultiStreamExtractor` / `JsonMultiStreamLoader`,
  and `JsonLineExtractor` / `JsonLineLoader`, built on
  `Wolfgang.Etl.Abstractions`.
- `JsonTypeInfo<TRecord>` constructors for source-generated, AOT-friendly
  serialization.

### Performance

- Cached the default `JsonSerializerOptions` and log operation-name strings as
  static fields; sealed the extractor and loader classes.

[Unreleased]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.8.1...HEAD
[0.8.1]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.8.0...v0.8.1
[0.5.0]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.4.0...v0.5.0
[0.4.0]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.3.0...v0.4.0
[0.3.0]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.2.2...v0.3.0
[0.2.2]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.2.1...v0.2.2
[0.2.1]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.2.0...v0.2.1
[0.2.0]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.1.0...v0.2.0
[0.1.0]: https://github.com/Chris-Wolfgang/ETL-Json/releases/tag/v0.1.0
