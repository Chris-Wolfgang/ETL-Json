# Session 003 — Remove TProgress Generic Parameter
**Date:** 2026-03-17
**Branch:** initial-development

## Goal
Simplify the public API by removing the `TProgress` generic parameter from all 6 classes and hardcoding `JsonReport`.

## What Was Done

### API Change
Before: `JsonSingleStreamExtractor<TRecord, TProgress> : ExtractorBase<TRecord, TProgress>`
After: `JsonSingleStreamExtractor<TRecord> : ExtractorBase<TRecord, JsonReport>`

Applied to all 6 classes:
- JsonSingleStreamExtractor
- JsonSingleStreamLoader
- JsonMultiStreamExtractor
- JsonMultiStreamLoader
- JsonLineExtractor
- JsonLineLoader

### Changes Per Class
1. Removed `TProgress` from class declaration and base class
2. Removed `where TProgress : notnull` constraint
3. Simplified `ILogger<>` type params (one fewer generic arg)
4. `CreateProgressReport()` now returns `JsonReport` directly — no runtime type check, no `NotSupportedException` branch
5. `CreateProgressTimer(IProgress<JsonReport>)` — concrete type instead of generic

### Test Updates
- All 6 test files updated: `<PersonRecord, JsonReport>` → `<PersonRecord>` for SUT references
- Contract base class inheritance still uses 3 type params: `ExtractorBaseContractTests<JsonSingleStreamExtractor<PersonRecord>, PersonRecord, JsonReport>`
- 279 tests still passing across 11 TFMs

### README Updated
- All code examples updated to use single generic param

### Commits on `initial-development`
1. `1226059` — Add Wolfgang.Etl.Json library with 3 extractor/loader pairs
2. `5116303` — Remove TProgress generic parameter, hardcode JsonReport

## Benefits
- Cleaner API: `JsonSingleStreamExtractor<Person>` instead of `JsonSingleStreamExtractor<Person, JsonReport>`
- Eliminated unreachable `NotSupportedException` defensive code
- Eliminated runtime type check in `CreateProgressReport()`
- Net reduction: 13 files changed, -60 lines
