// -----------------------------------------------------------------------
// <copyright file="TimeSpanExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Common.Util;

/// <summary>
/// Extension methods for <see cref="TimeSpan"/> instances.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Calculates the sum over the specified time spans.
    /// </summary>
    /// <param name="timeSpans">The time spans.</param>
    /// <returns>The sum.</returns>
    public static TimeSpan Sum(this IEnumerable<TimeSpan> timeSpans)
    {
        return new TimeSpan(timeSpans.Sum(s => s.Ticks));
    }

    /// <summary>
    /// Calculates the sum over time spans.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="items">The items.</param>
    /// <param name="accessor">The accessor getting the time span from the items.</param>
    /// <returns>
    /// The sum.
    /// </returns>
    public static TimeSpan Sum<T>(this IEnumerable<T> items, Func<T, TimeSpan> accessor)
    {
        return items.Select(accessor).Sum();
    }
}
