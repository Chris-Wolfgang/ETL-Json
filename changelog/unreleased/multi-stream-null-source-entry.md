type: fix

`JsonMultiStreamExtractor` now throws `InvalidOperationException` naming the index when a supplied `JsonNamedStream` entry is null, instead of a `NullReferenceException`.
