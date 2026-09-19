using System.Text.Json;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Construction-time configuration for <see cref="JsonSingleStreamExtractor{TRecord}"/> (ADR-0009).
/// </summary>
/// <remarks>
/// This stage has no settings of its own; the record exists so the settings every extractor shares (inherited from <see cref="ExtractorOptions"/>) are configured the same way as on every other stage.
/// <see cref="SerializerOptions"/> configures the reflection-based deserializer. The source-generated path takes a
/// <c>JsonTypeInfo&lt;TRecord&gt;</c> as its own constructor parameter instead, because the type info carries its own
/// serializer options; a record that sets <see cref="SerializerOptions"/> is rejected by those constructors rather
/// than silently ignored.
/// </remarks>
public sealed record JsonSingleStreamExtractorOptions : ExtractorOptions
{
    /// <summary>
    /// Gets the <see cref="JsonSerializerOptions"/> for the reflection-based deserializer. <see langword="null"/> (the default)
    /// uses the serializer's defaults. Not combinable with a source-generated <c>JsonTypeInfo&lt;TRecord&gt;</c>.
    /// </summary>
    public JsonSerializerOptions? SerializerOptions { get; init; }
}
