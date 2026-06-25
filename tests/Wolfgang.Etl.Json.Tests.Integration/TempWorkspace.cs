using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Wolfgang.Etl.Json.Tests.Integration;

/// <summary>
/// Creates a unique temporary directory for a test and deletes it (recursively)
/// on dispose. Integration tests use this to exercise the extractors and loaders
/// against real files on disk rather than in-memory streams.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class TempWorkspace : IDisposable
{
    public TempWorkspace()
    {
        Root = Path.Combine
        (
            Path.GetTempPath(),
            "Wolfgang.Etl.Json.IT." + Guid.NewGuid().ToString("N")
        );
        Directory.CreateDirectory(Root);
    }



    /// <summary>
    /// Gets the absolute path of the temporary directory backing this workspace.
    /// </summary>
    public string Root { get; }



    /// <summary>
    /// Returns an absolute path inside the workspace for the given file name.
    /// </summary>
    /// <param name="fileName">The file name to combine with the workspace root.</param>
    public string PathFor(string fileName) => Path.Combine(Root, fileName);



    /// <summary>
    /// Opens an asynchronous <see cref="FileStream"/> for writing a new file in
    /// the workspace, overwriting any existing file of the same name.
    /// </summary>
    /// <param name="fileName">The file name to create.</param>
    public FileStream CreateFile(string fileName) =>
        new
        (
            PathFor(fileName),
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true
        );



    /// <summary>
    /// Opens an asynchronous <see cref="FileStream"/> for reading an existing
    /// file in the workspace.
    /// </summary>
    /// <param name="fileName">The file name to open.</param>
    public FileStream OpenFile(string fileName) =>
        new
        (
            PathFor(fileName),
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true
        );



    public void Dispose()
    {
        try
        {
            if (Directory.Exists(Root))
            {
                Directory.Delete(Root, recursive: true);
            }
        }
        catch (IOException)
        {
            // Best-effort cleanup: a virus scanner or lingering handle can briefly
            // lock a temp file on Windows. The OS reclaims %TEMP% regardless, so a
            // failed delete must not fail an otherwise-passing test.
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
