using System;
using System.Buffers;

namespace Wolfgang.Etl.Json;



/// <summary>
/// A growable in-memory <see cref="IBufferWriter{T}"/> over a <c>byte[]</c>. A stage hands it to a
/// <c>Utf8JsonWriter</c> so serialization never touches the destination stream, then writes the
/// accumulated bytes with an asynchronous <c>WriteAsync</c>. Exists because
/// <c>ArrayBufferWriter&lt;T&gt;</c> is not available on net462 / netstandard2.0. The backing array comes from
/// <see cref="ArrayPool{T}.Shared"/> so a run does not allocate (and discard) a growing series of arrays; dispose
/// it to return the array.
/// </summary>
internal sealed class ByteBufferWriter : IBufferWriter<byte>, IDisposable
{
    private const int DefaultCapacity = 4096;

    private byte[] _buffer = ArrayPool<byte>.Shared.Rent(DefaultCapacity);



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
        var bigger = ArrayPool<byte>.Shared.Rent(newSize);
        Array.Copy(_buffer, bigger, WrittenCount);
        ArrayPool<byte>.Shared.Return(_buffer);
        _buffer = bigger;
    }



    /// <summary>Returns the backing array to the pool.</summary>
    public void Dispose()
    {
        ArrayPool<byte>.Shared.Return(_buffer);
        _buffer = Array.Empty<byte>();
        WrittenCount = 0;
    }
}
