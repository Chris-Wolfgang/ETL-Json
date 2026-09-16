using System.IO;
using System.Text.Json;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// Compile-time guards for overload resolution across the shipped constructors and the record constructors
/// added for ADR-0009. Every call shape a 0.8.x caller could have written — including a positional
/// <see langword="null"/> in the second (or, for the type-info family, third) position — must still bind to
/// exactly one constructor. If any of these lines stops compiling with CS0121, an overload was added that
/// makes an existing call ambiguous; that is the regression this file exists to catch.
/// </summary>
public class ConstructorOverloadResolutionTests
{
    [Fact]
    public void Positional_null_after_the_source_still_binds_on_every_stage()
    {
        // Binds the (source, ILogger?) overload: all of its parameters have a corresponding argument, so it is
        // preferred over the serializer-options and record overloads, which would need default substitution.
        using var lineExtractor = new JsonLineExtractor<PersonRecord>(new MemoryStream(), null);
        using var lineLoader = new JsonLineLoader<PersonRecord>(new MemoryStream(), null);
        using var singleExtractor = new JsonSingleStreamExtractor<PersonRecord>(new MemoryStream(), null);
        using var singleLoader = new JsonSingleStreamLoader<PersonRecord>(new MemoryStream(), null);
        var multiExtractor = new JsonMultiStreamExtractor<PersonRecord>([new MemoryStream()], null);
        var multiExtractorNamed = new JsonMultiStreamExtractor<PersonRecord>([new JsonNamedStream(new MemoryStream(), "a")], null);
        var multiLoader = new JsonMultiStreamLoader<PersonRecord>(_ => new MemoryStream(), null);
        var multiLoaderNamed = new JsonMultiStreamLoader<PersonRecord>(_ => new JsonNamedDestination(new MemoryStream()), null);

        Assert.NotNull(lineExtractor);
        Assert.NotNull(lineLoader);
        Assert.NotNull(singleExtractor);
        Assert.NotNull(singleLoader);
        Assert.NotNull(multiExtractor);
        Assert.NotNull(multiExtractorNamed);
        Assert.NotNull(multiLoader);
        Assert.NotNull(multiLoaderNamed);
    }



    [Fact]
    public void Positional_null_after_the_type_info_still_binds_on_every_stage()
    {
        // Binds the (source, JsonTypeInfo<T>, ILogger?) overload for the same reason.
        var typeInfo = TestJsonContext.Default.PersonRecord;

        using var lineExtractor = new JsonLineExtractor<PersonRecord>(new MemoryStream(), typeInfo, null);
        using var lineLoader = new JsonLineLoader<PersonRecord>(new MemoryStream(), typeInfo, null);
        using var singleExtractor = new JsonSingleStreamExtractor<PersonRecord>(new MemoryStream(), typeInfo, null);
        using var singleLoader = new JsonSingleStreamLoader<PersonRecord>(new MemoryStream(), typeInfo, null);
        var multiExtractor = new JsonMultiStreamExtractor<PersonRecord>([new MemoryStream()], typeInfo, null);
        var multiLoader = new JsonMultiStreamLoader<PersonRecord>(_ => new MemoryStream(), typeInfo, null);

        Assert.NotNull(lineExtractor);
        Assert.NotNull(lineLoader);
        Assert.NotNull(singleExtractor);
        Assert.NotNull(singleLoader);
        Assert.NotNull(multiExtractor);
        Assert.NotNull(multiLoader);
    }



    [Fact]
    public void Positional_serializer_options_with_and_without_a_null_logger_still_bind()
    {
        // (source, JsonSerializerOptions?) and (source, JsonSerializerOptions?, ILogger?) — the shipped
        // reflection family — with the serializer options typed. (source, null, null) is deliberately absent:
        // it has been ambiguous since 0.8.x between this overload and (source, JsonTypeInfo<T>, ILogger?),
        // both of which take the two nulls with no default substitution; nothing here changes that.
        var serializerOptions = new JsonSerializerOptions();

        using var typed = new JsonLineExtractor<PersonRecord>(new MemoryStream(), serializerOptions);
        using var typedWithLogger = new JsonLineExtractor<PersonRecord>(new MemoryStream(), serializerOptions, null);
        using var record = new JsonLineExtractor<PersonRecord>(new MemoryStream(), new JsonLineExtractorOptions());
        using var recordWithLogger = new JsonLineExtractor<PersonRecord>(new MemoryStream(), new JsonLineExtractorOptions(), null);

        Assert.NotNull(typed);
        Assert.NotNull(typedWithLogger);
        Assert.NotNull(record);
        Assert.NotNull(recordWithLogger);
    }
}
