# Session 002 — Add Missing Test Coverage
**Date:** 2026-03-17
**Branch:** setup/configure-from-template-20260316-224343

## Goal
Identify and fill test coverage gaps across all source classes.

## What Was Done

### Coverage Gaps Identified
- JsonReport: no standalone tests
- Internal constructor null validation (all 6 classes)
- CreateProgressReport NotSupportedException branch (all 6 classes)
- Null deserialization paths (MultiStreamExtractor, JsonLineExtractor)
- Empty sequence output verification (SingleStreamLoader, JsonLineLoader, MultiStreamLoader)
- CreateProgressTimer duplicate wiring guard (all 6 classes)

### Tests Added (28 new tests, 251 → 279)
- **JsonReportTests.cs** (new file) — 5 tests: constructor, properties, record equality
- **Internal constructor null validation** — 3 tests per class × 6 = 18 tests
- **Null deserialization paths** — 2 tests (MultiStreamExtractor null stream, JsonLineExtractor null line)
- **Empty sequence output** — 3 tests (SingleStreamLoader `[]`, JsonLineLoader empty, MultiStreamLoader no streams)

### Build Fix
- `string.Split('\n', StringSplitOptions.RemoveEmptyEntries)` not available on .NET Framework — changed to `string.Split(new[] { '\n' }, ...)`
- StreamWriter UTF-8 BOM on .NET Framework — changed empty sequence test to check trimmed content instead of byte length

### Coverage Results (net10.0)
- **Line coverage: 95.8%** (551/575 lines)
- **Branch coverage: 85.2%** (174/204 branches)
- **Method coverage: 100%** (38/38 methods)
- Uncovered: CreateProgressReport NotSupportedException paths + compiler-generated async state machine branches

### Packages Added
- `coverlet.collector` 8.0.1 to test project for XPlat Code Coverage

## Open Items
- CreateProgressReport NotSupportedException branches could get `[ExcludeFromCodeCoverage]` (defensive code)
- Remaining branch gaps are compiler-generated async state machine branches (expected per CLAUDE.md)
