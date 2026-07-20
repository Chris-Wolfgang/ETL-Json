using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Class-named JSON source factories for the fluent <see cref="EtlPipeline"/> chain. Each factory
/// constructs the matching extractor and seeds the pipeline, enabling
/// <c>EtlPipeline.Create().JsonLineExtractor&lt;Person&gt;("people.jsonl")</c>.
/// </summary>
/// <remarks>
/// Path-based factories own the file stream they open and dispose it when the pipeline finishes
/// enumerating. Stream-based factories do not dispose — the caller owns the stream's lifetime.
/// </remarks>
public static class EtlPipelineJsonSourceExtensions
{
    /// <summary>
    /// Starts a pipeline that reads JSONL (JSON Lines / NDJSON) records from a file.
    /// </summary>
    /// <typeparam name="T">The record type to deserialize.</typeparam>
    /// <param name="pipeline">The pipeline seed from <see cref="EtlPipeline.Create"/>.</param>
    /// <param name="path">The path of the JSONL file to read.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>An <see cref="IEtlPipeline{T}"/> for chaining.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pipeline"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    public static IEtlPipeline<T> JsonLineExtractor<T>
    (
        this EtlPipeline pipeline,
        string path,
        JsonSerializerOptions? options = null
    )
        where T : notnull
    {
        if (pipeline is null)
        {
            throw new ArgumentNullException(nameof(pipeline));
        }

        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        return pipeline.From(ReadJsonLinesAsync<T>(path, options));
    }


    /// <summary>
    /// Starts a pipeline that reads JSONL records from an existing stream. The caller owns the stream.
    /// </summary>
    /// <typeparam name="T">The record type to deserialize.</typeparam>
    /// <param name="pipeline">The pipeline seed from <see cref="EtlPipeline.Create"/>.</param>
    /// <param name="stream">The stream to read JSONL from.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>An <see cref="IEtlPipeline{T}"/> for chaining.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pipeline"/> or <paramref name="stream"/> is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    public static IEtlPipeline<T> JsonLineExtractor<T>
    (
        this EtlPipeline pipeline,
        Stream stream,
        JsonSerializerOptions? options = null
    )
        where T : notnull
    {
        if (pipeline is null)
        {
            throw new ArgumentNullException(nameof(pipeline));
        }

        if (stream is null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        var extractor = options is null
            ? new JsonLineExtractor<T>(stream)
            : new JsonLineExtractor<T>(stream, options);
        return pipeline.From(extractor);
    }


    /// <summary>
    /// Starts a pipeline that reads records from a single JSON array file.
    /// </summary>
    /// <typeparam name="T">The record type to deserialize.</typeparam>
    /// <param name="pipeline">The pipeline seed from <see cref="EtlPipeline.Create"/>.</param>
    /// <param name="path">The path of the JSON array file to read.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>An <see cref="IEtlPipeline{T}"/> for chaining.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pipeline"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    public static IEtlPipeline<T> JsonSingleStreamExtractor<T>
    (
        this EtlPipeline pipeline,
        string path,
        JsonSerializerOptions? options = null
    )
        where T : notnull
    {
        if (pipeline is null)
        {
            throw new ArgumentNullException(nameof(pipeline));
        }

        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        return pipeline.From(ReadJsonArrayAsync<T>(path, options));
    }


    /// <summary>
    /// Starts a pipeline that reads records from a single JSON array stream. The caller owns the stream.
    /// </summary>
    /// <typeparam name="T">The record type to deserialize.</typeparam>
    /// <param name="pipeline">The pipeline seed from <see cref="EtlPipeline.Create"/>.</param>
    /// <param name="stream">The stream to read the JSON array from.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>An <see cref="IEtlPipeline{T}"/> for chaining.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pipeline"/> or <paramref name="stream"/> is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    public static IEtlPipeline<T> JsonSingleStreamExtractor<T>
    (
        this EtlPipeline pipeline,
        Stream stream,
        JsonSerializerOptions? options = null
    )
        where T : notnull
    {
        if (pipeline is null)
        {
            throw new ArgumentNullException(nameof(pipeline));
        }

        if (stream is null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        var extractor = options is null
            ? new JsonSingleStreamExtractor<T>(stream)
            : new JsonSingleStreamExtractor<T>(stream, options);
        return pipeline.From(extractor);
    }


#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    private static async IAsyncEnumerable<T> ReadJsonLinesAsync<T>
    (
        string path,
        JsonSerializerOptions? options,
        [EnumeratorCancellation] CancellationToken token = default
    )
        where T : notnull
    {
#if NETSTANDARD2_0 || NET462 || NET481
        using var stream = File.OpenRead(path);
#else
        await using var stream = File.OpenRead(path);
#endif
        var extractor = options is null
            ? new JsonLineExtractor<T>(stream)
            : new JsonLineExtractor<T>(stream, options);
        await foreach (var item in extractor.ExtractAsync(token).ConfigureAwait(false))
        {
            yield return item;
        }
    }


#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON deserialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    private static async IAsyncEnumerable<T> ReadJsonArrayAsync<T>
    (
        string path,
        JsonSerializerOptions? options,
        [EnumeratorCancellation] CancellationToken token = default
    )
        where T : notnull
    {
#if NETSTANDARD2_0 || NET462 || NET481
        using var stream = File.OpenRead(path);
#else
        await using var stream = File.OpenRead(path);
#endif
        var extractor = options is null
            ? new JsonSingleStreamExtractor<T>(stream)
            : new JsonSingleStreamExtractor<T>(stream, options);
        await foreach (var item in extractor.ExtractAsync(token).ConfigureAwait(false))
        {
            yield return item;
        }
    }
}
