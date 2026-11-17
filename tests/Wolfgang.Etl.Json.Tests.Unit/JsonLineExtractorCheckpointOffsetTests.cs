using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// <see cref="JsonLineExtractor{TRecord}.CurrentByteOffset"/> must be an absolute, valid position in the stream
/// at the moment each item is yielded, even for a stream the caller already advanced and for a final line
/// without a terminator.
/// </summary>
public class JsonLineExtractorCheckpointOffsetTests
{
    private const string Header = "HEADER\n";
    private const string Line1 = "{\"FirstName\":\"A\",\"LastName\":\"B\",\"Age\":1}";
    private const string Line2 = "{\"FirstName\":\"C\",\"LastName\":\"D\",\"Age\":2}";



    [Fact]
    public async Task CurrentByteOffset_when_stream_is_pre_positioned_is_absolute_and_resumable()
    {
        var bytes = Encoding.UTF8.GetBytes(Header + Line1 + "\n" + Line2 + "\n");
        using var stream = new MemoryStream(bytes);
        stream.Position = Header.Length;

        var first = new JsonLineExtractor<PersonRecord>(stream, new JsonLineExtractorOptions { EnableCheckpointing = true, MaximumItemCount = 1 });
        var offsets = new List<long>();
        await foreach (var _ in first.ExtractAsync())
        {
            offsets.Add(first.CurrentByteOffset);
        }

        Assert.Equal(new long[] { Header.Length + Line1.Length + 1 }, offsets);

        var resumed = new JsonLineExtractor<PersonRecord>(stream, new JsonLineExtractorOptions { StartByteOffset = offsets[0] });
        var items = await resumed.ExtractAsync().ToListAsync();

        Assert.Equal(2, Assert.Single(items).Age);
    }



    [Fact]
    public async Task CurrentByteOffset_when_last_line_has_no_terminator_never_passes_the_end_inside_the_loop()
    {
        var bytes = Encoding.UTF8.GetBytes(Line1 + "\n" + Line2);
        using var stream = new MemoryStream(bytes);

        var sut = new JsonLineExtractor<PersonRecord>(stream, new JsonLineExtractorOptions { EnableCheckpointing = true });
        var offsets = new List<long>();
        await foreach (var _ in sut.ExtractAsync())
        {
            offsets.Add(sut.CurrentByteOffset);
        }

        Assert.Equal(new long[] { Line1.Length + 1, bytes.Length }, offsets);
        Assert.Equal(bytes.Length, sut.CurrentByteOffset);
    }
}
