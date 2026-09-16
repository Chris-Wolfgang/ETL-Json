using System;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Construction-time configuration for <see cref="JsonSingleStreamExtractor{TRecord}"/> (ADR-0009).
/// </summary>
/// <remarks>
/// This stage has no settings of its own; the record exists so the settings every extractor shares (inherited from <see cref="ExtractorOptions"/>) are configured the same way as on every other stage.
/// Serializer configuration is not part of this record: it travels as its own constructor parameter
/// (<c>JsonSerializerOptions</c> on the reflection constructors, <c>JsonTypeInfo&lt;TRecord&gt;</c> on the
/// source-generated ones), because a <c>JsonTypeInfo</c> already carries its options and a second copy here
/// would be an inert setting on that path.
/// </remarks>
public sealed record JsonSingleStreamExtractorOptions : ExtractorOptions
{
}
