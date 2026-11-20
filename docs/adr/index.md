# Architecture Decision Records

| Number | Title | Status | Date |
|--------|-------|--------|------|
| [ADR-001](ADR-001-system-text-json.md) | System.Text.Json over Newtonsoft.Json | Accepted | 2026-07-16 |
| [ADR-002](ADR-002-sync-writeline-loader-hot-path.md) | Sync StreamWriter.WriteLine in loader hot paths | Superseded (StreamWriter removed) | 2026-07-16 |
| [ADR-003](ADR-003-leaveopen-streamwriter-compat.md) | `#if` guards in CreateStreamWriter for leaveOpen compat | Superseded (StreamWriter removed) | 2026-07-16 |
| [ADR-004](ADR-004-async-only-io-via-banned-symbols.md) | Async-only I/O enforced through BannedSymbols.txt | Accepted | 2026-11-17 |
| [ADR-005](ADR-005-trim-and-native-aot.md) | Trim and Native AOT: annotate the reflection-based overloads, offer `JsonTypeInfo<T>` overloads | Accepted | 2026-11-17 |
| [ADR-006](ADR-006-assemblyversion-pinned.md) | AssemblyVersion pinned at 1.0.0.0 | Accepted | 2026-11-17 |
| [ADR-007](ADR-007-record-architecture-decisions.md) | Record architecture decisions | Accepted | 2026-11-17 |

---

To add a new ADR, copy [TEMPLATE.md](TEMPLATE.md), assign the next number, and add a row to this table.
