// -----------------------------------------------------------------------
// <copyright file="PaidLeaveTypeExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records;

/// <summary>
/// Extension methods for <see cref="PaidLeaveType"/> values.
/// </summary>
public static class PaidLeaveTypeExtensions
{
    /// <summary>
    /// Converts to the specified value to a human-readable string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The human-readable string.</returns>
    public static string ToPrettyString(this PaidLeaveType value)
        => value switch
        {
            PaidLeaveType.Vacation => "Ferien",
            PaidLeaveType.Sickness => "Krank",
            PaidLeaveType.MilitaryService => "Militär",
            PaidLeaveType.Other => "Sonstige",
            _ => value.ToString(),
        };
}
