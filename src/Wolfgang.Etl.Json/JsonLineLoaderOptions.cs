using System;
using System.Text.Json;
using System.Text;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Construction-time configuration for <see cref="JsonLineLoader{TRecord}"/> (ADR-0009).
/// </summary>
/// <remarks>
/// Carries the settings specific to this stage together with the ones every loader shares (inherited from <see cref="LoaderOptions"/>), so one object configures the whole stage.
/// <see cref="SerializerOptions"/> configures the reflection-based serializer. The source-generated path takes a
/// <c>JsonTypeInfo&lt;TRecord&gt;</c> as its own constructor parameter instead, because the type info carries its own
/// serializer options; a record that sets <see cref="SerializerOptions"/> is rejected by those constructors rather
/// than silently ignored.
/// </remarks>
public sealed record JsonLineLoaderOptions : LoaderOptions
{    /// <summary>
    /// Gets the text encoding used to write the stream. <see langword="null"/> (the default) writes UTF-8 without a byte-order mark.
    /// </summary>
    public Encoding? Encoding { get; init; }



    /// <summary>
    /// Gets a value indicating whether the loader runs without writing: the source is enumerated and counted, but nothing reaches the destination. Defaults to <see langword="false"/>.
    /// </summary>
    public bool IsDryRun { get; init; }



    /// <summary>
    /// Gets the <see cref="JsonSerializerOptions"/> for the reflection-based serializer. <see langword="null"/> (the default)
    /// uses the serializer's defaults. Not combinable with a source-generated <c>JsonTypeInfo&lt;TRecord&gt;</c>.
    /// </summary>
    public JsonSerializerOptions? SerializerOptions { get; init; }
}
