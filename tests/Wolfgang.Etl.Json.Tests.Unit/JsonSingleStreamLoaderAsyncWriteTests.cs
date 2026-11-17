using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// <see cref="JsonSingleStreamLoader{TRecord}"/> must write only through <c>WriteAsync</c> (a destination such
/// as an ASP.NET Core response body forbids synchronous I/O) and must batch small records into larger writes.
/// </summary>
public class JsonSingleStreamLoaderAsyncWriteTests
{
    [Fact]
    public async Task LoadAsync_when_destination_forbids_synchronous_io_writes_the_array()
    {
        using var stream = new AsyncOnlyStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>(stream);

        await sut.LoadAsync(People(3).ToAsyncEnumerable());

        var written = JsonSerializer.Deserialize<List<PersonRecord>>(stream.ToArray());
        Assert.Equal(People(3), written);
    }



    [Fact]
    public async Task LoadAsync_batches_small_records_into_few_writes()
    {
        using var stream = new AsyncOnlyStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>(stream);

        await sut.LoadAsync(People(5000).ToAsyncEnumerable());

        Assert.True(stream.AsyncWrites < 100, $"expected batched writes, saw {stream.AsyncWrites}");
        Assert.Equal(5000, JsonSerializer.Deserialize<List<PersonRecord>>(stream.ToArray())!.Count);
    }



    [Fact]
    public async Task LoadAsync_when_dry_run_writes_nothing()
    {
        using var stream = new AsyncOnlyStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>(stream, new JsonSingleStreamLoaderOptions { IsDryRun = true });

        await sut.LoadAsync(People(3).ToAsyncEnumerable());

        Assert.Equal(0, stream.AsyncWrites);
    }



    [Fact]
    public void ByteBufferWriter_when_advanced_past_capacity_throws()
    {
        var sut = new ByteBufferWriter();

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.Advance(sut.Buffer.Length + 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => sut.Advance(-1));
    }



    [Fact]
    public void ByteBufferWriter_grows_for_a_large_size_hint_and_keeps_written_bytes()
    {
        var sut = new ByteBufferWriter();
        sut.GetSpan(1)[0] = 7;
        sut.Advance(1);

        var memory = sut.GetMemory(100_000);
        var span = sut.GetSpan(100_000);

        Assert.True(memory.Length >= 100_000);
        Assert.True(span.Length >= 100_000);
        Assert.Equal(7, sut.Buffer[0]);
        Assert.Equal(1, sut.WrittenCount);
        sut.Clear();
        Assert.Equal(0, sut.WrittenCount);
    }



    private static List<PersonRecord> People(int count) =>
        Enumerable.Range(0, count).Select(i => new PersonRecord { FirstName = "F" + i, LastName = "L", Age = i }).ToList();



    private sealed class AsyncOnlyStream : Stream
    {
        private readonly MemoryStream _inner = new();



        public int AsyncWrites { get; private set; }



        public byte[] ToArray() => _inner.ToArray();



        public override bool CanRead => false;



        public override bool CanSeek => false;



        public override bool CanWrite => true;



        public override long Length => throw new NotSupportedException();



        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }



        public override void Write(byte[] buffer, int offset, int count) =>
            throw new InvalidOperationException("Synchronous operations are disallowed.");



        public override void Flush() =>
            throw new InvalidOperationException("Synchronous operations are disallowed.");



        public override Task FlushAsync(CancellationToken cancellationToken) => Task.CompletedTask;



        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            AsyncWrites++;
            _inner.Write(buffer, offset, count);
            return Task.CompletedTask;
        }



        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();



        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();



        public override void SetLength(long value) => throw new NotSupportedException();
    }
}
