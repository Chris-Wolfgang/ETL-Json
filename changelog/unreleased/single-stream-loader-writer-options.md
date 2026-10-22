type: fix

`JsonSingleStreamLoader` now honours `WriteIndented`, `IndentCharacter`, `IndentSize`, `NewLine`, `Encoder` and `MaxDepth` from the serializer options or `JsonTypeInfo`, matching the other loaders; previously its output was always compact with default escaping.
