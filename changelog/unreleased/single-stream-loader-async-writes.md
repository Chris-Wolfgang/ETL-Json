type: fix

`JsonSingleStreamLoader` now writes to the destination only through `WriteAsync`, batched, so it works on streams that forbid synchronous I/O and no longer issues one blocking write per record.
