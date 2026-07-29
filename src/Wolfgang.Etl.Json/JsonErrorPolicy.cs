using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Wolfgang.Etl.Abstractions;

namespace Wolfgang.Etl.Json;

/// <summary>
/// Ready-made error policies for the JSON extractors' <c>OnError</c> hook. Each is a
/// <see cref="Func{T, TResult}"/> from an <see cref="ItemErrorContext"/> to an
/// <see cref="ItemErrorAction"/>, so it can be assigned directly:
/// <example><code>
/// var deadLetters = new List&lt;ItemErrorContext&gt;();
/// var extractor = new JsonLineExtractor&lt;Person&gt;(stream)
/// {
///     OnError = JsonErrorPolicy.SkipDeadLetterAndLog(deadLetters, logger)
/// };
/// </code></example>
/// The dead-letter overloads write to a caller-owned collection, so its size (and therefore the
/// memory a bad feed can consume) stays under the caller's control.
/// </summary>
public static class JsonErrorPolicy
{
    /// <summary>
    /// A policy that discards the failed record and continues with the next one. The stage's
    /// <see cref="ExtractorBase{TSource, TProgress}.CurrentErrorItemCount"/> is incremented so the
    /// skip is never silent.
    /// </summary>
    public static Func<ItemErrorContext, ItemErrorAction> Skip { get; } = _ => ItemErrorAction.Skip;



    /// <summary>
    /// A policy that re-throws the failure and stops the run. Equivalent to leaving <c>OnError</c>
    /// unset, provided for symmetry and explicitness.
    /// </summary>
    public static Func<ItemErrorContext, ItemErrorAction> Abort { get; } = _ => ItemErrorAction.Abort;



    /// <summary>
    /// Logs the failure as a warning through <paramref name="logger"/>, then discards the record and
    /// continues.
    /// </summary>
    /// <param name="logger">The logger the returned policy writes each failure to.</param>
    /// <returns>A policy that logs and returns <see cref="ItemErrorAction.Skip"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="logger"/> is <see langword="null"/>.</exception>
    public static Func<ItemErrorContext, ItemErrorAction> SkipAndLog(ILogger logger)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        return context =>
        {
            JsonLogMessages.DeserializationErrorAtIndex(logger, context.ItemNumber, context.Exception);
            return ItemErrorAction.Skip;
        };
    }



    /// <summary>
    /// Records the failure in <paramref name="deadLetters"/> (a "dead-letter" queue the caller owns),
    /// then discards the record and continues.
    /// </summary>
    /// <param name="deadLetters">The caller-owned collection each failed item is added to.</param>
    /// <returns>A policy that dead-letters and returns <see cref="ItemErrorAction.Skip"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="deadLetters"/> is <see langword="null"/>.</exception>
    public static Func<ItemErrorContext, ItemErrorAction> SkipAndDeadLetter(ICollection<ItemErrorContext> deadLetters)
    {
        if (deadLetters is null)
        {
            throw new ArgumentNullException(nameof(deadLetters));
        }

        return context =>
        {
            deadLetters.Add(context);
            return ItemErrorAction.Skip;
        };
    }



    /// <summary>
    /// Writes the failure to <paramref name="deadLetters"/> (a caller-owned channel) with
    /// <see cref="ChannelWriter{T}.TryWrite(T)"/>, then discards the record and continues. Because the
    /// hook is synchronous the non-blocking <c>TryWrite</c> is used, so a bounded channel that is full
    /// drops the failure — size the channel, or use <see cref="BoundedChannelFullMode"/>, accordingly.
    /// </summary>
    /// <param name="deadLetters">The caller-owned channel each failed item is written to.</param>
    /// <returns>A policy that dead-letters and returns <see cref="ItemErrorAction.Skip"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="deadLetters"/> is <see langword="null"/>.</exception>
    public static Func<ItemErrorContext, ItemErrorAction> SkipAndDeadLetter(ChannelWriter<ItemErrorContext> deadLetters)
    {
        if (deadLetters is null)
        {
            throw new ArgumentNullException(nameof(deadLetters));
        }

        return context =>
        {
            deadLetters.TryWrite(context);
            return ItemErrorAction.Skip;
        };
    }



    /// <summary>
    /// Records the failure in <paramref name="deadLetters"/> and logs it as a warning through
    /// <paramref name="logger"/>, then discards the record and continues.
    /// </summary>
    /// <param name="deadLetters">The caller-owned collection each failed item is added to.</param>
    /// <param name="logger">The logger the returned policy writes each failure to.</param>
    /// <returns>A policy that dead-letters, logs, and returns <see cref="ItemErrorAction.Skip"/>.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="deadLetters"/> or <paramref name="logger"/> is <see langword="null"/>.
    /// </exception>
    public static Func<ItemErrorContext, ItemErrorAction> SkipDeadLetterAndLog
    (
        ICollection<ItemErrorContext> deadLetters,
        ILogger logger
    )
    {
        if (deadLetters is null)
        {
            throw new ArgumentNullException(nameof(deadLetters));
        }

        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        return context =>
        {
            deadLetters.Add(context);
            JsonLogMessages.DeserializationErrorAtIndex(logger, context.ItemNumber, context.Exception);
            return ItemErrorAction.Skip;
        };
    }



    /// <summary>
    /// Writes the failure to <paramref name="deadLetters"/> with
    /// <see cref="ChannelWriter{T}.TryWrite(T)"/> and logs it as a warning through
    /// <paramref name="logger"/>, then discards the record and continues. See
    /// <see cref="SkipAndDeadLetter(ChannelWriter{ItemErrorContext})"/> for the <c>TryWrite</c> caveat.
    /// </summary>
    /// <param name="deadLetters">The caller-owned channel each failed item is written to.</param>
    /// <param name="logger">The logger the returned policy writes each failure to.</param>
    /// <returns>A policy that dead-letters, logs, and returns <see cref="ItemErrorAction.Skip"/>.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="deadLetters"/> or <paramref name="logger"/> is <see langword="null"/>.
    /// </exception>
    public static Func<ItemErrorContext, ItemErrorAction> SkipDeadLetterAndLog
    (
        ChannelWriter<ItemErrorContext> deadLetters,
        ILogger logger
    )
    {
        if (deadLetters is null)
        {
            throw new ArgumentNullException(nameof(deadLetters));
        }

        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        return context =>
        {
            deadLetters.TryWrite(context);
            JsonLogMessages.DeserializationErrorAtIndex(logger, context.ItemNumber, context.Exception);
            return ItemErrorAction.Skip;
        };
    }
}
