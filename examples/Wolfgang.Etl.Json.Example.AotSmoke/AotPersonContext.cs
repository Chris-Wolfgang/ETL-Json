using System.Text.Json.Serialization;

namespace Wolfgang.Etl.Json.Example.AotSmoke;

[JsonSerializable(typeof(AotPerson))]
internal sealed partial class AotPersonContext : JsonSerializerContext;
