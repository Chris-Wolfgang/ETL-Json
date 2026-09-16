using System;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Construction-time configuration for <see cref="JsonMultiStreamLoader{TRecord}"/> (ADR-0009).
/// </summary>
/// <remarks>
/// Carries the settings specific to this stage together with the ones every loader shares (inherited from <see cref="LoaderOptions"/>), so one object configures the whole stage.
/// Serializer configuration is not part of this record: it travels as its own constructor parameter
/// (<c>JsonSerializerOptions</c> on the reflection constructors, <c>JsonTypeInfo&lt;TRecord&gt;</c> on the
/// source-generated ones), because a <c>JsonTypeInfo</c> already carries its options and a second copy here
/// would be an inert setting on that path.
/// </remarks>
public sealed record JsonMultiStreamLoaderOptions : LoaderOptions
{    /// <summary>
    /// Gets a value indicating whether the loader runs without writing: the source is enumerated and counted, but no destination is opened or written. Defaults to <see langword="false"/>.
    /// </summary>
    public bool IsDryRun { get; init; }
}
