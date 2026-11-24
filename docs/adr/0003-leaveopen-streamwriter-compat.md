# ADR-0003 `#if` Guards in CreateStreamWriter for leaveOpen Compat

- **Status**: Superseded — StreamWriter removed (2026-07-16)
- **Date**: 2026-07-16

---

## Context

`JsonLineLoader` needed to write to a `Stream` it does not own, meaning `StreamWriter` had to be constructed with `leaveOpen: true` so it would not close the caller's stream on dispose. The 2-arg `new StreamWriter(stream, leaveOpen: true)` constructor was introduced in .NET 6 and was not available on net462, netstandard2.0, or net481. A cross-TFM strategy was required.

---

## Decision (original)

A `#if` preprocessor guard in `CreateStreamWriter` called different constructors depending on the target framework. Modern TFMs (net6+) used the 2-arg overload. Older TFMs used the 5-arg overload, which required passing `Encoding.UTF8` — causing a UTF-8 BOM to be written on those TFMs.

---

## Update — Superseded (2026-07-16)

`JsonLineLoader` no longer uses `StreamWriter` at all. The caller passes in a `Stream` they own; wrapping it in a `StreamWriter` (which disposes the stream on its own dispose) was the wrong approach from the start.

The replacement writes directly to `_stream`:
- UTF-8 path (default): `JsonSerializer.SerializeToUtf8Bytes` + `Stream.WriteAsync`
- Custom encoding path: `JsonSerializer.Serialize` (string) + `Encoding.GetBytes` + `Stream.WriteAsync`

This eliminates:
- `leaveOpen: true` (no wrapper to close the stream)
- The `#if` guard (no constructor variation needed)
- The UTF-8 BOM (no `Encoding.UTF8` reference)
- `CreateStreamWriter` method
- The `TrimStart('﻿')` workaround in snapshot tests
- `CA1849`/`AsyncFixer02` suppressions (writes are now genuinely async)

Note: the explicit `FlushAsync` call was later reinstated in `JsonLineLoader.CompleteLoadingAsync` so a caller inspecting the stream before disposing it sees every line.
