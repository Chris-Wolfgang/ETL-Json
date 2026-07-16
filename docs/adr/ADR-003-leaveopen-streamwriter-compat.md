# ADR-003 `#if` Guards in CreateStreamWriter for leaveOpen Compat

- **Status**: Accepted
- **Date**: 2026-07-16
- **Updated**: 2026-07-16 (BOM issue resolved — see Update section)

---

## Context

`JsonLineLoader` needs to write to a `Stream` it does not own, meaning `StreamWriter` must be constructed with `leaveOpen: true` so it does not close the caller's stream on dispose. The convenient `new StreamWriter(stream, leaveOpen: true)` constructor was introduced in .NET 6 and is not available on net462, netstandard2.0, or net481. A cross-TFM strategy is required.

---

## Decision

We use a `#if` preprocessor guard in `CreateStreamWriter` to call different constructors depending on the target framework. Modern TFMs (net6+) use the clean `leaveOpen: true` two-argument overload. Older TFMs use the five-argument overload with `new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)` to suppress the BOM preamble.

---

## Considered Options

### Option A: Always use the 5-arg overload

- Pro: Compiles on all TFMs without `#if`.
- Con: Forces a magic buffer size even on modern TFMs where a cleaner API exists.

### Option B: `#if` guard (chosen)

- Pro: Modern TFMs (net6+) use the clean 2-arg constructor — no buffer size, no encoding ceremony.
- Pro: Older TFMs compile and run correctly using the 5-arg form.
- Con: Slightly more code to maintain across the `#if` boundary.

---

## Consequences

**Easier:**

- All TFMs produce BOM-free JSON output.
- The library compiles and runs correctly across the full TFM matrix without runtime fallbacks.

**Harder:**

- The `#if` guard must be maintained when TFM targets change.

---

## Update — BOM issue resolved (2026-07-16)

The original draft of this ADR noted that the 5-arg constructor path (older TFMs) passed `System.Text.Encoding.UTF8`, which writes a UTF-8 BOM preamble. This caused observable output differences and required `TrimStart('﻿')` workarounds in the snapshot test suite.

**Fix (issue #227):** The 5-arg path now passes `new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)`, which is available on all TFMs including net462 and netstandard2.0, and produces BOM-free output. The `TrimStart` workaround has been removed. Both `#if` branches now produce identical, BOM-free output.
