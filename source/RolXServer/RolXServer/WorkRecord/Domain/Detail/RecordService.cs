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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RolXServer.Common.DataAccess;
using RolXServer.WorkRecord.DataAccess;
using RolXServer.WorkRecord.Domain.Model;

namespace RolXServer.WorkRecord.Domain.Detail
{
    /// <summary>
    /// Provides access to records.
    /// </summary>
    public sealed class RecordService : IRecordService
    {
        private readonly IHolidayRules holidayRules;
        private readonly IRepository<UserSetting> userSettingRepository;
        private readonly Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordService" /> class.
        /// </summary>
        /// <param name="holidayRules">The holiday rules.</param>
        /// <param name="userSettingRepository">The user setting repository.</param>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public RecordService(
            IHolidayRules holidayRules,
            IRepository<UserSetting> userSettingRepository,
            IOptions<Settings> settingsAccessor)
        {
            this.holidayRules = holidayRules;
            this.userSettingRepository = userSettingRepository;
            this.settings = settingsAccessor.Value;
        }

        /// <summary>
        /// Gets all records of the specified month, of the user with the specified identifier.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The records.
        /// </returns>
        public async Task<IEnumerable<Record>> GetAllOfMonth(DateTime month, Guid userId)
        {
            var result = AllDaysOfSameMonth(month)
                .Select(d => new Record { Date = d })
                .Select(r => ApplyWeekends(r))
                .Select(r => this.holidayRules.Apply(r))
                .Select(r => this.ApplyNominalWorkTime(r));

            return await this.ApplyPartTimeFactor(result.ToList(), userId);
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

        private static Record ApplyPartTimeFactor(Record record, IEnumerable<UserSetting> settings)
        {
            var factor = settings
                .Where(s => s.StartDate <= record.Date)
                .Select(s => s.PartTimeFactor)
                .DefaultIfEmpty(1.0)
                .First();

            record.NominalWorkTime *= factor;

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

        private async Task<IEnumerable<Record>> ApplyPartTimeFactor(IList<Record> records, Guid userId)
        {
            var lastDate = records.Max(r => r.Date);
            var settings = await this.userSettingRepository.Entities
                .Where(s => s.UserId == userId && s.StartDate <= lastDate)
                .OrderByDescending(s => s.StartDate)
                .Take(31)
                .ToListAsync();

            return records.Select(r => ApplyPartTimeFactor(r, settings));
        }
    }
}
