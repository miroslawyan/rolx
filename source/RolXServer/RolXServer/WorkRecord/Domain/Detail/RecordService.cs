// -----------------------------------------------------------------------
// <copyright file="RecordService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RolXServer.WorkRecord.Domain.Model;

namespace RolXServer.WorkRecord.Domain.Detail
{
    /// <summary>
    /// Provides access to records.
    /// </summary>
    public sealed class RecordService : IRecordService
    {
        private readonly IHolidayRules holidayRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordService"/> class.
        /// </summary>
        /// <param name="holidayRules">The holiday rules.</param>
        public RecordService(IHolidayRules holidayRules)
        {
            this.holidayRules = holidayRules;
        }

        /// <summary>
        /// Gets all records of the specified month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns>
        /// The records.
        /// </returns>
        public Task<IEnumerable<Record>> GetAllOfMonth(DateTime month)
        {
            var result = AllDaysOfSameMonth(month)
                .Select(d => new Record { Date = d })
                .Select(r => this.holidayRules.Apply(r));

            return Task.FromResult(result);
        }

        private static IEnumerable<DateTime> AllDaysOfSameMonth(DateTime month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(month.Year, month.Month))
                .Select(d => new DateTime(month.Year, month.Month, d));
        }
    }
}
