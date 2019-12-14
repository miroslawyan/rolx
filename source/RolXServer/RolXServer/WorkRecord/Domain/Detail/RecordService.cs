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
using RolXServer.Auth.DataAccess;
using RolXServer.Common.Util;
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
        /// Creates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>The created record.</returns>
        public async Task<Record> Create(Record record)
        {
            if (record.Id != 0)
            {
                throw new ArgumentException("Record must not have an id while creating", nameof(record));
            }

            record.Sanitize();

            var entity = record.ToEntity();

            this.dbContext.Add(entity);
            await this.dbContext.SaveChangesAsync();

            record.Id = entity.Id;
            return record;
        }

        /// <summary>
        /// Updates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>The async task.</returns>
        public async Task Update(Record record)
        {
            if (record.Id == 0)
            {
                throw new ArgumentException("Record must have a valid id while updating", nameof(record));
            }

            record.Sanitize();

            this.dbContext.Records.Update(record.ToEntity());

            var entryIds = record.Entries
                .Select(e => e.Id);

            this.dbContext.RemoveRange(
                this.dbContext.Records
                .Where(r => r.Id == record.Id)
                .SelectMany(p => p.Entries)
                .Where(e => !entryIds.Contains(e.Id)));

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
