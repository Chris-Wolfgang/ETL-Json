type: fix

`JsonLineExtractor` checkpoint offsets (`CurrentByteOffset`) now count the byte-order mark, use the encoding detected from it, and detect CRLF line endings past the first 256 bytes, so resuming from `StartByteOffset` no longer starts mid-line on BOM-prefixed, UTF-16/UTF-32 or long-line CRLF files.
