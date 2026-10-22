type: fix

`JsonMultiStreamExtractor` rejects a `JsonNamedStream` carrying a null stream with an `InvalidOperationException` naming the source (was a `NullReferenceException`); `JsonSingleStreamExtractor` logs a Warning when a skip or dead-letter policy ends the run early at a malformed element instead of logging normal completion; `JsonLineLoader` flushes the stream before completing.
