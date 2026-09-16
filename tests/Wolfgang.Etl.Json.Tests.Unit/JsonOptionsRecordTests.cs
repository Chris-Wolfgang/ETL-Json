using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// The options records (ADR-0009): each stage's record inherits the per-stage-kind base record from
/// Wolfgang.Etl.Abstractions 0.24, the record constructors apply both the inherited and the stage-specific
/// settings, and serializer configuration stays a constructor parameter beside the record.
/// </summary>
public class JsonOptionsRecordTests
{
    private static Func<ItemErrorContext, ItemErrorAction> AnyPolicy => _ => default;



    public static IEnumerable<object[]> ExtractorRecords() =>
    [
        [new JsonLineExtractorOptions()],
        [new JsonSingleStreamExtractorOptions()],
        [new JsonMultiStreamExtractorOptions()],
    ];



    public static IEnumerable<object[]> LoaderRecords() =>
    [
        [new JsonLineLoaderOptions()],
        [new JsonSingleStreamLoaderOptions()],
        [new JsonMultiStreamLoaderOptions()],
    ];



    [Theory]
    [MemberData(nameof(ExtractorRecords))]
    public void Extractor_records_inherit_ExtractorOptions(object record)
    {
        Assert.IsAssignableFrom<ExtractorOptions>(record);
    }



    [Theory]
    [MemberData(nameof(LoaderRecords))]
    public void Loader_records_inherit_LoaderOptions(object record)
    {
        Assert.IsAssignableFrom<LoaderOptions>(record);
    }



    [Fact]
    public void JsonLineExtractor_when_constructed_with_options_applies_inherited_and_own_settings()
    {
        var policy = AnyPolicy;
        var options = new JsonLineExtractorOptions
        {
            ReportingInterval = 5,
            SkipItemCount = 2,
            MaximumItemCount = 3,
            ErrorPolicy = policy,
            Encoding = Encoding.Unicode,
            EnableCheckpointing = true,
            StartByteOffset = 7,
        };

        using var sut = new JsonLineExtractor<PersonRecord>(new MemoryStream(), options);

        Assert.Equal(5, sut.ReportingInterval);
        Assert.Equal(2, sut.SkipItemCount);
        Assert.Equal(3, sut.MaximumItemCount);
        Assert.Same(policy, sut.ErrorPolicy);
        Assert.Same(Encoding.Unicode, sut.Encoding);
        Assert.True(sut.EnableCheckpointing);
        Assert.Equal(7, sut.StartByteOffset);
    }



    [Fact]
    public void JsonLineExtractor_when_constructed_with_typeInfo_and_options_applies_the_options()
    {
        var options = new JsonLineExtractorOptions { SkipItemCount = 2, EnableCheckpointing = true };

        using var sut = new JsonLineExtractor<PersonRecord>
        (
            new MemoryStream(),
            TestJsonContext.Default.PersonRecord,
            options
        );

        Assert.Equal(2, sut.SkipItemCount);
        Assert.True(sut.EnableCheckpointing);
    }



    [Fact]
    public void JsonLineExtractor_when_the_record_carries_SerializerOptions_uses_them()
    {
        var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        using var sut = new JsonLineExtractor<PersonRecord>(new MemoryStream(), new JsonLineExtractorOptions { SerializerOptions = serializerOptions });

        Assert.NotNull(sut);
    }



    [Fact]
    public void JsonLineExtractor_when_typeInfo_and_SerializerOptions_are_both_supplied_throws_ArgumentException()
    {
        var options = new JsonLineExtractorOptions { SerializerOptions = new JsonSerializerOptions() };

        var ex = Assert.Throws<ArgumentException>(() => new JsonLineExtractor<PersonRecord>(new MemoryStream(), TestJsonContext.Default.PersonRecord, options));

        Assert.Equal("options", ex.ParamName);
    }



    [Fact]
    public void JsonLineLoader_when_typeInfo_and_SerializerOptions_are_both_supplied_throws_ArgumentException()
    {
        var options = new JsonLineLoaderOptions { SerializerOptions = new JsonSerializerOptions() };

        var ex = Assert.Throws<ArgumentException>(() => new JsonLineLoader<PersonRecord>(new MemoryStream(), TestJsonContext.Default.PersonRecord, options));

        Assert.Equal("options", ex.ParamName);
    }



    [Fact]
    public void JsonLineExtractor_when_options_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonLineExtractor<PersonRecord>(new MemoryStream(), options: null!));

        Assert.Equal("options", ex.ParamName);
    }



    [Fact]
    public void JsonLineExtractorOptions_when_StartByteOffset_is_negative_throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new JsonLineExtractorOptions { StartByteOffset = -1 });
    }



    [Fact]
    public void JsonLineLoader_when_constructed_with_options_applies_inherited_and_own_settings()
    {
        var options = new JsonLineLoaderOptions
        {
            ReportingInterval = 5,
            MaximumItemCount = 3,
            Encoding = Encoding.Unicode,
            IsDryRun = true,
        };

        using var sut = new JsonLineLoader<PersonRecord>(new MemoryStream(), options);

        Assert.Equal(5, sut.ReportingInterval);
        Assert.Equal(3, sut.MaximumItemCount);
        Assert.Same(Encoding.Unicode, sut.Encoding);
        Assert.True(sut.IsDryRun);
    }



    [Fact]
    public void JsonSingleStreamLoader_and_JsonMultiStreamLoader_when_constructed_with_options_apply_IsDryRun()
    {
        using var single = new JsonSingleStreamLoader<PersonRecord>(new MemoryStream(), new JsonSingleStreamLoaderOptions { IsDryRun = true });
        var multi = new JsonMultiStreamLoader<PersonRecord>(_ => new MemoryStream(), new JsonMultiStreamLoaderOptions { IsDryRun = true });

        Assert.True(single.IsDryRun);
        Assert.True(multi.IsDryRun);
    }



    [Fact]
    public void JsonMultiStreamExtractor_when_constructed_with_named_sources_and_options_applies_inherited_settings()
    {
        var options = new JsonMultiStreamExtractorOptions { SkipItemCount = 4 };

        var sut = new JsonMultiStreamExtractor<PersonRecord>([new JsonNamedStream(new MemoryStream(), "a")], options);

        Assert.Equal(4, sut.SkipItemCount);
    }



    [Fact]
    public void Stages_when_constructed_without_options_keep_the_base_defaults()
    {
        var defaults = new ExtractorOptions();

        using var sut = new JsonSingleStreamExtractor<PersonRecord>(new MemoryStream());

        Assert.Equal(defaults.ReportingInterval, sut.ReportingInterval);
        Assert.Equal(defaults.SkipItemCount, sut.SkipItemCount);
        Assert.Equal(defaults.MaximumItemCount, sut.MaximumItemCount);
    }
}
