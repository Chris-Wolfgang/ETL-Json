using System.Diagnostics.CodeAnalysis;

namespace Wolfgang.Etl.Json.Tests.Integration.TestModels;

/// <summary>
/// Round-trip model for the integration tests. A <c>record</c> so equality is
/// structural — extracted instances compare equal to the originals they were
/// serialized from.
/// </summary>
[ExcludeFromCodeCoverage]
public record Person
{
    public int Id { get; set; }



    public string FirstName { get; set; } = string.Empty;



    public string LastName { get; set; } = string.Empty;



    public int Age { get; set; }
}
