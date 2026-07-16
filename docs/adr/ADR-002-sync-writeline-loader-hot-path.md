# ADR-002 Sync StreamWriter.WriteLine in Loader Hot Paths

- **Status**: Accepted
- **Date**: 2026-07-16

---

## Context

`JsonLineLoader` and `JsonMultiStreamLoader` write one serialized JSON line per item inside a tight `await foreach` loop. ETL workloads can process millions of items in a single run, so per-item allocation overhead is a meaningful concern. The choice between async and sync writes affects both allocation behavior and overall throughput.

---

## Decision

We will use synchronous `writer.WriteLine(json)` inside async methods, suppressing `CA1849` and `AsyncFixer02` with `#pragma warning disable/restore` placed directly above the method.

---

## Considered Options

### Option A: `await writer.WriteLineAsync(json)`

- Pro: Fully async — consistent with async-all-the-way guidelines.
- Con: Allocates an async state machine per item (typically 64–200 bytes).
- Con: Across millions of items this produces significant GC pressure and degrades ETL throughput.

### Option B: Sync `writer.WriteLine(json)` (chosen)

- Pro: No per-item async state machine allocation.
- Pro: `StreamWriter` has a large internal buffer; sync writes do not hit the OS synchronously — they are absorbed by the buffer and flushed at dispose or explicit `Flush()`.
- Con: Requires `#pragma warning disable CA1849, AsyncFixer02` to satisfy analyzers.
- Con: The pragmas must be placed above the method, not above `[Fact]`/`[Theory]` attributes, per project conventions.

---

## Consequences

**Easier:**

- High-throughput loads run with substantially lower GC pressure.
- No per-item heap allocation from async state machines.

**Harder:**

- Two analyzer suppressions (`CA1849`, `AsyncFixer02`) are required above each affected method; these are intentional and must not be removed without revisiting the allocation trade-off.
- Reviewers unfamiliar with this ADR may flag the suppressions; this document serves as the authoritative explanation.
