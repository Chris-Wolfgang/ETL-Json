using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Streams the elements of a top-level JSON array one at a time, deserializing an element only when
/// the caller asks for it.
/// </summary>
/// <typeparam name="TRecord">The element type.</typeparam>
/// <remarks>
/// <see cref="JsonSerializer.DeserializeAsyncEnumerable{TValue}(Stream, JsonTypeInfo{TValue}, CancellationToken)"/>
/// deserializes every complete element in its read buffer before it yields the first one, so a
/// malformed element that shares a buffer with an earlier one fails the run before the earlier one is
/// delivered, even when the caller would have stopped first (for example at <c>MaximumItemCount</c>).
/// This reader walks the array structure itself (the brackets, the commas and, when the serializer
/// options skip comments, the comments), measures each element with a byte scan, and hands only that
/// element's bytes to the serializer, so nothing past the element being returned is parsed and the
/// element's own syntax and shape are still validated by the serializer. It reads the same documents
/// (UTF-8, optional BOM, the trailing-comma, comment and depth settings of the serializer options) and,
/// like the serializer, rejects a root that is not an array and any content after the array.
/// </remarks>
internal sealed class JsonArrayElementReader<TRecord>
{
    private const int MinimumBufferSize = 256;
    private const int DefaultMaxDepth = 64;
    private const int NeedMoreData = -1;
    private const int EndOfInput = -2;

    private static ReadOnlySpan<byte> Utf8ByteOrderMark => new byte[] { 0xEF, 0xBB, 0xBF };

    // The bytes that matter while measuring an object or array: a string, a bracket, or a comment.
    private static ReadOnlySpan<byte> StructuralBytes => new[] { (byte)'"', (byte)'{', (byte)'}', (byte)'[', (byte)']' };

    private static ReadOnlySpan<byte> StructuralBytesWithComments =>
        new[] { (byte)'"', (byte)'{', (byte)'}', (byte)'[', (byte)']', (byte)'/' };

    // The bytes that end a number, true, false or null.
    private static ReadOnlySpan<byte> LiteralEndBytes =>
        new[] { (byte)',', (byte)']', (byte)'}', (byte)' ', (byte)'\t', (byte)'\n', (byte)'\r', (byte)'/' };

    private static ReadOnlySpan<byte> StringBytes => new[] { (byte)'"', (byte)'\\' };

    private readonly Stream _stream;
    private readonly JsonTypeInfo<TRecord> _typeInfo;
    private readonly bool _allowTrailingCommas;
    private readonly bool _skipComments;
    private readonly int _maxDepth;
    private byte[] _buffer;
    private int _start;
    private int _end;
    private bool _isFinalBlock;
    private Phase _phase;



    /// <summary>
    /// Initializes a new instance of the <see cref="JsonArrayElementReader{TRecord}"/> class.
    /// </summary>
    /// <param name="stream">The UTF-8 JSON stream to read.</param>
    /// <param name="typeInfo">The serialization contract for the elements.</param>
    internal JsonArrayElementReader
    (
        Stream stream,
        JsonTypeInfo<TRecord> typeInfo
    )
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));

        var options = typeInfo.Options;
        _allowTrailingCommas = options.AllowTrailingCommas;
        _skipComments = options.ReadCommentHandling == JsonCommentHandling.Skip;
        _maxDepth = options.MaxDepth > 0 ? options.MaxDepth : DefaultMaxDepth;
        _buffer = ArrayPool<byte>.Shared.Rent(Math.Max(MinimumBufferSize, options.DefaultBufferSize));
    }



    private enum Phase
    {
        BeforeByteOrderMark,
        BeforeArray,
        AfterOpeningBracket,
        AfterComma,
        AfterElement,
        AfterArray,
    }



    /// <summary>
    /// Reads the array, yielding each element as it is deserialized.
    /// </summary>
    /// <param name="token">A token to cancel the read.</param>
    /// <returns>The elements of the array, in document order; a JSON <c>null</c> element is <see langword="null"/>.</returns>
    /// <exception cref="JsonException">The document is not a well-formed JSON array of <typeparamref name="TRecord"/>.</exception>
    internal async IAsyncEnumerable<TRecord?> ReadAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        try
        {
            while (true)
            {
                if (TryReadNext(out var item, out var completed))
                {
                    yield return item;
                }
                else if (completed)
                {
                    yield break;
                }
                else
                {
                    await FillBufferAsync(token).ConfigureAwait(false);
                }
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(_buffer);
            _buffer = Array.Empty<byte>();
        }
    }



    /// <summary>
    /// Advances over the buffered bytes until an element is read, the array ends, or more data is needed.
    /// </summary>
    /// <param name="item">The element, when one was read.</param>
    /// <param name="completed"><see langword="true"/> when the array has ended and the input is exhausted.</param>
    /// <returns><see langword="true"/> when an element was read; otherwise <see langword="false"/>.</returns>
    /// <exception cref="JsonException">The buffered bytes are not part of a well-formed JSON array of <typeparamref name="TRecord"/>.</exception>
    private bool TryReadNext(out TRecord? item, out bool completed)
    {
        item = default;
        completed = false;

        if (_phase == Phase.BeforeByteOrderMark)
        {
            if (!TrySkipByteOrderMark())
            {
                return false;
            }
        }

        while (true)
        {
            var next = PeekNextByte();
            if (next == NeedMoreData)
            {
                return false;
            }

            if (next == EndOfInput)
            {
                completed = EndInputOrThrow();
                return false;
            }

            if (!TryConsumeSeparator((byte)next))
            {
                return TryReadElement(out item);
            }
        }
    }



    /// <summary>
    /// Handles the array punctuation that the next byte stands for.
    /// </summary>
    /// <returns><see langword="true"/> when the byte was punctuation and was consumed; <see langword="false"/> when it starts an element.</returns>
    /// <exception cref="JsonException">The byte is not valid at this position.</exception>
    private bool TryConsumeSeparator(byte next)
    {
        switch (_phase)
        {
            case Phase.BeforeArray:
                if (next != (byte)'[')
                {
                    throw new JsonException($"The JSON value could not be converted to an array of {typeof(TRecord)}: the root is not an array.");
                }

                _start++;
                _phase = Phase.AfterOpeningBracket;
                return true;

            case Phase.AfterOpeningBracket:
                return next == (byte)']' && CloseArray();

            case Phase.AfterComma:
                if (next != (byte)']')
                {
                    return false;
                }

                if (!_allowTrailingCommas)
                {
                    throw new JsonException("The JSON array has a trailing comma, which the serializer options do not allow.");
                }

                return CloseArray();

            case Phase.AfterElement:
                if (next == (byte)',')
                {
                    _start++;
                    _phase = Phase.AfterComma;
                    return true;
                }

                if (next == (byte)']')
                {
                    return CloseArray();
                }

                throw new JsonException("Expected ',' or ']' after an element of the JSON array.");

            default:
                throw new JsonException("The JSON array is followed by additional content.");
        }
    }



    private bool CloseArray()
    {
        _start++;
        _phase = Phase.AfterArray;
        return true;
    }



    /// <summary>
    /// Handles the end of the input, which is only valid after the closing bracket.
    /// </summary>
    /// <returns><see langword="true"/> when the array was complete.</returns>
    /// <exception cref="JsonException">The input ended before the array was complete.</exception>
    private bool EndInputOrThrow()
    {
        if (_phase == Phase.AfterArray)
        {
            return true;
        }

        throw new JsonException("The JSON input ended before the array was complete.");
    }



    private bool TryReadElement(out TRecord? item)
    {
        item = default;

        var length = MeasureElement();
        if (length == NeedMoreData)
        {
            return false;
        }

        item = JsonSerializer.Deserialize
        (
            new ReadOnlySpan<byte>(_buffer, _start, length),
            _typeInfo
        );
        _start += length;
        _phase = Phase.AfterElement;
        return true;
    }



    private bool TrySkipByteOrderMark()
    {
        var buffered = _end - _start;
        if (buffered < Utf8ByteOrderMark.Length && !_isFinalBlock)
        {
            return false;
        }

        if (buffered >= Utf8ByteOrderMark.Length
            && new ReadOnlySpan<byte>(_buffer, _start, Utf8ByteOrderMark.Length).SequenceEqual(Utf8ByteOrderMark))
        {
            _start += Utf8ByteOrderMark.Length;
        }

        _phase = Phase.BeforeArray;
        return true;
    }



    /// <summary>
    /// Skips white space (and comments, when the options skip them) and returns the next byte without
    /// consuming it.
    /// </summary>
    /// <returns>The byte, <see cref="NeedMoreData"/> or <see cref="EndOfInput"/>.</returns>
    /// <exception cref="JsonException">A comment is malformed.</exception>
    private int PeekNextByte()
    {
        while (true)
        {
            if (_start == _end)
            {
                return _isFinalBlock ? EndOfInput : NeedMoreData;
            }

            var current = _buffer[_start];
            if (current == (byte)' ' || current == (byte)'\n' || current == (byte)'\r' || current == (byte)'\t')
            {
                _start++;
                continue;
            }

            if (current != (byte)'/' || !_skipComments)
            {
                return current;
            }

            var commentLength = MeasureComment(_start);
            if (commentLength == NeedMoreData)
            {
                return NeedMoreData;
            }

            _start += commentLength;
        }
    }



    /// <summary>
    /// Measures the comment that starts at <paramref name="position"/>.
    /// </summary>
    /// <returns>The comment's length in bytes, or <see cref="NeedMoreData"/>.</returns>
    /// <exception cref="JsonException">The comment is malformed.</exception>
    private int MeasureComment(int position)
    {
        var span = new ReadOnlySpan<byte>(_buffer, position, _end - position);
        if (span.Length < 2)
        {
            return IncompleteOrThrow("A '/' that does not start a comment.");
        }

        if (span[1] == (byte)'/')
        {
            var lineEnd = span.Slice(2).IndexOf((byte)'\n');
            if (lineEnd >= 0)
            {
                return lineEnd + 3;
            }

            // A line comment may run to the end of the input.
            return _isFinalBlock ? span.Length : NeedMoreData;
        }

        if (span[1] != (byte)'*')
        {
            throw new JsonException("A '/' that does not start a comment.");
        }

        for (var i = 2; i < span.Length - 1; i++)
        {
            if (span[i] == (byte)'*' && span[i + 1] == (byte)'/')
            {
                return i + 2;
            }
        }

        return IncompleteOrThrow("The JSON input ended inside a comment.");
    }



    /// <summary>
    /// Measures the element that starts at the current position.
    /// </summary>
    /// <returns>The element's length in bytes, or <see cref="NeedMoreData"/>.</returns>
    /// <exception cref="JsonException">The element nests deeper than the serializer options allow.</exception>
    private int MeasureElement()
    {
        var span = new ReadOnlySpan<byte>(_buffer, _start, _end - _start);
        var first = span[0];

        if (first == (byte)'{' || first == (byte)'[')
        {
            return MeasureContainer(span);
        }

        if (first == (byte)'"')
        {
            var end = FindEndOfString(span, 1);
            return end < 0 ? IncompleteLength(span) : end;
        }

        var literalEnd = span.IndexOfAny(LiteralEndBytes);
        return literalEnd < 0 ? IncompleteLength(span) : literalEnd;
    }



    private int MeasureContainer(ReadOnlySpan<byte> span)
    {
        var searchBytes = _skipComments ? StructuralBytesWithComments : StructuralBytes;

        // The array that holds the element is the first level of nesting.
        var depth = 2;
        var position = 1;
        while (true)
        {
            var found = span.Slice(position).IndexOfAny(searchBytes);
            if (found < 0)
            {
                return IncompleteLength(span);
            }

            position += found;
            var current = span[position];
            if (current == (byte)'"')
            {
                position = FindEndOfString(span, position + 1);
                if (position < 0)
                {
                    return IncompleteLength(span);
                }
            }
            else if (current == (byte)'/')
            {
                var commentLength = MeasureComment(_start + position);
                if (commentLength == NeedMoreData)
                {
                    return NeedMoreData;
                }

                position += commentLength;
            }
            else if (current == (byte)'{' || current == (byte)'[')
            {
                depth++;
                if (depth > _maxDepth)
                {
                    throw new JsonException($"The JSON array nests deeper than the maximum depth of {_maxDepth}.");
                }

                position++;
            }
            else
            {
                depth--;
                position++;
                if (depth == 1)
                {
                    return position;
                }
            }
        }
    }



    /// <summary>
    /// Finds the end of the string whose first byte after the opening quote is at <paramref name="from"/>.
    /// </summary>
    /// <returns>The index just past the closing quote, or -1 when the string is not closed in the buffer.</returns>
    private static int FindEndOfString(ReadOnlySpan<byte> span, int from)
    {
        while (from < span.Length)
        {
            var found = span.Slice(from).IndexOfAny(StringBytes);
            if (found < 0)
            {
                return -1;
            }

            from += found;
            if (span[from] == (byte)'"')
            {
                return from + 1;
            }

            // A backslash escapes the next byte, which may itself be a quote.
            from += 2;
        }

        return -1;
    }



    /// <summary>
    /// What to do when an element runs past the buffered bytes: ask for more, or at the end of the input
    /// hand the remaining bytes to the serializer, which reports the truncated element.
    /// </summary>
    private int IncompleteLength(ReadOnlySpan<byte> span)
    {
        return _isFinalBlock ? span.Length : NeedMoreData;
    }



    private int IncompleteOrThrow(string message)
    {
        if (_isFinalBlock)
        {
            throw new JsonException(message);
        }

        return NeedMoreData;
    }



    /// <summary>
    /// Moves the unconsumed bytes to the front of the buffer, grows it when it is full, and reads more
    /// of the stream. A read of zero bytes marks the final block.
    /// </summary>
    private async Task FillBufferAsync(CancellationToken token)
    {
        var unconsumed = _end - _start;
        if (_start > 0)
        {
            Buffer.BlockCopy(_buffer, _start, _buffer, 0, unconsumed);
            _start = 0;
            _end = unconsumed;
        }

        if (_end == _buffer.Length)
        {
            var larger = ArrayPool<byte>.Shared.Rent(_buffer.Length * 2);
            Buffer.BlockCopy(_buffer, 0, larger, 0, _end);
            ArrayPool<byte>.Shared.Return(_buffer);
            _buffer = larger;
        }

#if NETSTANDARD2_0 || NET462 || NET481
        token.ThrowIfCancellationRequested();
#pragma warning disable CA2016, MA0040, S8949 // old TFM overload has no CancellationToken parameter
        var read = await _stream.ReadAsync(_buffer, _end, _buffer.Length - _end).ConfigureAwait(false);
#pragma warning restore CA2016, MA0040, S8949
#else
        var read = await _stream.ReadAsync(_buffer.AsMemory(_end, _buffer.Length - _end), token).ConfigureAwait(false);
#endif
        if (read == 0)
        {
            _isFinalBlock = true;
        }

        _end += read;
    }
}
