type: fix

`JsonLineExtractor.CurrentByteOffset` is now an absolute stream position for a stream the caller already advanced, and never passes the end of the stream for a final line without a terminator.
