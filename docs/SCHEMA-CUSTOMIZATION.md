# Schema Customization Without POCO Attributes

All extractors and loaders accept a `JsonSerializerOptions` or `JsonTypeInfo<T>` overload, so you can fully customize JSON serialization behaviour without modifying — or even owning — the POCO class.

## Naming policies

Apply a naming policy to remap all property names on write and match them case-insensitively on read:

```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    PropertyNameCaseInsensitive = true,
};

// Loader: FirstName → first_name, LastName → last_name, …
var loader = new JsonLineLoader<Person>(stream, options);

// Extractor: reads first_name → FirstName, …
var extractor = new JsonLineExtractor<Person>(stream, options);
```

Built-in policies: `CamelCase`, `SnakeCaseLower`, `SnakeCaseUpper`, `KebabCaseLower`, `KebabCaseUpper`.

## Ignoring properties

Use `DefaultJsonTypeInfoResolver` modifiers to suppress individual properties at runtime:

```csharp
var options = new JsonSerializerOptions();
options.TypeInfoResolverChain.Add(new DefaultJsonTypeInfoResolver
{
    Modifiers =
    {
        typeInfo =>
        {
            if (typeInfo.Type != typeof(Person)) return;
            foreach (var prop in typeInfo.Properties)
            {
                if (string.Equals(prop.Name, "InternalId", StringComparison.Ordinal))
                    prop.ShouldSerialize = (_, _) => false;
            }
        }
    }
});

var loader = new JsonLineLoader<Person>(stream, options);
```

> **TFM note:** `DefaultJsonTypeInfoResolver` modifiers require .NET 8 or later. For earlier TFMs use `[JsonIgnore]` on the POCO or a custom `JsonConverter<T>`.

## Custom converters

Register a converter for a specific type without touching the POCO:

```csharp
var options = new JsonSerializerOptions
{
    Converters = { new UnixEpochDateTimeConverter() }
};

var extractor = new JsonLineExtractor<Event>(stream, options);
```

## Source-generated type metadata (AOT-safe)

Pass a `JsonTypeInfo<T>` directly for reflection-free, AOT-compatible serialization:

```csharp
[JsonSerializable(typeof(Person))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
partial class PersonContext : JsonSerializerContext { }

var extractor = new JsonSingleStreamExtractor<Person>(stream, PersonContext.Default.Person);
var loader    = new JsonLineLoader<Person>(stream, PersonContext.Default.Person);
```

Source-generated contexts resolve naming, ignores, and converters at compile time — no runtime reflection is used.

## Runnable example

See `examples/Wolfgang.Etl.Json.Examples/Program.cs` — `SchemaCustomizationExample()` for a complete demo of snake_case naming and property suppression via `DefaultJsonTypeInfoResolver`.
