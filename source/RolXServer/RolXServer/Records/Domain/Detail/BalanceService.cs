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
using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain.Detail
{
    /// <summary>
    /// The balance service.
    /// </summary>
    public sealed class BalanceService : IBalanceService
    {
        private readonly RolXContext dbContext;
        private readonly IDayInfoService dayInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalanceService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="dayInfoService">The day information service.</param>
        public BalanceService(
            RolXContext dbContext,
            IDayInfoService dayInfoService)
        {
            this.dbContext = dbContext;
            this.dayInfoService = dayInfoService;
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

            var endDate = date.AddDays(1);
            var nominalWorkTime = this.dayInfoService.GetNominalWorkTime(user, endDate);

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
