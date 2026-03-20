using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Wolfgang.Etl.Json.Tests.Unit.TestModels;

[ExcludeFromCodeCoverage]
public record SnakeCasePersonRecord
{
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;



    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;



    [JsonPropertyName("age")]
    public int Age { get; set; }
}
