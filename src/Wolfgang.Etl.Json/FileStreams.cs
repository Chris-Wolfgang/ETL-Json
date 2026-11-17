using System.IO;

namespace Wolfgang.Etl.Json;



/// <summary>
/// Opens the files the stages own, with asynchronous I/O enabled so the stages' <c>ReadAsync</c> /
/// <c>WriteAsync</c> calls are genuinely asynchronous instead of blocking a thread-pool thread.
/// </summary>
internal static class FileStreams
{
    private const int BufferSize = 4096;



    /// <summary>Opens <paramref name="path"/> for sequential asynchronous reading.</summary>
    /// <param name="path">The path of the file to read.</param>
    internal static FileStream OpenRead(string path) =>
        new FileStream
        (
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            BufferSize,
            FileOptions.Asynchronous | FileOptions.SequentialScan
        );
}
