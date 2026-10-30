using System;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Wolfgang.Etl.Json;

/// <summary>
/// The serialization contract for <typeparamref name="TRecord"/> used by every extractor and loader.
/// </summary>
/// <typeparam name="TRecord">The record type.</typeparam>
/// <remarks>
/// <para>
/// The worker methods of the stages only ever call the <see cref="JsonTypeInfo{T}"/> overloads of
/// <see cref="JsonSerializer"/>, which the trimmer and the native-AOT compiler can analyze. A stage built
/// from a caller-supplied <see cref="JsonTypeInfo{T}"/> (usually a source-generated
/// <c>JsonSerializerContext</c>) uses it as is through <see cref="FromTypeInfo"/>.
/// </para>
/// <para>
/// A stage built from <see cref="JsonSerializerOptions"/> goes through <see cref="FromOptions"/>, which
/// carries <c>RequiresUnreferencedCode</c> / <c>RequiresDynamicCode</c>. The reflection-based metadata is
/// resolved lazily, on the first record, exactly where <see cref="JsonSerializer"/>'s options overloads
/// would resolve it: the options are locked at the same moment and resolution errors surface at the same
/// call as before. Because the only path to reflection is through an annotated factory, the trim and AOT
/// analyzers report it at the caller's constructor call rather than inside this library.
/// </para>
/// </remarks>
internal sealed class RecordTypeInfo<TRecord>
{
    private const string ReflectionMessage = "JSON serialization and deserialization of unknown types may require types that cannot be statically analyzed. Use the JsonTypeInfo overload for AOT compatibility.";

    private readonly Func<JsonTypeInfo<TRecord>>? _resolve;
    private JsonTypeInfo<TRecord>? _typeInfo;



    private RecordTypeInfo
    (
        JsonTypeInfo<TRecord>? typeInfo,
        Func<JsonTypeInfo<TRecord>>? resolve
    )
    {
        _typeInfo = typeInfo;
        _resolve = resolve;
    }



    /// <summary>
    /// Gets the <see cref="JsonTypeInfo{T}"/> for <typeparamref name="TRecord"/>, resolving the
    /// reflection-based metadata on first use when the instance was created by <see cref="FromOptions"/>.
    /// </summary>
    internal JsonTypeInfo<TRecord> Value => _typeInfo ??= _resolve!();



    /// <summary>
    /// Wraps a caller-supplied <see cref="JsonTypeInfo{T}"/>. Trim- and AOT-safe.
    /// </summary>
    /// <param name="typeInfo">The type info to use.</param>
    /// <returns>The contract.</returns>
    internal static RecordTypeInfo<TRecord> FromTypeInfo(JsonTypeInfo<TRecord> typeInfo) =>
        new(typeInfo, resolve: null);



    /// <summary>
    /// Creates a contract that resolves reflection-based metadata from <paramref name="options"/> on first use.
    /// </summary>
    /// <param name="options">
    /// The serializer options, or <see langword="null"/> for <see cref="JsonSerializerOptions.Default"/>,
    /// matching <see cref="JsonSerializer"/>'s own handling of a <see langword="null"/> options argument.
    /// </param>
    /// <returns>The contract.</returns>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode(ReflectionMessage)]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode(ReflectionMessage)]
#endif
#endif
    internal static RecordTypeInfo<TRecord> FromOptions(JsonSerializerOptions? options) =>
        new(typeInfo: null, () => Resolve(options));



#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode(ReflectionMessage)]
#if NET7_0_OR_GREATER
    [RequiresDynamicCode(ReflectionMessage)]
#endif
#endif
    private static JsonTypeInfo<TRecord> Resolve(JsonSerializerOptions? options)
    {
        var effective = options ?? JsonSerializerOptions.Default;

        // The JsonSerializer options overloads do the same on their first call: lock the options and fill in the
        // reflection-based resolver when none was configured.
        if (!effective.IsReadOnly)
        {
            effective.MakeReadOnly(populateMissingResolver: true);
        }

        return (JsonTypeInfo<TRecord>)effective.GetTypeInfo(typeof(TRecord));
    }
}
