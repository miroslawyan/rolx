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
using RolXServer.Records.Domain.Detail.Balances;
using RolXServer.Records.Domain.Mapping;
using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain.Detail
{
    /// <summary>
    /// Provides access to records.
    /// </summary>
    public sealed class RecordService : IRecordService
    {
        private readonly RolXContext dbContext;
        private readonly Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordService" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public RecordService(
            RolXContext dbContext,
            IOptions<Settings> settingsAccessor)
        {
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
            var user = await this.dbContext.Users
                .Include(u => u.PartTimeSettings)
                .SingleAsync(u => u.Id == userId);

            var entities = await this.dbContext.Records
                .Include(r => r.Entries).ThenInclude(e => e.Phase)
                .Where(r => r.Date >= range.Begin && r.Date < range.End && r.UserId == userId)
                .OrderBy(r => r.Date)
                .ToListAsync();

            return user.DayInfos(range, this.settings.NominalWorkTimePerDay)
                .ToRecords(userId, entities);
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
                .SingleOrDefaultAsync(r => r.Date == record.DayInfo.Date && r.UserId == record.UserId);

            if (record.IsEmpty)
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
                record.Update(entity);
            }
            else
            {
                entity = record.ToEntity();
                this.dbContext.Records.Add(entity);
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
