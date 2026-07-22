using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Class-named JSON sink terminators for the fluent <see cref="EtlPipeline"/> chain. Each terminator
/// constructs the matching loader and terminates the pipeline, enabling
/// <c>… .JsonLineLoader&lt;Person&gt;("people.jsonl")</c>.
/// </summary>
/// <remarks>
/// Path-based terminators own the file stream they create and dispose it after the run completes
/// (success or failure). Stream-based terminators do not dispose — the caller owns the stream.
/// </remarks>
public static class EtlPipelineJsonSinkExtensions
{
    /// <summary>
    /// Terminates the pipeline, writing each record as a line of JSONL to a file.
    /// </summary>
    /// <typeparam name="T">The record type to serialize.</typeparam>
    /// <param name="pipeline">The pipeline to terminate.</param>
    /// <param name="path">The path of the JSONL file to write.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>A runnable <see cref="IEtlPipelineSink"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pipeline"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    public static IEtlPipelineSink JsonLineLoader<T>
    (
        this IEtlPipeline<T> pipeline,
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

        var stream = File.Create(path);
        var loader = options is null
            ? new JsonLineLoader<T>(stream)
            : new JsonLineLoader<T>(stream, options);
        return pipeline.To(loader).DisposingOwned(stream);
    }


    /// <summary>
    /// Terminates the pipeline, writing each record as a line of JSONL to a stream. The caller owns the stream.
    /// </summary>
    /// <typeparam name="T">The record type to serialize.</typeparam>
    /// <param name="pipeline">The pipeline to terminate.</param>
    /// <param name="stream">The stream to write JSONL to.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>A runnable <see cref="IEtlPipelineSink"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pipeline"/> or <paramref name="stream"/> is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    public static IEtlPipelineSink JsonLineLoader<T>
    (
        this IEtlPipeline<T> pipeline,
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

        var loader = options is null
            ? new JsonLineLoader<T>(stream)
            : new JsonLineLoader<T>(stream, options);
        return pipeline.To(loader);
    }


    /// <summary>
    /// Terminates the pipeline, writing all records as a single JSON array to a file.
    /// </summary>
    /// <typeparam name="T">The record type to serialize.</typeparam>
    /// <param name="pipeline">The pipeline to terminate.</param>
    /// <param name="path">The path of the JSON array file to write.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>A runnable <see cref="IEtlPipelineSink"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pipeline"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    public static IEtlPipelineSink JsonSingleStreamLoader<T>
    (
        this IEtlPipeline<T> pipeline,
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

        var stream = File.Create(path);
        var loader = options is null
            ? new JsonSingleStreamLoader<T>(stream)
            : new JsonSingleStreamLoader<T>(stream, options);
        return pipeline.To(loader).DisposingOwned(stream);
    }


    /// <summary>
    /// Terminates the pipeline, writing all records as a single JSON array to a stream. The caller owns the stream.
    /// </summary>
    /// <typeparam name="T">The record type to serialize.</typeparam>
    /// <param name="pipeline">The pipeline to terminate.</param>
    /// <param name="stream">The stream to write the JSON array to.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>A runnable <see cref="IEtlPipelineSink"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pipeline"/> or <paramref name="stream"/> is <see langword="null"/>.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("JSON serialization of unknown types may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("JSON serialization of unknown types may require types that cannot be statically analyzed.")]
#endif
    public static IEtlPipelineSink JsonSingleStreamLoader<T>
    (
        this IEtlPipeline<T> pipeline,
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

        var loader = options is null
            ? new JsonSingleStreamLoader<T>(stream)
            : new JsonSingleStreamLoader<T>(stream, options);
        return pipeline.To(loader);
    }
}
