using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// Argument guards that no other test reached: the pipeline extensions' stream overloads, the path
/// constructors, and the options-record / type-info constructors of the multi-stream stages. Each
/// fact checks the parameter name, so a guard that stops firing (and lets a later call fail with a
/// different argument) is reported.
/// </summary>
public class NullGuardTests
{
    [Fact]
    public void JsonLineLoader_extension_with_stream_when_pipeline_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => ((IEtlPipeline<PersonRecord>)null!).JsonLineLoader(new MemoryStream()));

        Assert.Equal("pipeline", ex.ParamName);
    }



    [Fact]
    public void JsonSingleStreamLoader_extension_with_stream_when_pipeline_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => ((IEtlPipeline<PersonRecord>)null!).JsonSingleStreamLoader(new MemoryStream()));

        Assert.Equal("pipeline", ex.ParamName);
    }



    [Fact]
    public void JsonSingleStreamExtractor_extension_with_stream_when_pipeline_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => ((EtlPipeline)null!).JsonSingleStreamExtractor<PersonRecord>(new MemoryStream()));

        Assert.Equal("pipeline", ex.ParamName);
    }



    [Fact]
    public void JsonLineExtractor_path_ctor_when_path_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonLineExtractor<PersonRecord>((string)null!, (JsonSerializerOptions?)null));

        Assert.Equal("path", ex.ParamName);
    }



    [Fact]
    public void JsonLineExtractor_path_ctor_with_a_record_when_path_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonLineExtractor<PersonRecord>((string)null!, new JsonLineExtractorOptions()));

        Assert.Equal("path", ex.ParamName);
    }



    [Fact]
    public void JsonSingleStreamExtractor_path_ctor_when_path_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonSingleStreamExtractor<PersonRecord>((string)null!, (JsonSerializerOptions?)null));

        Assert.Equal("path", ex.ParamName);
    }



    [Fact]
    public void JsonSingleStreamExtractor_path_ctor_with_a_record_when_path_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonSingleStreamExtractor<PersonRecord>((string)null!, new JsonSingleStreamExtractorOptions()));

        Assert.Equal("path", ex.ParamName);
    }



    [Fact]
    public void JsonMultiStreamExtractor_record_ctor_when_streams_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonMultiStreamExtractor<PersonRecord>((IEnumerable<Stream>)null!, new JsonMultiStreamExtractorOptions()));

        Assert.Equal("streams", ex.ParamName);
    }



    [Fact]
    public void JsonMultiStreamExtractor_typeInfo_record_ctor_when_streams_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonMultiStreamExtractor<PersonRecord>((IEnumerable<Stream>)null!, TestJsonContext.Default.PersonRecord, new JsonMultiStreamExtractorOptions()));

        Assert.Equal("streams", ex.ParamName);
    }



    [Fact]
    public void JsonMultiStreamLoader_serializer_options_ctor_when_streamFactory_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonMultiStreamLoader<PersonRecord>((Func<PersonRecord, Stream>)null!, (JsonSerializerOptions?)null));

        Assert.Equal("streamFactory", ex.ParamName);
    }



    [Fact]
    public void JsonMultiStreamLoader_record_ctor_when_streamFactory_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonMultiStreamLoader<PersonRecord>((Func<PersonRecord, Stream>)null!, new JsonMultiStreamLoaderOptions()));

        Assert.Equal("streamFactory", ex.ParamName);
    }



    [Fact]
    public void JsonMultiStreamLoader_typeInfo_record_ctor_when_streamFactory_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonMultiStreamLoader<PersonRecord>((Func<PersonRecord, Stream>)null!, TestJsonContext.Default.PersonRecord, new JsonMultiStreamLoaderOptions()));

        Assert.Equal("streamFactory", ex.ParamName);
    }



    [Fact]
    public void JsonLineLoader_typeInfo_record_ctor_when_stream_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonLineLoader<PersonRecord>(null!, TestJsonContext.Default.PersonRecord, new JsonLineLoaderOptions()));

        Assert.Equal("stream", ex.ParamName);
    }



    [Fact]
    public void JsonLineLoader_typeInfo_record_ctor_when_typeInfo_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonLineLoader<PersonRecord>(new MemoryStream(), null!, new JsonLineLoaderOptions()));

        Assert.Equal("typeInfo", ex.ParamName);
    }


    [Fact]
    public void JsonLineExtractor_typeInfo_record_ctor_when_options_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonLineExtractor<PersonRecord>(new MemoryStream(), TestJsonContext.Default.PersonRecord, (JsonLineExtractorOptions)null!));

        Assert.Equal("options", ex.ParamName);
    }


    [Fact]
    public void JsonLineLoader_typeInfo_record_ctor_when_options_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonLineLoader<PersonRecord>(new MemoryStream(), TestJsonContext.Default.PersonRecord, (JsonLineLoaderOptions)null!));

        Assert.Equal("options", ex.ParamName);
    }


    [Fact]
    public void JsonSingleStreamExtractor_typeInfo_record_ctor_when_options_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonSingleStreamExtractor<PersonRecord>(new MemoryStream(), TestJsonContext.Default.PersonRecord, (JsonSingleStreamExtractorOptions)null!));

        Assert.Equal("options", ex.ParamName);
    }


    [Fact]
    public void JsonSingleStreamLoader_typeInfo_record_ctor_when_options_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonSingleStreamLoader<PersonRecord>(new MemoryStream(), TestJsonContext.Default.PersonRecord, (JsonSingleStreamLoaderOptions)null!));

        Assert.Equal("options", ex.ParamName);
    }


    [Fact]
    public void JsonMultiStreamExtractor_typeInfo_record_ctor_when_options_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonMultiStreamExtractor<PersonRecord>(new[] { new MemoryStream() }, TestJsonContext.Default.PersonRecord, (JsonMultiStreamExtractorOptions)null!));

        Assert.Equal("options", ex.ParamName);
    }


    [Fact]
    public void JsonMultiStreamLoader_typeInfo_record_ctor_when_options_is_null_throws_ArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new JsonMultiStreamLoader<PersonRecord>((Func<PersonRecord, Stream>)(_ => new MemoryStream()), TestJsonContext.Default.PersonRecord, (JsonMultiStreamLoaderOptions)null!));

        Assert.Equal("options", ex.ParamName);
    }
}
