// -----------------------------------------------------------------------
// <copyright file="VacationBudgetEvaluation.cs" company="Christian Ewald">
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
    /// Extension methods for <see cref="DateRange"/> instances.
    /// </summary>
    internal static class VacationBudgetEvaluation
    {
        /// <summary>
        /// Gets the vacation budget for the specified user up to the specified year.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="year">The year.</param>
        /// <param name="vacationDaysPerYear">The vacation days per year.</param>
        /// <param name="nominalWorkTimePerDay">The nominal work time per day.</param>
        /// <returns>The vacation budget.</returns>
        public static TimeSpan VacationBudget(this User user, int year, int vacationDaysPerYear, TimeSpan nominalWorkTimePerDay)
        {
            return new TimeSpan(
                user.RangesWithPartTimeFactor(year)
                    .Select(t => t.Item1.VacationDays(vacationDaysPerYear) * t.Item2 * nominalWorkTimePerDay)
                    .Sum(s => s.Ticks));
        }

        /// <summary>
        /// Gets the number of vacation days for the specified range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="vacationDaysPerYear">The number of vacation days per year.</param>
        /// <returns>The number of vacation days.</returns>
        public static double VacationDays(this DateRange range, int vacationDaysPerYear)
        {
            return range.TotalMonths() * vacationDaysPerYear / 12.0;
        }

        private static IEnumerable<(DateRange, double)> RangesWithPartTimeFactor(this User user, int year)
        {
            if (!user.EntryDate.HasValue)
            {
                throw new InvalidOperationException("User must have an entry date.");
            }

            var lastStartDate = user.EntryDate.Value;
            var lastFactor = 1.0;

            foreach (var setting in user.Settings)
            {
                if (setting.StartDate > lastStartDate)
                {
                    yield return (new DateRange(lastStartDate, setting.StartDate), lastFactor);

                    lastFactor = setting.PartTimeFactor;
                    lastStartDate = setting.StartDate;
                }

                lastFactor = setting.PartTimeFactor;
            }

            var endDate = user.LeavingDate.HasValue && user.LeavingDate.Value.Year == year
                ? user.LeavingDate.Value.AddDays(1)
                : new DateTime(year + 1, 1, 1);

            yield return (new DateRange(lastStartDate, endDate), lastFactor);
        }

        private static double TotalMonths(this DateRange range)
        {
            return (range.End.AbsoluteMonths() - range.Begin.AbsoluteMonths())
                - range.Begin.DayInMonthFactor()
                + range.End.DayInMonthFactor();
        }

        private static int AbsoluteMonths(this DateTime date)
        {
            return (date.Year * 12) + date.Month - 1;
        }

        private static double DayInMonthFactor(this DateTime date)
        {
            return (date.Day - 1.0) / DateTime.DaysInMonth(date.Year, date.Month);
        }
    }
}
