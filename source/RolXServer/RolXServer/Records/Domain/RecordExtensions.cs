// -----------------------------------------------------------------------
// <copyright file="RecordExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;
using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain;

/// <summary>
/// Extension methods for <see cref="Record"/> instances.
/// </summary>
public static class RecordExtensions
{
    /// <summary>
    /// Gets the paid-leave time of the specified record.
    /// </summary>
    /// <param name="record">The record.</param>
    /// <returns>paid-leave time.</returns>
    public static TimeSpan PaidLeaveTime(this Record record)
        => record.PaidLeaveType.HasValue
        ? record.DayInfo.NominalWorkTime - record.Entries.Sum(entry => entry.Duration)
        : TimeSpan.Zero;
}
