# ADR-001 System.Text.Json over Newtonsoft.Json

- **Status**: Accepted
- **Date**: 2026-07-16

---

## Context

ETL-Json needs a JSON serialization library for its extractors and loaders. The library targets a wide TFM range: net462, netstandard2.0, netstandard2.1, net8.0, and net10.0. A choice must be made between the two dominant .NET JSON libraries. High-throughput ETL workloads make performance and allocation overhead meaningful factors.

---

## Decision

We will use System.Text.Json for all JSON serialization and deserialization.

---

## Considered Options

### Option A: Newtonsoft.Json

- Pro: Mature, battle-tested, and widely understood.
- Pro: Flexible — supports dynamic types, BSON, and complex contract resolvers.
- Con: Adds a NuGet transitive dependency that all consumers inherit.
- Con: Not AOT-compatible without significant extra configuration.
- Con: Measurably slower and higher-allocating than System.Text.Json for high-throughput scenarios.

### Option B: System.Text.Json

- Pro: Available on net462+ via a NuGet polyfill; inbox on net5+ with zero extra dependency.
- Pro: First-class AOT support via `JsonTypeInfo<T>` source-generated constructors.
- Pro: Significantly faster and lower-allocating than Newtonsoft in benchmark comparisons.
- Pro: No transitive dependency added to consumer projects on net5+.
- Con: Consumers must use `[JsonPropertyName]` and related attributes instead of Newtonsoft conventions.
- Con: No BSON or dynamic-type support — acceptable for this library's use cases.

---

## Consequences

**Easier:**

- AOT deployment scenarios are supported without additional configuration.
- Consumer projects on net5+ gain no new transitive dependencies.
- High-throughput workloads benefit from lower allocations and faster serialization.

**Harder:**

- Consumers migrating from Newtonsoft must replace `[JsonProperty]` with `[JsonPropertyName]` and similar attribute swaps.
- Dynamic/BSON scenarios are not supported and must be handled outside this library.
