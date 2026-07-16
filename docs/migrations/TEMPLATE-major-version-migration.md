# Migrating from vX.Y to vA.B

## Prerequisites

Install the new NuGet package:

```
dotnet add package Wolfgang.Etl.Json --version A.B.0
```

---

## Breaking Changes

| API | Before | After | Reason |
|-----|--------|-------|--------|
| `ExampleType.OldMethod()` | `void OldMethod()` | `Task OldMethodAsync()` | Async I/O requirement |
| _(add rows as needed)_ | | | |

---

## Deprecations

| Deprecated API | Replacement | Removed In |
|----------------|-------------|------------|
| `ExampleType.OldProperty` | `ExampleType.NewProperty` | vB.0 |
| _(add rows as needed)_ | | |

---

## Before / After Code Samples

_(Maintainer: add representative before/after snippets for each breaking change.)_

### Example: [Change name]

**Before (vX.Y):**

```csharp
// TODO
```

**After (vA.B):**

```csharp
// TODO
```

---

## Notable Behavior Changes

- _(List any changes to observable behavior that are not API-level breaks — e.g. BOM handling, encoding defaults, error messages.)_

---

## Deprecation Timeline

| Version | Action |
|---------|--------|
| vA.B | Deprecated APIs marked with `[Obsolete]` |
| vB.0 | Deprecated APIs removed |

---

> **Reminder for maintainers**: Link this guide in the GitHub Release notes for the vA.B release.
