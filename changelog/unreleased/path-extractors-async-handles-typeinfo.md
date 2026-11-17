type: feature

Path-based `JsonLineExtractor` and `JsonSingleStreamExtractor` constructors no longer leak the file handle when the options record is null, open the file for asynchronous I/O, and gain `(string path, JsonTypeInfo<T> typeInfo, ...)` overloads for source-generated, trim-safe use.
