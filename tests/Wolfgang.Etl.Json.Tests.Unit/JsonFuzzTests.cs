#if NET8_0_OR_GREATER

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsCheck;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// Property / fuzz tests (CsCheck): every loader -> extractor pair must round-trip any list of
/// records unchanged. The short version runs in PR CI at CsCheck's default case count; the long
/// version runs in <c>fuzz.yaml</c>, which raises the budget through the <c>CsCheck_Time</c> /
/// <c>CsCheck_Iter</c> environment variables CsCheck reads at runtime.
///
/// Marked <c>[Trait("Category", "Fuzz")]</c> so <c>fuzz.yaml</c> can select just these tests.
/// net8.0+ only: CsCheck targets net8.0 (see the csproj).
/// </summary>
[Trait("Category", "Fuzz")]
public class JsonFuzzTests
{
    // Characters that force JSON escaping (quote, backslash, control characters, and a newline
    // that must not split a JSON Lines record) mixed with ordinary and non-ASCII letters. Lone
    // UTF-16 surrogates are left out: they are not valid text, and the serializer replaces them.
    private static readonly Gen<string> GenField =
        Gen.Char["\"\\/\b\f\n\r\t abcxyzABCXYZ0189{}[]:,é漢"].Array[0, 16]
            .Select(chars => new string(chars));


    private static readonly Gen<PersonRecord> GenPerson =
        Gen.Select
        (
            GenField,
            GenField,
            Gen.Int,
            (first, last, age) => new PersonRecord { FirstName = first, LastName = last, Age = age }
        );


    private static readonly Gen<List<PersonRecord>> GenPeople = GenPerson.List[0, 40];



    [Fact]
    public async Task JsonLine_Extract_after_Load_round_trips_every_record()
    {
        await GenPeople.SampleAsync
        (
            async records =>
            {
                using var written = new MemoryStream();
                using (var loader = new JsonLineLoader<PersonRecord>(written))
                {
                    await loader.LoadAsync(records.ToAsyncEnumerable());
                }

                using var source = new MemoryStream(written.ToArray());
                using var extractor = new JsonLineExtractor<PersonRecord>(source);
                var readBack = await extractor.ExtractAsync().ToListAsync();

                Assert.Equal(records, readBack);
            }
        );
    }



    [Fact]
    public async Task JsonSingleStream_Extract_after_Load_round_trips_every_record()
    {
        await GenPeople.SampleAsync
        (
            async records =>
            {
                using var written = new MemoryStream();
                using (var loader = new JsonSingleStreamLoader<PersonRecord>(written))
                {
                    await loader.LoadAsync(records.ToAsyncEnumerable());
                }

                using var source = new MemoryStream(written.ToArray());
                using var extractor = new JsonSingleStreamExtractor<PersonRecord>(source);
                var readBack = await extractor.ExtractAsync().ToListAsync();

                Assert.Equal(records, readBack);
            }
        );
    }



    [Fact]
    public async Task JsonMultiStream_Extract_after_Load_round_trips_every_record()
    {
        await GenPeople.SampleAsync
        (
            async records =>
            {
                var written = new List<MemoryStream>();
                using (var loader = new JsonMultiStreamLoader<PersonRecord>(_ => NewTrackedStream(written)))
                {
                    await loader.LoadAsync(records.ToAsyncEnumerable());
                }

                var sources = written
                    .Select(stream => (Stream)new MemoryStream(stream.ToArray()))
                    .ToList();
                using var extractor = new JsonMultiStreamExtractor<PersonRecord>(sources);
                var readBack = await extractor.ExtractAsync().ToListAsync();

                Assert.Equal(records, readBack);
            }
        );
    }



    private static MemoryStream NewTrackedStream(List<MemoryStream> written)
    {
        var stream = new MemoryStream();
        written.Add(stream);
        return stream;
    }
}

#endif
