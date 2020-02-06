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
using RolXServer.Common.Util;
using RolXServer.Users.DataAccess;
using RolXServer.WorkRecord.Domain.Mapping;
using RolXServer.WorkRecord.Domain.Model;

namespace RolXServer.WorkRecord.Domain.Detail
{
    /// <summary>
    /// Provides access to records.
    /// </summary>
    public sealed class RecordService : IRecordService
    {
        private readonly IHolidayRules holidayRules;
        private readonly RolXContext dbContext;
        private readonly Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordService" /> class.
        /// </summary>
        /// <param name="holidayRules">The holiday rules.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public RecordService(
            IHolidayRules holidayRules,
            RolXContext dbContext,
            IOptions<Settings> settingsAccessor)
        {
            this.holidayRules = holidayRules;
            this.dbContext = dbContext;
            this.settings = settingsAccessor.Value;
        }

        /// <summary>
        /// Gets all records of the specified range (begin..end], of the user with the specified identifier.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The requested records.
        /// </returns>
        public async Task<IEnumerable<Record>> GetRange(DateRange range, Guid userId)
        {
            var entities = await this.dbContext.Records
                .Include(r => r.Entries).ThenInclude(e => e.Phase)
                .Where(r => r.Date >= range.Begin && r.Date < range.End && r.UserId == userId)
                .OrderBy(r => r.Date)
                .ToListAsync();

            var result = entities.ToDomainsIn(range, userId)
                .Select(r => ApplyWeekends(r))
                .Select(r => this.holidayRules.Apply(r))
                .Select(r => this.ApplyNominalWorkTime(r));

            return await this.ApplyPartTimeFactor(result.ToList(), userId);
        }

        /// <summary>
        /// Updates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>The async task.</returns>
        public async Task Update(Record record)
        {
            record.Sanitize();

            var entity = await this.dbContext.Records
                .Include(r => r.Entries)
                .SingleOrDefaultAsync(r => r.Date == record.Date && r.UserId == record.UserId);

            if (record.Entries.Count == 0)
            {
                if (entity != null)
                {
                    this.dbContext.Records.Remove(entity);
                    await this.dbContext.SaveChangesAsync();
                }

                return;
            }

            if (entity != null)
            {
                entity.Entries.Clear();
                entity.Entries = record.Entries;
            }
            else
            {
                entity = record.ToEntity();
                this.dbContext.Records.Add(entity);
            }

            await this.dbContext.SaveChangesAsync();
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

        private async Task<IEnumerable<Record>> ApplyPartTimeFactor(IEnumerable<Record> records, Guid userId)
        {
            var settings = (await this.GetUserSettings(userId)).ToList();
            return records.Select(r => ApplyPartTimeFactor(r, settings));
        }

        private async Task<IEnumerable<UserSetting>> GetUserSettings(Guid userId)
        {
            // There usually are not too many setting entries.
            // So it shouldn't be a problem to just load them all.
            return await this.dbContext.UserSettings
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.StartDate)
                .ToListAsync();
        }
    }
}
