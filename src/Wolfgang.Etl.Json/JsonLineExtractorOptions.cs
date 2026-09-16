using System;
using System.Text;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Construction-time configuration for <see cref="JsonLineExtractor{TRecord}"/> (ADR-0009).
/// </summary>
/// <remarks>
/// Carries the settings specific to this stage together with the ones every extractor shares (inherited from <see cref="ExtractorOptions"/>), so one object configures the whole stage.
/// Serializer configuration is not part of this record: it travels as its own constructor parameter
/// (<c>JsonSerializerOptions</c> on the reflection constructors, <c>JsonTypeInfo&lt;TRecord&gt;</c> on the
/// source-generated ones), because a <c>JsonTypeInfo</c> already carries its options and a second copy here
/// would be an inert setting on that path.
/// </remarks>
public sealed record JsonLineExtractorOptions : ExtractorOptions
{    /// <summary>
    /// Gets the text encoding used to decode the stream. <see langword="null"/> (the default) detects the encoding from a byte-order mark and falls back to UTF-8.
    /// </summary>
    public Encoding? Encoding { get; init; }



    /// <summary>
    /// Gets a value indicating whether byte offsets are tracked so extraction can resume from <see cref="JsonLineExtractor{TRecord}.CurrentByteOffset"/>. Defaults to <see langword="false"/>.
    /// </summary>
    public bool EnableCheckpointing { get; init; }



    /// <summary>
    /// Gets the byte offset to seek to before extraction starts. Defaults to <c>0</c>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">The value is negative.</exception>
    public long StartByteOffset
    {
        get;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "StartByteOffset must be zero or greater.");
            }

            field = value;
        }
    }
}
