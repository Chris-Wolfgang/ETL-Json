# ADR-006 AssemblyVersion pinned at 1.0.0.0

- **Status**: Accepted
- **Date**: 2026-11-17

---

## Context

`Wolfgang.Etl.Json` targets `net462` and `net481`, so it runs on the .NET Framework CLR, which binds a reference by its full four-part `AssemblyVersion`. The package `<Version>` moves on every release. If `AssemblyVersion` moved with it, every .NET Framework consumer would need a binding redirect on every upgrade. .NET 5 and later ignore the value for binding.

The project file already carries the pin and a comment that explains it; there was no ADR. Separately, the ETL family has settled a policy (recorded in ETL-Abstractions as "`<AssemblyVersion>` policy for the ETL family"): a new library that targets .NET Framework derives `0.{Minor}.0.0` during `0.x`, and a library that already ships pinned at `1.0.0.0` stays pinned, because moving it to a `0.x` value would be a numeric downgrade that breaks the redirects consumers already have.

---

## Decision

We pin `<AssemblyVersion>` at `1.0.0.0` in `src/Wolfgang.Etl.Json/Wolfgang.Etl.Json.csproj`. Before 1.0 it stays there even across the breaking changes a `0.x` minor is allowed to make; the package `<Version>` and Package Validation convey those breaks. `<FileVersion>` is derived from `<Version>` and the informational version follows it, so those two carry the real release number. The pin is to be revisited at 1.0.

---

## Considered Options

### Option A: Let the SDK derive AssemblyVersion from `<Version>`

- Pro: One number to change per release.
- Con: A different binding identity on every release, and a redirect on every upgrade for .NET Framework consumers.

### Option B: Derive `0.{Minor}.0.0`, as the family policy prescribes for new libraries

- Pro: A breaking `0.x` minor changes the binding identity, so a .NET Framework consumer gets a load-time signal.
- Con: Not available to a library already published at `1.0.0.0`. The change is a downgrade.

### Option C: Keep the pin at 1.0.0.0 (chosen)

- Pro: No binding redirects across patch and minor releases.
- Con: A breaking `0.x` minor gives a .NET Framework consumer no bind-time signal.

---

## Consequences

**Easier:**

- .NET Framework consumers upgrade across patch and minor releases with no binding redirect.

**Harder:**

- A consumer who swaps in a breaking `0.x` release without recompiling finds out at run time, not at load time.
- The `1.0.0.0` identity is already in use, so the real `1.0.0` release will not change it unless something else is decided. The family policy leaves that question open; it needs an ADR here before 1.0.

*Recorded after the fact: the pin was already in force when this ADR was written.*
