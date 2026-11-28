using System;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// Boundary behaviour of the internal <see cref="ByteBufferWriter"/>: <see cref="ByteBufferWriter.Advance"/>
/// accepts exactly the free space, and <see cref="ByteBufferWriter.GetSpan"/> grows only when the free space is
/// smaller than the size hint, and then to at least the hint.
/// </summary>
public class ByteBufferWriterTests
{
    [Fact]
    public void Advance_when_count_is_zero_does_not_throw()
    {
        using var sut = new ByteBufferWriter();

        sut.Advance(0);

        Assert.Equal(0, sut.WrittenCount);
    }



    [Fact]
    public void Advance_when_count_equals_the_free_space_fills_the_buffer()
    {
        using var sut = new ByteBufferWriter();
        var capacity = sut.Buffer.Length;

        sut.Advance(capacity);

        Assert.Equal(capacity, sut.WrittenCount);
    }



    [Fact]
    public void Advance_when_count_exceeds_the_free_space_throws_ArgumentOutOfRangeException()
    {
        using var sut = new ByteBufferWriter();
        var capacity = sut.Buffer.Length;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Advance(capacity + 1));

        Assert.Equal("count", ex.ParamName);
    }



    [Fact]
    public void GetSpan_when_size_hint_equals_the_free_space_keeps_the_backing_array()
    {
        using var sut = new ByteBufferWriter();
        var before = sut.Buffer;

        var span = sut.GetSpan(before.Length);

        Assert.Same(before, sut.Buffer);
        Assert.Equal(before.Length, span.Length);
    }



    [Fact]
    public void GetSpan_when_size_hint_is_more_than_double_the_capacity_returns_at_least_the_hint()
    {
        using var sut = new ByteBufferWriter();
        var sizeHint = sut.Buffer.Length * 3;

        var span = sut.GetSpan(sizeHint);

        Assert.True
        (
            span.Length >= sizeHint,
            $"GetSpan({sizeHint}) returned {span.Length} bytes"
        );
    }



    [Fact]
    public void GetSpan_when_buffer_grows_keeps_the_written_bytes()
    {
        using var sut = new ByteBufferWriter();
        var capacity = sut.Buffer.Length;
        new byte[] { 7, 8, 9 }.CopyTo(sut.GetSpan(3));
        sut.Advance(3);

        sut.GetSpan(capacity);

        Assert.Equal
        (
            new byte[] { 7, 8, 9 },
            sut.Buffer.AsSpan(0, sut.WrittenCount).ToArray()
        );
    }
}
