type: fix

The extractors and loaders no longer reach reflection-based `System.Text.Json` from their worker code: a stage built with a `JsonTypeInfo<T>` is now fully trim- and native-AOT-safe (the AOT smoke publish reports 0 IL2026/IL3050, down from 6 + 6), and the remaining `JsonNamedStream` / `JsonNamedDestination` constructors of `JsonMultiStreamExtractor` and `JsonMultiStreamLoader` that serialize by reflection now carry `[RequiresUnreferencedCode]` / `[RequiresDynamicCode]` like every other reflection-based constructor.
