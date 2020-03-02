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
            var user = await this.dbContext.Users
                .Include(u => u.Settings)
                .SingleAsync(u => u.Id == userId);

            if (!user.EntryDate.HasValue)
            {
                throw new InvalidOperationException("Only users with valid entry date may have a balance.");
            }

            var endDate = date.AddDays(1);
            var nominalWorkTime = user.NominalWorkTime(
                new DateRange(user.EntryDate.Value, endDate),
                this.settings.NominalWorkTimePerDay);

            var actualWorkTime = TimeSpan.FromSeconds(
               await this.dbContext.Records
                   .Where(r => r.UserId == userId && r.Date < endDate)
                   .SelectMany(r => r.Entries)
                   .SumAsync(e => e.DurationSeconds));

            return new Balance
            {
                ByDate = date,
                Overtime = actualWorkTime - nominalWorkTime,
            };
        }
    }
}
