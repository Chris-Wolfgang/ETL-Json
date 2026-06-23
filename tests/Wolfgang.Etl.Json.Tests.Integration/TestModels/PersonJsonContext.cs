using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Wolfgang.Etl.Json.Tests.Integration.TestModels;

/// <summary>
/// Source-generated serialization context, used to exercise the
/// <c>JsonTypeInfo&lt;T&gt;</c> (reflection-free / AOT-friendly) constructors
/// end-to-end against real streams.
/// </summary>
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(List<Person>))]
internal partial class PersonJsonContext : JsonSerializerContext
{
}
