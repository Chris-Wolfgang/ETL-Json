# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

> Targets the next release (`0.2.1`). Library public API and runtime behavior
> are unchanged from `0.2.0`; this round is canonical CI/docs/metadata work plus
> a binding-stability fix.

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

[Unreleased]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v0.2.0...HEAD
[0.2.0]: https://github.com/Chris-Wolfgang/ETL-Json/compare/v.0.1.0...v0.2.0
[0.1.0]: https://github.com/Chris-Wolfgang/ETL-Json/releases/tag/v.0.1.0
