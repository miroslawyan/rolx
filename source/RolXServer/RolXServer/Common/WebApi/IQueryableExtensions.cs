// -----------------------------------------------------------------------
// <copyright file="IQueryableExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace RolXServer.Common.WebApi;

/// <summary>
/// Extensions for <see cref="IQueryable"/>.
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// Asynchronously returns the first element of a sequence,
    /// or throws <see cref="NotFoundException"/> if the sequence contains no elements.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="NotFoundException"> Thrown if if the sequence contains no elements. </exception>
    public static async Task<TSource> FirstOrThrowNotFoundAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
    {
        var result = await source.FirstOrDefaultAsync(cancellationToken);
        if (result == null)
        {
            throw new NotFoundException($"No {typeof(TSource).Name} found for the given query");
        }

        return result;
    }

    /// <summary>
    /// Asynchronously returns the first element of a sequence that satisfies a specified condition,
    /// or throws <see cref="NotFoundException"/> if the sequence contains no elements.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="NotFoundException"> Thrown if if the sequence contains no elements. </exception>
    public static async Task<TSource> FirstOrThrowNotFoundAsync<TSource>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var result = await source.FirstOrDefaultAsync(predicate, cancellationToken);
        if (result == null)
        {
            throw new NotFoundException($"No {typeof(TSource).Name} found for the given query");
        }

        return result;
    }
}
