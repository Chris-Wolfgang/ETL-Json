type: fix

The JSON loaders now apply `ErrorPolicy` to a record that fails to serialize: Skip drops it, counts it in `CurrentErrorItemCount` and leaves no partial output; the default aborts with the original exception.
