// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Common.Util;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/> instances.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Converts the specified source into a lazy enumerated stream.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="serializer">The serializer.</param>
    /// <returns>The stream.</returns>
    public static Stream ToStream<T>(this IEnumerable<T> source, Func<T, IEnumerable<byte>> serializer)
        => new EnumeratingStream<T>(source, serializer);
}
