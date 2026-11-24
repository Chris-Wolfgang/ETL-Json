# ADR-0005 Trim and Native AOT: annotate the reflection-based overloads, offer `JsonTypeInfo<T>` overloads

- **Status**: Accepted
- **Date**: 2026-11-17

---

## Context

`System.Text.Json` serializes a type in one of two ways. With reflection it needs nothing from the caller, but it cannot be trimmed safely and it generates code at run time, so it does not work under Native AOT. With a source-generated `JsonTypeInfo<T>` it needs the caller to declare a `JsonSerializerContext`, and it works under trimming and AOT.

A consumer who publishes with `PublishTrimmed` or `PublishAot` needs to know which of this library's entry points are safe, and needs the build to tell them when they pick one that is not.

Three pieces were already in place when this ADR was written, added at different times: the annotations on the reflection-based overloads (July 2026), a publish-and-run smoke test (July 2026), and the compatibility declaration (November 2026). None had a record.

---

## Decision

We keep both serialization paths and mark which is which.

- The constructors and pipeline factories that serialize by reflection carry `[RequiresUnreferencedCode]` and `[RequiresDynamicCode]`.
- Each of the six stages has overloads that take a source-generated `JsonTypeInfo<TRecord>`. Those carry no annotation and are the trim- and AOT-safe path.
- `Directory.Build.props` sets `IsAotCompatible` and `IsTrimmable` for the projects under `src/`, on the target frameworks compatible with `net8.0`. `.editorconfig` keeps the `Trimming`, `AOT` and `SingleFile` analyzer categories at `warning`, which is an error in a Release build.
- `examples/Wolfgang.Etl.Json.Example.AotSmoke` uses the `JsonTypeInfo<T>` overloads of the single-stream, line and multi-stream stages with a `JsonSerializerContext`. The `aot-smoke.yaml` workflow publishes it with `PublishAot` and `PublishTrimmed` and runs the native binary, on every pull request.

---

## Considered Options

### Option A: Reflection-based overloads only

- Pro: The smallest public surface.
- Con: The library could not be used in a trimmed or AOT-published app.

### Option B: `JsonTypeInfo<T>` overloads only

- Pro: Every entry point is trim- and AOT-safe.
- Con: Every consumer has to write a `JsonSerializerContext`, including the many who never trim or AOT-publish.

### Option C: Both, with the reflection-based ones annotated (chosen)

- Pro: The simple call stays simple, and the safe path exists for those who need it.
- Pro: A consumer who trims or AOT-publishes gets a build warning at the exact call that is not safe.
- Con: Each stage has roughly twice the constructors, and each new entry point needs both forms.

---

## Consequences

**Easier:**

- A consumer can use the library in a trimmed or AOT-published app by passing a `JsonTypeInfo<T>`.
- A new reflection or dynamic-code use in shipped code that is not annotated fails the Release build.

**Harder:**

- The public surface is larger, and the annotations are part of it: removing one later is safe, adding one is a break for callers who treat the warning as an error.
- The smoke test is informational today. Its publish step runs with `continue-on-error`, so a trim or AOT warning there does not fail the pull request. Making it a required check is tracked in issue #444.
- The smoke test covers the `JsonTypeInfo<T>` path of three round trips. An overload it does not call is covered by the analyzers only.
- Nothing is claimed for the target frameworks below `net8.0`.

*Recorded after the fact: the annotations, the overloads, the declaration and the smoke test were already in force when this ADR was written.*
