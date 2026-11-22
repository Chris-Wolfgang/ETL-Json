using System;
using System.Collections.Generic;
using System.IO;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Streams the elements of a top-level JSON array, delivering an element before a malformed element
/// after it can fail the run.
/// </summary>
/// <typeparam name="TRecord">The element type.</typeparam>
/// <remarks>
/// <see cref="JsonSerializer.DeserializeAsyncEnumerable{TValue}(Stream, JsonTypeInfo{TValue}, CancellationToken)"/>
/// deserializes every complete element in its read buffer before it yields the first one, so a
/// malformed element that shares a buffer with an earlier one fails the run before the earlier one is
/// delivered, even when the caller would have stopped first (for example at <c>MaximumItemCount</c>).
/// <para>
/// A stream that can seek is read with <c>DeserializeAsyncEnumerable</c>, which parses a whole buffer in
/// one pass and is the fast path. If it throws a <see cref="JsonException"/>, the stream is rewound and
/// read again one element at a time, skipping the elements the fast path already delivered, so the
/// caller sees every good element before the malformed one, exactly as if the elements had been read
/// lazily. A stream that cannot seek is always read one element at a time.
/// </para>
/// <para>
/// The element-at-a-time reader locates each element with <see cref="Utf8JsonReader.TrySkip"/> and
/// deserializes only that element, so nothing past the element being returned is parsed. It reads the
/// same documents (UTF-8, optional BOM, the trailing-comma, comment and depth settings of the serializer
/// options) and, like the serializer, rejects a root that is not an array and any content after the array.
/// </para>
/// </remarks>
internal sealed class JsonArrayElementReader<TRecord>
{
    private const int MinimumBufferSize = 256;

    private static ReadOnlySpan<byte> Utf8ByteOrderMark => new byte[] { 0xEF, 0xBB, 0xBF };

    private readonly Stream _stream;
    private readonly JsonTypeInfo<TRecord> _typeInfo;
    private JsonReaderState _readerState;
    private byte[] _buffer;
    private int _start;
    private int _end;
    private bool _isFinalBlock;
    private Phase _phase;
    private int _elementsToSkip;



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
        _readerState = new JsonReaderState
        (
            new JsonReaderOptions
            {
                AllowTrailingCommas = options.AllowTrailingCommas,
                CommentHandling = options.ReadCommentHandling,
                MaxDepth = options.MaxDepth,
            }
        );
        _buffer = Array.Empty<byte>();
    }



    private enum Phase
    {
        BeforeByteOrderMark,
        BeforeArray,
        InArray,
        AfterArray,
    }



    private enum StepResult
    {
        Element,
        Progress,
        NeedMoreData,
        Completed,
    }



    /// <summary>
    /// Reads the array, yielding each element in document order.
    /// </summary>
    /// <param name="token">A token to cancel the read.</param>
    /// <returns>The elements of the array, in document order; a JSON <c>null</c> element is <see langword="null"/>.</returns>
    /// <exception cref="JsonException">The document is not a well-formed JSON array of <typeparamref name="TRecord"/>.</exception>
    internal IAsyncEnumerable<TRecord?> ReadAsync
    (
        CancellationToken token
    )
    {
        return _stream.CanSeek
            ? ReadSeekableAsync(token)
            : ReadElementwiseAsync(0, token);
    }



    /// <summary>
    /// Reads a seekable stream with the serializer's single-pass enumerable, and on a
    /// <see cref="JsonException"/> rewinds and re-reads one element at a time.
    /// </summary>
    private async IAsyncEnumerable<TRecord?> ReadSeekableAsync
    (
        [EnumeratorCancellation] CancellationToken token
    )
    {
        var origin = _stream.Position;
        var delivered = 0;

        var enumerator = JsonSerializer
            .DeserializeAsyncEnumerable(_stream, _typeInfo, token)
            .GetAsyncEnumerator(token);
        try
        {
            while (true)
            {
                bool hasNext;
                try
                {
                    hasNext = await enumerator.MoveNextAsync().ConfigureAwait(false);
                }
                catch (JsonException)
                {
                    break;
                }

                if (!hasNext)
                {
                    yield break;
                }

                delivered++;
                yield return enumerator.Current;
            }
        }
        finally
        {
            await enumerator.DisposeAsync().ConfigureAwait(false);
        }

        // The serializer parses a whole buffer before it yields from it, so a malformed element
        // may have hidden the good elements before it. Read again, lazily, to deliver them.
        _stream.Position = origin;
        await foreach (var item in ReadElementwiseAsync(delivered, token).ConfigureAwait(false))
        {
            yield return item;
        }
    }



    /// <summary>
    /// Reads the array one element at a time, discarding the first <paramref name="elementsToSkip"/>.
    /// </summary>
    private async IAsyncEnumerable<TRecord?> ReadElementwiseAsync
    (
        int elementsToSkip,
        [EnumeratorCancellation] CancellationToken token
    )
    {
        _elementsToSkip = elementsToSkip;
        _buffer = ArrayPool<byte>.Shared.Rent(Math.Max(MinimumBufferSize, _typeInfo.Options.DefaultBufferSize));
        try
        {
            while (true)
            {
                var result = StepOrThrowJsonException(out var item);
                switch (result)
                {
                    case StepResult.Element:
                        yield return item;
                        break;
                    case StepResult.NeedMoreData:
                        await FillBufferAsync(token).ConfigureAwait(false);
                        break;
                    case StepResult.Completed:
                        yield break;
                    default:
                        // StepResult.Progress: a token was consumed; take the next step.
                        break;
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
    /// Runs <see cref="Step"/>, rethrowing a syntax error from <see cref="Utf8JsonReader"/> (an internal
    /// <see cref="JsonException"/> subtype) as a plain <see cref="JsonException"/>, as the serializer does.
    /// </summary>
    /// <exception cref="JsonException">The document is not a well-formed JSON array of <typeparamref name="TRecord"/>.</exception>
    private StepResult StepOrThrowJsonException(out TRecord? item)
    {
        try
        {
            return Step(out item);
        }
        catch (JsonException ex) when (ex.GetType() != typeof(JsonException))
        {
            throw new JsonException(ex.Message, ex.Path, ex.LineNumber, ex.BytePositionInLine, ex);
        }
    }



    /// <summary>
    /// Advances by at most one token or one element over the buffered bytes. A step that runs out of
    /// data consumes nothing, so it is repeated from the same position once more data is buffered.
    /// </summary>
    /// <exception cref="JsonException">The buffered bytes are not part of a well-formed JSON array of <typeparamref name="TRecord"/>.</exception>
    private StepResult Step(out TRecord? item)
    {
        item = default;

        if (_phase == Phase.BeforeByteOrderMark)
        {
            return SkipByteOrderMark();
        }

        var reader = new Utf8JsonReader
        (
            new ReadOnlySpan<byte>(_buffer, _start, _end - _start),
            _isFinalBlock,
            _readerState
        );

        if (!reader.Read())
        {
            return _isFinalBlock && _phase == Phase.AfterArray
                ? StepResult.Completed
                : StepResult.NeedMoreData;
        }

        switch (_phase)
        {
            case Phase.BeforeArray:
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException
                    (
                        $"The JSON value could not be converted to an array of {typeof(TRecord)}: the root token is {reader.TokenType}, not StartArray."
                    );
                }

                _phase = Phase.InArray;
                Commit(ref reader);
                return StepResult.Progress;

            case Phase.InArray:
                return StepInArray(ref reader, out item);

            default:
                // Utf8JsonReader rejects a second top-level value itself; reaching this means it did not.
                throw new JsonException("The JSON array is followed by additional content.");
        }
    }



    private StepResult StepInArray(ref Utf8JsonReader reader, out TRecord? item)
    {
        item = default;

        if (reader.TokenType == JsonTokenType.EndArray)
        {
            _phase = Phase.AfterArray;
            Commit(ref reader);
            return StepResult.Progress;
        }

        var elementStart = (int)reader.TokenStartIndex;
        if (!reader.TrySkip())
        {
            return StepResult.NeedMoreData;
        }

        if (_elementsToSkip > 0)
        {
            _elementsToSkip--;
            Commit(ref reader);
            return StepResult.Progress;
        }

        var elementLength = (int)reader.BytesConsumed - elementStart;
        item = JsonSerializer.Deserialize
        (
            new ReadOnlySpan<byte>(_buffer, _start + elementStart, elementLength),
            _typeInfo
        );
        Commit(ref reader);
        return StepResult.Element;
    }



    private StepResult SkipByteOrderMark()
    {
        var buffered = _end - _start;
        if (buffered < Utf8ByteOrderMark.Length && !_isFinalBlock)
        {
            return StepResult.NeedMoreData;
        }

        if (buffered >= Utf8ByteOrderMark.Length
            && new ReadOnlySpan<byte>(_buffer, _start, Utf8ByteOrderMark.Length).SequenceEqual(Utf8ByteOrderMark))
        {
            _start += Utf8ByteOrderMark.Length;
        }

        _phase = Phase.BeforeArray;
        return StepResult.Progress;
    }



    private void Commit(ref Utf8JsonReader reader)
    {
        _start += (int)reader.BytesConsumed;
        _readerState = reader.CurrentState;
    }



    /// <summary>
    /// Moves the unconsumed bytes to the front of the buffer, grows it when it is full, and reads more
    /// of the stream. A read of zero bytes marks the final block.
    /// </summary>
    /// <exception cref="JsonException">More data is needed after the final block was read.</exception>
    private async Task FillBufferAsync(CancellationToken token)
    {
        if (_isFinalBlock)
        {
            // Utf8JsonReader throws on an incomplete final block, so this is unreachable for valid input.
            throw new JsonException("The JSON input ended before the array was complete.");
        }

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
