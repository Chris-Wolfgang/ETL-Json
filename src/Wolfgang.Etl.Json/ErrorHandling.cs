namespace Wolfgang.Etl.Json;

/// <summary>Controls how deserialization errors are handled during extraction.</summary>
public enum ErrorHandling
{
    /// <summary>Throw on the first deserialization error (default).</summary>
    Throw,

    /// <summary>
    /// Capture the error in <c>Errors</c> and continue processing remaining records where possible.
    /// For streaming formats (e.g. a JSON array), a parse error mid-stream may prevent continuation.
    /// </summary>
    CaptureAndContinue,

    /// <summary>Skip the error silently, logging a warning if a logger is configured.</summary>
    SkipAndLog,
}
