namespace Wolfgang.Etl.Json;

/// <summary>Controls how deserialization errors are handled during extraction.</summary>
public enum ErrorHandling
{
    /// <summary>Throw on the first deserialization error (default).</summary>
    Throw,

    /// <summary>Capture the error and continue processing remaining records.</summary>
    CaptureAndContinue,

    /// <summary>Skip the error silently, logging a warning if a logger is configured.</summary>
    SkipAndLog,
}
