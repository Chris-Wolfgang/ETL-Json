type: fix

`JsonSingleStreamExtractor` now reads and deserializes one array element at a time, so an element past `MaximumItemCount` is never parsed and a malformed element no longer fails the run before the items ahead of it are delivered.
