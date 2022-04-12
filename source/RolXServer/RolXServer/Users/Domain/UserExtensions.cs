// -----------------------------------------------------------------------
// <copyright file="UserExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;
using RolXServer.Users.DataAccess;

namespace RolXServer.Users.Domain;

/// <summary>
/// Extensions methods for <see cref="User"/> instances.
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Gets the full name of the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The full name.</returns>
    public static string FullName(this User user)
        => $"{user.FirstName} {user.LastName}";

    /// <summary>
    /// Gets the users part-time settings before the specified date, in descending order.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="date">The date.</param>
    /// <returns>The part-time settings.</returns>
    public static IEnumerable<UserPartTimeSetting> PartTimeSettingsDescendingBefore(this User user, DateOnly date)
        => user.PartTimeSettings
        .Where(s => s.StartDate < date)
        .OrderByDescending(s => s.StartDate)
        .Append(new UserPartTimeSetting
        {
            StartDate = user.EntryDate,
            Factor = 1,
        });

    /// <summary>
    /// Gets the users part-time settings in the specified range.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="range">The range.</param>
    /// <returns>The part-time settings.</returns>
    public static IEnumerable<UserPartTimeSetting> PartTimeSettingsFor(this User user, DateRange range)
    {
        var sortedSettings = user.PartTimeSettingsDescendingBefore(range.End).ToList();
        return sortedSettings.Take(sortedSettings.Count(s => s.StartDate >= range.Begin) + 1);
    }

    /// <summary>
    /// Gets the users part-time factor at the specified date.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="date">The date.</param>
    /// <returns>The part-time factor.</returns>
    public static double PartTimeFactorAt(this User user, DateOnly date)
        => user.PartTimeSettingsDescendingBefore(date.AddDays(1))
            .Select(s => s.Factor)
            .DefaultIfEmpty(1.0)
            .First();
}
