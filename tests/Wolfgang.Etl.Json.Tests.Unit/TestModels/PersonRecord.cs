using System.Diagnostics.CodeAnalysis;

namespace Wolfgang.Etl.Json.Tests.Unit.TestModels;

[ExcludeFromCodeCoverage]
public record PersonRecord
{
    public string FirstName { get; set; } = string.Empty;



    public string LastName { get; set; } = string.Empty;



    public int Age { get; set; }
}
