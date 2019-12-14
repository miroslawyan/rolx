// -----------------------------------------------------------------------
// <copyright file="PhaseService.cs" company="Christian Ewald">
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
using RolXServer.Account.DataAccess;

namespace RolXServer.Account.Domain.Detail
{
    /// <summary>
    /// Provides access to <see cref="Phase"/> instances.
    /// </summary>
    internal sealed class PhaseService : IPhaseService
    {
        private readonly RolXContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhaseService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PhaseService(RolXContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all phases.
        /// </summary>
        /// <returns>
        /// The phases.
        /// </returns>
        public async Task<IEnumerable<Phase>> GetAll()
        {
            return await this.context.Phases.ToListAsync();
        }

        /// <summary>
        /// Gets the suitable phases for the specified user at the specified date.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The suitable phases.
        /// </returns>
        public async Task<IEnumerable<Phase>> GetSuitable(Guid userId, DateTime date)
        {
            var begin = date.AddMonths(-2);
            var end = date.AddMonths(1);

            return await this.context.Records
                .Where(r => r.UserId == userId && r.Date >= begin && r.Date < end)
                .SelectMany(r => r.Entries)
                .Select(e => e.Phase)
                .Distinct()
                .ToListAsync();
        }
    }
}
