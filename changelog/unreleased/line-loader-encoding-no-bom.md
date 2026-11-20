type: docs

Documented that `JsonLineLoader` never writes a byte-order mark, so a non-UTF-8 file must be read with the same `Encoding` on `JsonLineExtractorOptions`.
