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

using Microsoft.Extensions.Options;
using RolXServer.WorkRecord.Domain.Model;

namespace RolXServer.WorkRecord.Domain.Detail
{
    /// <summary>
    /// Provides access to records.
    /// </summary>
    public sealed class RecordService : IRecordService
    {
        private readonly IHolidayRules holidayRules;
        private readonly Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordService" /> class.
        /// </summary>
        /// <param name="holidayRules">The holiday rules.</param>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public RecordService(IHolidayRules holidayRules, IOptions<Settings> settingsAccessor)
        {
            this.holidayRules = holidayRules;
            this.settings = settingsAccessor.Value;
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
                .Select(r => ApplyWeekends(r))
                .Select(r => this.holidayRules.Apply(r))
                .Select(r => this.ApplyNominalWorkTime(r));

            return Task.FromResult(result);
        }

        private static IEnumerable<DateTime> AllDaysOfSameMonth(DateTime month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(month.Year, month.Month))
                .Select(d => new DateTime(month.Year, month.Month, d));
        }

        private static Record ApplyWeekends(Record record)
        {
            if (record.Date.DayOfWeek == DayOfWeek.Saturday || record.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                record.DayType = DayType.Weekend;
            }

            return record;
        }

        private Record ApplyNominalWorkTime(Record record)
        {
            if (record.DayType == DayType.Workday)
            {
                record.NominalWorkTime = this.settings.NominalWorkTimePerDay;
            }

            return record;
        }
    }
}
