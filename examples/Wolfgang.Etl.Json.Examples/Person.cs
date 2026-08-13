using System.Diagnostics.CodeAnalysis;

namespace Wolfgang.Etl.Json.Examples;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global",
    Justification = "Property getters are consumed by System.Text.Json serialization in the demo pipeline.")]
public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? Email { get; set; }
}
