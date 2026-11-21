using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Wolfgang.Etl.Json.Tests.Unit.TestModels;

[JsonSerializable(typeof(PersonRecord))]
[JsonSerializable(typeof(List<PersonRecord>))]
[JsonSerializable(typeof(SnakeCasePersonRecord))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(string))]
internal partial class TestJsonContext : JsonSerializerContext
{
}
