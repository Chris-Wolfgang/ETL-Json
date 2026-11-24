# Architecture Decision Records

| Number | Title | Status | Date |
|--------|-------|--------|------|
| [0000](0000-record-architecture-decisions.md) | Record architecture decisions | Accepted | 2026-11-20 |
| [0001](0001-system-text-json.md) | System.Text.Json over Newtonsoft.Json | Accepted | 2026-07-16 |
| [0002](0002-sync-writeline-loader-hot-path.md) | Sync StreamWriter.WriteLine in loader hot paths | Superseded (StreamWriter removed) | 2026-07-16 |
| [0003](0003-leaveopen-streamwriter-compat.md) | `#if` guards in CreateStreamWriter for leaveOpen compat | Superseded (StreamWriter removed) | 2026-07-16 |
| [0004](0004-async-only-io-via-banned-symbols.md) | Async-only I/O enforced through BannedSymbols.txt | Accepted | 2026-11-17 |
| [0005](0005-trim-and-native-aot.md) | Trim and Native AOT: annotate the reflection-based overloads, offer `JsonTypeInfo<T>` overloads | Accepted | 2026-11-17 |
| [0006](0006-assemblyversion-pinned.md) | AssemblyVersion pinned at 1.0.0.0 | Accepted | 2026-11-17 |
| 0007 | Record architecture decisions (moved to [0000](0000-record-architecture-decisions.md)) | Retired | 2026-11-23 |

---

To add a new ADR, copy [TEMPLATE.md](TEMPLATE.md), assign the next number, and add a row to this table.
