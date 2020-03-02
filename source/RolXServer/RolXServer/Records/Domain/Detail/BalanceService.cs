// -----------------------------------------------------------------------
// <copyright file="BalanceService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RolXServer.Common.Util;
using RolXServer.Records.Domain.Detail.Balances;
using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain.Detail
{
    /// <summary>
    /// The balance service.
    /// </summary>
    public sealed class BalanceService : IBalanceService
    {
        private readonly RolXContext dbContext;
        private readonly Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalanceService" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="settingsAccessor">The settings accessor.</param>
        public BalanceService(RolXContext dbContext, IOptions<Settings> settingsAccessor)
        {
            this.dbContext = dbContext;
            this.settings = settingsAccessor.Value;
        }

        /// <summary>
        /// Gets the balance of the specified user by the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The balance.
        /// </returns>
        public async Task<Balance> GetByDate(DateTime date, Guid userId)
        {
            var data = await this.dbContext.Users
                .Include(u => u.Settings)
                .Where(u => u.Id == userId)
                .Select(u => new BalanceData
                {
                    User = u,

                    ActualWorkTimeSeconds = u.Records
                        .Where(r => r.Date <= date)
                        .SelectMany(r => r.Entries)
                        .Sum(e => e.DurationSeconds),

                    PaidLeaveDays = u.Records
                        .Where(r => r.Date <= date && r.PaidLeaveType != null)
                        .Select(r => new PaidLeaveDay
                        {
                            Date = r.Date,
                            ActualWorkTimeSeconds = r.Entries.Sum(e => e.DurationSeconds),
                        }).ToList(),
                })
                .SingleAsync();

            data.ByDate = date;
            data.NominalWorkTimePerDay = this.settings.NominalWorkTimePerDay;

            return data.ToBalance();
        }
    }
}
