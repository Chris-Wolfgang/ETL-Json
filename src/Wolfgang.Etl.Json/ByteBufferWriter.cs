using System;
using System.Buffers;

namespace Wolfgang.Etl.Json;



/// <summary>
/// A growable in-memory <see cref="IBufferWriter{T}"/> over a <c>byte[]</c>. A stage hands it to a
/// <c>Utf8JsonWriter</c> so serialization never touches the destination stream, then writes the
/// accumulated bytes with an asynchronous <c>WriteAsync</c>. Exists because
/// <c>ArrayBufferWriter&lt;T&gt;</c> is not available on net462 / netstandard2.0.
/// </summary>
internal sealed class ByteBufferWriter : IBufferWriter<byte>
{
    private const int DefaultCapacity = 4096;

    private byte[] _buffer = new byte[DefaultCapacity];



    /// <summary>Gets the backing array; only the first <see cref="WrittenCount"/> bytes are meaningful.</summary>
    internal byte[] Buffer => _buffer;



    /// <summary>Gets the number of bytes written since the last <see cref="Clear"/>.</summary>
    internal int WrittenCount { get; private set; }



    /// <summary>Discards the written bytes, keeping the capacity.</summary>
    internal void Clear() => WrittenCount = 0;



    /// <inheritdoc />
    public void Advance(int count)
    {
        if (count < 0 || WrittenCount > _buffer.Length - count)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        WrittenCount += count;
    }



    /// <inheritdoc />
    public Memory<byte> GetMemory(int sizeHint = 0)
    {
        EnsureCapacity(sizeHint);
        return _buffer.AsMemory(WrittenCount);
    }



    /// <inheritdoc />
    public Span<byte> GetSpan(int sizeHint = 0)
    {
        EnsureCapacity(sizeHint);
        return _buffer.AsSpan(WrittenCount);
    }



    private void EnsureCapacity(int sizeHint)
    {
        var needed = Math.Max(sizeHint, 1);
        if (_buffer.Length - WrittenCount >= needed)
        {
            return;
        }

        var newSize = Math.Max(_buffer.Length * 2, WrittenCount + needed);
        Array.Resize(ref _buffer, newSize);
    }
}
