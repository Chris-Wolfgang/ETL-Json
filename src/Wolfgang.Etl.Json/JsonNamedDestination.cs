using System.IO;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Associates a <see cref="Stream"/> with an optional human-readable name for progress reporting.
/// </summary>
/// <param name="Stream">The stream to load into.</param>
/// <param name="Name">
/// An optional name identifying the stream (e.g. a file path or blob key).
/// Surfaced in <see cref="JsonReport.CurrentSourceName"/> during loading.
/// </param>
public sealed record JsonNamedDestination(Stream Stream, string? Name = null);
