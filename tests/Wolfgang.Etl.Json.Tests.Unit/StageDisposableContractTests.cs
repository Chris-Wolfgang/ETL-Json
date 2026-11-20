using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Wolfgang.Etl.TestKit.Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public class JsonLineExtractorDisposableStageContractTests : DisposableStageContractTests<JsonLineExtractor<PersonRecord>>
{
    protected override JsonLineExtractor<PersonRecord> CreateSut() => new JsonLineExtractor<PersonRecord>(new MemoryStream());



    protected override async Task<bool> InvokeReportsObjectDisposedAsync(bool disposeFirst, bool useAsyncDispose)
    {
        var sut = CreateSut();
        if (disposeFirst)
        {
            if (useAsyncDispose)
            {
                await sut.DisposeAsync();
            }
            else
            {
                sut.Dispose();
            }
        }

        try
        {
            await sut.ExtractAsync().ToListAsync();
            return false;
        }
        catch (ObjectDisposedException)
        {
            return true;
        }
    }
}



public class JsonSingleStreamExtractorDisposableStageContractTests : DisposableStageContractTests<JsonSingleStreamExtractor<PersonRecord>>
{
    protected override JsonSingleStreamExtractor<PersonRecord> CreateSut() => new JsonSingleStreamExtractor<PersonRecord>(new MemoryStream(Encoding.UTF8.GetBytes("[]")));



    protected override async Task<bool> InvokeReportsObjectDisposedAsync(bool disposeFirst, bool useAsyncDispose)
    {
        var sut = CreateSut();
        if (disposeFirst)
        {
            if (useAsyncDispose)
            {
                await sut.DisposeAsync();
            }
            else
            {
                sut.Dispose();
            }
        }

        try
        {
            await sut.ExtractAsync().ToListAsync();
            return false;
        }
        catch (ObjectDisposedException)
        {
            return true;
        }
    }
}



public class JsonMultiStreamExtractorDisposableStageContractTests : DisposableStageContractTests<JsonMultiStreamExtractor<PersonRecord>>
{
    protected override JsonMultiStreamExtractor<PersonRecord> CreateSut() => new JsonMultiStreamExtractor<PersonRecord>(new Stream[] { new MemoryStream(Encoding.UTF8.GetBytes("{}")) });



    protected override async Task<bool> InvokeReportsObjectDisposedAsync(bool disposeFirst, bool useAsyncDispose)
    {
        var sut = CreateSut();
        if (disposeFirst)
        {
            if (useAsyncDispose)
            {
                await sut.DisposeAsync();
            }
            else
            {
                sut.Dispose();
            }
        }

        try
        {
            await sut.ExtractAsync().ToListAsync();
            return false;
        }
        catch (ObjectDisposedException)
        {
            return true;
        }
    }
}



public class JsonLineLoaderDisposableStageContractTests : DisposableStageContractTests<JsonLineLoader<PersonRecord>>
{
    protected override JsonLineLoader<PersonRecord> CreateSut() => new JsonLineLoader<PersonRecord>(new MemoryStream());



    protected override async Task<bool> InvokeReportsObjectDisposedAsync(bool disposeFirst, bool useAsyncDispose)
    {
        var sut = CreateSut();
        if (disposeFirst)
        {
            if (useAsyncDispose)
            {
                await sut.DisposeAsync();
            }
            else
            {
                sut.Dispose();
            }
        }

        try
        {
            await sut.LoadAsync(new[] { new PersonRecord() }.ToAsyncEnumerable());
            return false;
        }
        catch (ObjectDisposedException)
        {
            return true;
        }
    }
}



public class JsonSingleStreamLoaderDisposableStageContractTests : DisposableStageContractTests<JsonSingleStreamLoader<PersonRecord>>
{
    protected override JsonSingleStreamLoader<PersonRecord> CreateSut() => new JsonSingleStreamLoader<PersonRecord>(new MemoryStream());



    protected override async Task<bool> InvokeReportsObjectDisposedAsync(bool disposeFirst, bool useAsyncDispose)
    {
        var sut = CreateSut();
        if (disposeFirst)
        {
            if (useAsyncDispose)
            {
                await sut.DisposeAsync();
            }
            else
            {
                sut.Dispose();
            }
        }

        try
        {
            await sut.LoadAsync(new[] { new PersonRecord() }.ToAsyncEnumerable());
            return false;
        }
        catch (ObjectDisposedException)
        {
            return true;
        }
    }
}



public class JsonMultiStreamLoaderDisposableStageContractTests : DisposableStageContractTests<JsonMultiStreamLoader<PersonRecord>>
{
    protected override JsonMultiStreamLoader<PersonRecord> CreateSut() => new JsonMultiStreamLoader<PersonRecord>(_ => new MemoryStream());



    protected override async Task<bool> InvokeReportsObjectDisposedAsync(bool disposeFirst, bool useAsyncDispose)
    {
        var sut = CreateSut();
        if (disposeFirst)
        {
            if (useAsyncDispose)
            {
                await sut.DisposeAsync();
            }
            else
            {
                sut.Dispose();
            }
        }

        try
        {
            await sut.LoadAsync(new[] { new PersonRecord() }.ToAsyncEnumerable());
            return false;
        }
        catch (ObjectDisposedException)
        {
            return true;
        }
    }
}
