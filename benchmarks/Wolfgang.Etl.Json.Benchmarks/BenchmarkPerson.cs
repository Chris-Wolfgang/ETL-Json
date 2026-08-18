using System.Diagnostics.CodeAnalysis;

namespace Wolfgang.Etl.Json.Benchmarks;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global",
    Justification = "Property getters are consumed by System.Text.Json serialization in benchmark setup.")]
public class BenchmarkPerson
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
}
