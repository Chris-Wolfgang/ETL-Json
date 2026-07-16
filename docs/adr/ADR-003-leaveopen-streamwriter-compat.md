# ADR-003 `#if` Guards in CreateStreamWriter for leaveOpen Compat

- **Status**: Accepted
- **Date**: 2026-07-16

---

## Context

`JsonLineLoader` needs to write to a `Stream` it does not own, meaning `StreamWriter` must be constructed with `leaveOpen: true` so it does not close the caller's stream on dispose. The convenient `new StreamWriter(stream, leaveOpen: true)` constructor was introduced in .NET 6 and is not available on net462, netstandard2.0, or net481. A cross-TFM strategy is required.

---

## Decision

We will use a `#if` preprocessor guard in `CreateStreamWriter` to call different constructors depending on the target framework. Modern TFMs (net6+) use the clean `leaveOpen: true` two-argument overload. Older TFMs use the five-argument overload `new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true)`.

---

## Considered Options

### Option A: Always use the 5-arg overload

- Pro: Compiles on all TFMs without `#if`.
- Con: `Encoding.UTF8` on older frameworks writes a UTF-8 BOM, which is incorrect for most JSON consumers.
- Con: Forces BOM behavior even on modern TFMs where a cleaner API exists.

### Option B: `#if` guard (chosen)

- Pro: Modern TFMs (net6+) use the clean 2-arg constructor — no BOM, no magic buffer size.
- Pro: Older TFMs compile and run correctly using the 5-arg form.
- Con: Older TFMs (net462, netstandard2.0, net481) emit a UTF-8 BOM at the start of each stream — this is a documented behavior difference.
- Con: Test suite must strip the BOM (via `TrimStart('﻿')`) when comparing output across TFMs.

---

## Consequences

**Easier:**

- Consumers on net6+ get clean, BOM-free JSON output with no special handling required.
- The library compiles and runs correctly across the full TFM matrix without runtime fallbacks.

**Harder:**

- `JsonLineLoader` writes a UTF-8 BOM on net462, netstandard2.0, and net481; consumers on those TFMs should use a BOM-aware parser or set an explicit `Encoding.UTF8` (which strips the BOM on read).
- The test suite's cross-TFM comparison logic must account for the BOM; do not remove the `TrimStart` call without re-examining the full TFM test matrix.
