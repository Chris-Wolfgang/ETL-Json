# ADR-0004 Async-only I/O enforced through BannedSymbols.txt

- **Status**: Accepted
- **Date**: 2026-11-17

---

## Context

Every extractor and loader in `Wolfgang.Etl.Json` is a stage in an `IAsyncEnumerable<T>` pipeline. A blocking call on that path (`Task.Result`, `Thread.Sleep`, a synchronous `Stream.Write`) holds a thread-pool thread for the length of the I/O and can deadlock a consumer that runs under a single-threaded synchronization context.

This library has been on both sides of the question. [ADR-0002](0002-sync-writeline-loader-hot-path.md) chose a synchronous `StreamWriter.WriteLine` in the loader hot path to avoid a state machine per item, and was superseded when `StreamWriter` was removed. Since then nothing current has stated the rule, although the mechanism is in the repository: a `BannedSymbols.txt` at the root, read by `Microsoft.CodeAnalysis.BannedApiAnalyzers`. On the date of this ADR the list had the same 65 entries in all eight ETL repositories.

---

## Decision

All stream I/O in shipped code is asynchronous, and the banned-API analyzer enforces it.

- `BannedSymbols.txt` bans `Task.Wait`, `Task.WaitAll`, `Task.WaitAny`, `Task<T>.Result`, `GetAwaiter().GetResult()`, `Thread.Sleep`, the synchronous `File` helpers, `Stream.Read` / `Write` / `Flush` / `CopyTo`, `Parallel.For` / `ForEach` and the blocking `Console` reads. Each entry names its replacement.
- `.editorconfig` sets `RS0030` to `error` for the shipped code and to `none` for tests, benchmarks and examples.
- Where an awaited stream call per item would be too expensive, the record is serialized into memory first and the bytes are written to the stream with `WriteAsync`. The line and single-stream loaders work this way. The per-item cost ADR-0002 was avoiding is avoided without a synchronous stream call.
- The shipped code has no suppression of `RS0030` or of the async-usage analyzers (`CA1849`, `VSTHRD103`, `AsyncFixer`).

---

## Considered Options

### Option A: Rely on code review and AsyncFixer

- Pro: Nothing to maintain.
- Con: AsyncFixer catches some patterns, not all of the banned APIs, and review misses them under time pressure.

### Option B: Allow synchronous writes into a buffered writer (ADR-0002)

- Pro: No state machine per item.
- Con: Needs analyzer suppressions at every such call, and a synchronous write reaches the stream whenever the buffer fills.
- Con: Superseded in this library when `StreamWriter` was removed.

### Option C: Ban the blocking APIs with an analyzer, and buffer in memory where a per-item await is too expensive (chosen)

- Pro: A blocking call fails the build with a message that names the replacement.
- Pro: No suppressions to explain.
- Con: The list is maintained by hand and has to stay in step across the ETL repositories.

---

## Consequences

**Easier:**

- A direct blocking call cannot merge into shipped code.
- A reviewer does not have to judge whether a given synchronous call is one of the allowed ones; there are none.

**Harder:**

- A stage that buffers has to manage the buffer: when to flush it, and what to do with a half-written record when an item fails.
- The rule covers the listed members only. A synchronous member that is not on the list, such as one on `TextReader` or `TextWriter`, is caught by review or not at all.
- `BannedSymbols.txt` is a protected file, so a change to the list goes in its own pull request.

*Recorded after the fact: the rule was already in force when this ADR was written.*
