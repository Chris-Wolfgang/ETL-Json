using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wolfgang.Etl.Json.Tests.Unit.TestModels;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

/// <summary>
/// <c>RecordTypeInfo&lt;T&gt;</c>: the single serialization contract the stages' workers use. A caller-supplied
/// type info is used as is; options-based metadata is resolved on first use, so the options stay mutable
/// until the first record, exactly as with the <see cref="JsonSerializer"/> options overloads.
/// </summary>
public class RecordTypeInfoTests
{
    [Fact]
    public void Value_when_created_from_a_type_info_returns_that_instance()
    {
        var typeInfo = TestJsonContext.Default.PersonRecord;

        var sut = RecordTypeInfo<PersonRecord>.FromTypeInfo(typeInfo);

        Assert.Same(typeInfo, sut.Value);
    }



    [Fact]
    public void FromOptions_does_not_lock_the_options_before_first_use()
    {
        var options = new JsonSerializerOptions();

        _ = RecordTypeInfo<PersonRecord>.FromOptions(options);

        Assert.False(options.IsReadOnly);
    }



    [Fact]
    public void Value_when_created_from_options_locks_them()
    {
        var options = new JsonSerializerOptions();
        var sut = RecordTypeInfo<PersonRecord>.FromOptions(options);

        _ = sut.Value;

        Assert.True(options.IsReadOnly);
    }



    [Fact]
    public void Value_when_created_from_options_resolves_the_record_type()
    {
        var sut = RecordTypeInfo<PersonRecord>.FromOptions(new JsonSerializerOptions());

        Assert.Equal(typeof(PersonRecord), sut.Value.Type);
    }



    [Fact]
    public void Value_when_created_from_null_options_resolves_the_record_type()
    {
        // STJ shares metadata caches between equivalent options instances, so the resolved
        // JsonTypeInfo.Options is not guaranteed to be the JsonSerializerOptions.Default instance itself.
        var sut = RecordTypeInfo<PersonRecord>.FromOptions(options: null);

        Assert.Equal(typeof(PersonRecord), sut.Value.Type);
    }



    [Fact]
    public void Value_when_read_twice_returns_the_cached_instance()
    {
        var sut = RecordTypeInfo<PersonRecord>.FromOptions(new JsonSerializerOptions());

        var first = sut.Value;

        Assert.Same(first, sut.Value);
    }



    [Fact]
    public async Task ExtractAsync_when_options_change_after_construction_honours_the_change()
    {
        // The options are only locked when the first record is read, as with JsonSerializer's options
        // overloads, so configuring them between construction and extraction still takes effect.
        var options = new JsonSerializerOptions();
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("{\"firstname\":\"Alice\",\"lastname\":\"Smith\",\"age\":30}\n"));
        var sut = new JsonLineExtractor<PersonRecord>(stream, options);
        options.PropertyNameCaseInsensitive = true;

        var items = new List<PersonRecord>();
        await foreach (var item in sut.ExtractAsync())
        {
            items.Add(item);
        }

        Assert.Equal("Alice", Assert.Single(items).FirstName);
    }
}
