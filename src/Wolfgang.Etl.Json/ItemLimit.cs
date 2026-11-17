using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Wolfgang.Etl.Json;



/// <summary>
/// Stops an upstream sequence from being advanced once a stage's <c>MaximumItemCount</c> is reached, so the
/// loaders never pull (and thereby produce, count or fail on) an item beyond the limit.
/// </summary>
internal static class ItemLimit
{
    /// <summary>
    /// Yields the items of <paramref name="source"/> until <paramref name="limitReached"/> returns <see langword="true"/>.
    /// The predicate is evaluated before the source is asked for its next item.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="source">The upstream sequence.</param>
    /// <param name="limitReached">Returns <see langword="true"/> once no further item may be consumed.</param>
    /// <param name="token">The cancellation token passed to the upstream enumerator.</param>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    internal static IAsyncEnumerable<T> Until<T>
    (
        IAsyncEnumerable<T>? source,
        Func<bool> limitReached,
        CancellationToken token = default
    )
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return UntilIterator(source, limitReached, token);
    }



    private static async IAsyncEnumerable<T> UntilIterator<T>
    (
        IAsyncEnumerable<T> source,
        Func<bool> limitReached,
        [EnumeratorCancellation] CancellationToken token
    )
    {
        var enumerator = source.GetAsyncEnumerator(token);
        try
        {
            while (!limitReached() && await enumerator.MoveNextAsync().ConfigureAwait(false))
            {
                yield return enumerator.Current;
            }
        }
        finally
        {
            await enumerator.DisposeAsync().ConfigureAwait(false);
        }
    }
}
