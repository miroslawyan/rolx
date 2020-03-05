// -----------------------------------------------------------------------
// <copyright file="PaidLeaveDayExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using RolXServer.Common.Util;
using RolXServer.Users.DataAccess;

namespace RolXServer.Records.Domain.Detail.Balances
{
    /// <summary>
    /// Extension methods for <see cref="PaidLeaveDay"/> instances.
    /// </summary>
    internal static class PaidLeaveDayExtensions
    {
        /// <summary>
        /// Evaluates the sum of paid leave time over the specified paid leave days.
        /// </summary>
        /// <param name="paidLeaveDays">The paid leave days.</param>
        /// <param name="user">The user.</param>
        /// <param name="nominalWorkTimePerDay">The nominal work time per day.</param>
        /// <returns>The sum of paid leave time.</returns>
        public static TimeSpan SumOfPaidLeaveTime(this IEnumerable<PaidLeaveDay> paidLeaveDays, User user, TimeSpan nominalWorkTimePerDay)
        {
            return paidLeaveDays
                .Sum(d => user.NominalWorkTime(d.Date, nominalWorkTimePerDay) - d.ActualWorkTime);
        }
    }
}
