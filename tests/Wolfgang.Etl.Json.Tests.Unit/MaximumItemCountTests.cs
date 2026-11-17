using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// <c>MaximumItemCount</c> is tested before the next record, source or upstream item is consumed, so
/// the item after the limit can neither fail the run nor be pulled from upstream.
/// </summary>
public class MaximumItemCountTests
{
    private const string Good = "{\"FirstName\":\"A\",\"LastName\":\"B\",\"Age\":1}";



    [Fact]
    public async Task JsonLineExtractor_when_limit_is_reached_does_not_read_a_malformed_next_line()
    {
        using var stream = ToStream(Good + "\n{bad\n" + Good + "\n");
        var sut = new JsonLineExtractor<PersonRecord>(stream, new JsonLineExtractorOptions { MaximumItemCount = 1 });

        var items = await sut.ExtractAsync().ToListAsync();

        Assert.Single(items);
    }



    [Fact]
    public async Task JsonMultiStreamExtractor_when_limit_is_reached_does_not_request_the_next_source()
    {
        var requested = 0;
        IEnumerable<Stream> Sources()
        {
            requested++;
            yield return ToStream(Good);
            requested++;
            yield return ToStream("{bad");
        }

        var sut = new JsonMultiStreamExtractor<PersonRecord>(Sources(), new JsonMultiStreamExtractorOptions { MaximumItemCount = 1 });

        var items = await sut.ExtractAsync().ToListAsync();

        Assert.Single(items);
        Assert.Equal(1, requested);
    }



    [Fact]
    public async Task JsonLineLoader_when_limit_is_reached_does_not_pull_another_item_from_upstream()
    {
        var pulled = 0;
        using var stream = new MemoryStream();
        var sut = new JsonLineLoader<PersonRecord>(stream, new JsonLineLoaderOptions { MaximumItemCount = 2 });

        await sut.LoadAsync(Counting(5, () => pulled++));

        Assert.Equal(2, pulled);
        Assert.Equal(2, sut.CurrentItemCount);
    }



    [Fact]
    public async Task JsonSingleStreamLoader_when_limit_is_reached_does_not_pull_another_item_from_upstream()
    {
        var pulled = 0;
        using var stream = new MemoryStream();
        var sut = new JsonSingleStreamLoader<PersonRecord>(stream, new JsonSingleStreamLoaderOptions { MaximumItemCount = 2 });

        await sut.LoadAsync(Counting(5, () => pulled++));

        Assert.Equal(2, pulled);
        Assert.Equal(2, sut.CurrentItemCount);
    }



    [Fact]
    public async Task JsonMultiStreamLoader_when_limit_is_reached_does_not_pull_another_item_from_upstream()
    {
        var pulled = 0;
        var sut = new JsonMultiStreamLoader<PersonRecord>(_ => new MemoryStream(), new JsonMultiStreamLoaderOptions { MaximumItemCount = 2 });

        await sut.LoadAsync(Counting(5, () => pulled++));

        Assert.Equal(2, pulled);
        Assert.Equal(2, sut.CurrentItemCount);
    }



    [Fact]
    public async Task ItemLimit_Until_when_source_is_null_throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await ItemLimit.Until<int>(null, () => false).ToListAsync());
    }



    private static async IAsyncEnumerable<PersonRecord> Counting(int count, Action onPull)
    {
        for (var i = 0; i < count; i++)
        {
            onPull();
            yield return new PersonRecord { FirstName = "A", LastName = "B", Age = i };
            await Task.Yield();
        }
    }



    private static MemoryStream ToStream(string text) => new(Encoding.UTF8.GetBytes(text));
}
