using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Wolfgang.Etl.Json;



/// <summary>
/// A write-only stream over a file that is created (and truncated) on the first write or flush, not when the
/// object is constructed. The path-based fluent sink terminators use it so that composing a pipeline neither
/// destroys an existing output file nor holds a handle on it; the file is touched only when the run starts
/// writing. The file is opened for asynchronous I/O and closed by <see cref="Stream.Dispose()"/>.
/// </summary>
internal sealed class LazyFileStream : Stream
{
    private const int BufferSize = 4096;

    private readonly string _path;
    private FileStream? _file;



    /// <summary>Initializes a new instance that will create <paramref name="path"/> on first use.</summary>
    /// <param name="path">The path of the file to create.</param>
    internal LazyFileStream(string path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }



    /// <summary>Gets a value indicating whether the file has been created yet.</summary>
    internal bool IsOpen => _file is not null;



    /// <inheritdoc />
    public override bool CanRead => false;



    /// <inheritdoc />
    public override bool CanSeek => false;



    /// <inheritdoc />
    public override bool CanWrite => true;



    /// <inheritdoc />
    public override long Length => throw new NotSupportedException();



    /// <inheritdoc />
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }



    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException("This stream is asynchronous only; use WriteAsync.");



    /// <inheritdoc />
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
        Open().WriteAsync(buffer, offset, count, cancellationToken);



    /// <inheritdoc />
    public override void Flush() =>
        throw new NotSupportedException("This stream is asynchronous only; use FlushAsync.");



    /// <inheritdoc />
    public override Task FlushAsync(CancellationToken cancellationToken) => Open().FlushAsync(cancellationToken);



    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();



    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();



    /// <inheritdoc />
    public override void SetLength(long value) => throw new NotSupportedException();



    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _file?.Dispose();
        }

        base.Dispose(disposing);
    }



    private FileStream Open() =>
        _file ??= new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None, BufferSize, FileOptions.Asynchronous);
}
