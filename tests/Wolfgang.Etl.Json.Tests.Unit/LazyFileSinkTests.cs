using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// The path-based fluent sink terminators must not create, truncate or lock the output file while the
/// pipeline is being composed; the file is created when the run writes, and closed when the run ends.
/// </summary>
public class LazyFileSinkTests
{
    private static readonly PersonRecord[] People =
    [
        new() { FirstName = "Alice", LastName = "Smith", Age = 30 },
    ];



    [Fact]
    public void JsonLineLoader_path_terminator_when_built_but_not_run_leaves_the_existing_file_untouched_and_unlocked()
    {
        var path = Path.GetTempFileName();
        try
        {
            File.WriteAllText(path, "precious");

            _ = EtlPipeline.Create().From(People.ToAsyncEnumerable()).JsonLineLoader(path);

            Assert.Equal("precious", File.ReadAllText(path));
            using var exclusive = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            Assert.True(exclusive.CanWrite);
        }
        finally
        {
            File.Delete(path);
        }
    }



    [Fact]
    public void JsonSingleStreamLoader_path_terminator_when_built_but_not_run_leaves_the_existing_file_untouched_and_unlocked()
    {
        var path = Path.GetTempFileName();
        try
        {
            File.WriteAllText(path, "precious");

            _ = EtlPipeline.Create().From(People.ToAsyncEnumerable()).JsonSingleStreamLoader(path);

            Assert.Equal("precious", File.ReadAllText(path));
            using var exclusive = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            Assert.True(exclusive.CanWrite);
        }
        finally
        {
            File.Delete(path);
        }
    }



    [Fact]
    public async Task JsonLineLoader_path_terminator_when_run_with_no_items_creates_an_empty_file_and_closes_it()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".jsonl");
        try
        {
            await EtlPipeline.Create().From(Array.Empty<PersonRecord>().ToAsyncEnumerable()).JsonLineLoader(path).RunAsync();

            Assert.Equal(0, new FileInfo(path).Length);
            using var exclusive = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            Assert.True(exclusive.CanWrite);
        }
        finally
        {
            File.Delete(path);
        }
    }



    [Fact]
    public async Task JsonSingleStreamLoader_path_terminator_when_run_writes_the_array_and_closes_the_file()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
        try
        {
            await EtlPipeline.Create().From(People.ToAsyncEnumerable()).JsonSingleStreamLoader(path).RunAsync();

            var readBack = new List<PersonRecord>();
            using (var stream = File.OpenRead(path))
            {
                await foreach (var p in new JsonSingleStreamExtractor<PersonRecord>(stream).ExtractAsync())
                {
                    readBack.Add(p);
                }
            }

            Assert.Equal(People, readBack);
        }
        finally
        {
            File.Delete(path);
        }
    }



    [Fact]
    public async Task LazyFileStream_when_disposed_before_use_never_creates_the_file()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".tmp");
        var sut = new LazyFileStream(path);

        Assert.False(sut.IsOpen);
        sut.Dispose();

        Assert.False(File.Exists(path));
        await Task.CompletedTask;
    }



    [Fact]
    public async Task LazyFileStream_async_write_and_flush_create_the_file_and_unsupported_members_throw()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".tmp");
        try
        {
            using (var sut = new LazyFileStream(path))
            {
                await sut.WriteAsync(new byte[] { 1, 2, 3 }, 0, 3);
                await sut.FlushAsync();

                Assert.True(sut.IsOpen);
                Assert.True(sut.CanWrite);
                Assert.False(sut.CanRead);
                Assert.False(sut.CanSeek);
                Assert.Throws<NotSupportedException>(() => sut.Write(new byte[1], 0, 1));
                Assert.Throws<NotSupportedException>(() => sut.Flush());
                Assert.Throws<NotSupportedException>(() => sut.Length);
                Assert.Throws<NotSupportedException>(() => sut.Position);
                Assert.Throws<NotSupportedException>(() => sut.Position = 0);
                Assert.Throws<NotSupportedException>(() => sut.Read(new byte[1], 0, 1));
                Assert.Throws<NotSupportedException>(() => sut.Seek(0, SeekOrigin.Begin));
                Assert.Throws<NotSupportedException>(() => sut.SetLength(0));
            }

            Assert.Equal(3, new FileInfo(path).Length);
        }
        finally
        {
            File.Delete(path);
        }
    }



    [Fact]
    public void LazyFileStream_when_path_is_null_throws()
    {
        Assert.Throws<ArgumentNullException>(() => new LazyFileStream(null!));
    }
}
