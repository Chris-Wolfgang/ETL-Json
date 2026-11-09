type: fix

The named-source `JsonMultiStreamExtractor` and named-destination `JsonMultiStreamLoader` constructors that serialize by reflection (no `JsonTypeInfo<T>`) now carry `[RequiresUnreferencedCode]` / `[RequiresDynamicCode]` like the other reflection-based constructors, so a trimmed or native-AOT app gets the IL2026/IL3050 warning at the call site instead of a silent runtime failure.
