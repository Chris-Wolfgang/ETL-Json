using System;
using System.IO;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Integration;

public class TempWorkspaceTests
{
    [Fact]
    public void Dispose_deletes_the_workspace_directory()
    {
        var sut = new TempWorkspace();
        File.WriteAllText(sut.PathFor("a.json"), "[]");

        sut.Dispose();

        Assert.False(Directory.Exists(sut.Root));
    }



    [Fact]
    public void DeleteQuietly_when_delete_throws_IOException_swallows_it()
    {
        var ex = Record.Exception(() => TempWorkspace.DeleteQuietly(() => throw new IOException("locked")));

        Assert.Null(ex);
    }



    [Fact]
    public void DeleteQuietly_when_delete_throws_UnauthorizedAccessException_swallows_it()
    {
        var ex = Record.Exception(() => TempWorkspace.DeleteQuietly(() => throw new UnauthorizedAccessException("denied")));

        Assert.Null(ex);
    }



    [Fact]
    public void DeleteQuietly_when_delete_throws_another_exception_propagates_it()
    {
        Assert.Throws<InvalidOperationException>
        (
            () => TempWorkspace.DeleteQuietly(() => throw new InvalidOperationException("bug"))
        );
    }
}
