using System.Text;

namespace Wolfgang.Etl.Json;

/// <summary>
/// The byte-order marks a <see cref="System.IO.StreamReader"/> detects. Hoisted here (non-generic)
/// so the CLR shares a single table across every <c>TRecord</c> instantiation of the extractors
/// instead of allocating one per closed generic type.
/// </summary>
internal static class ByteOrderMarks
{
    /// <summary>
    /// Encodings the <see cref="System.IO.StreamReader"/> detects from a byte-order mark, longest
    /// preamble first (the UTF-32 LE mark starts with the UTF-16 LE mark).
    /// </summary>
    internal static readonly Encoding[] DetectedEncodings =
    {
        new UTF32Encoding(bigEndian: false, byteOrderMark: true),
        new UTF32Encoding(bigEndian: true, byteOrderMark: true),
        Encoding.UTF8,
        Encoding.Unicode,
        Encoding.BigEndianUnicode,
    };
}
