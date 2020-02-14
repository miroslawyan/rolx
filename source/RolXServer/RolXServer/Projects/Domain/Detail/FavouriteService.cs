// -----------------------------------------------------------------------
// <copyright file="FavouriteService.cs" company="Christian Ewald">
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
using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain.Detail
{
    /// <summary>
    /// Provides access to favourite <see cref="Phase"/> instances.
    /// </summary>
    internal sealed class FavouriteService : IFavouriteService
    {
        private readonly RolXContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavouriteService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FavouriteService(RolXContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all favourite phases of the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The favourite phases.
        /// </returns>
        public async Task<IEnumerable<Phase>> GetAll(Guid userId)
        {
            return await this.context.FavouritePhases
                .Where(f => f.UserId == userId)
                .Select(f => f.Phase)
                .ToListAsync();
        }

        /// <summary>
        /// Adds the specified phase to the favourites of the specified user.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The async task.
        /// </returns>
        public async Task Add(Phase phase, Guid userId)
        {
            this.context.FavouritePhases.Add(ToEntity(phase, userId));
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the specified phase from the favourites of the specified user.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The async task.
        /// </returns>
        public async Task Remove(Phase phase, Guid userId)
        {
            this.context.FavouritePhases.Remove(ToEntity(phase, userId));
            await this.context.SaveChangesAsync();
        }

        private static FavouritePhase ToEntity(Phase phase, Guid userId) => new FavouritePhase
        {
            UserId = userId,
            PhaseId = phase.Id,
        };
    }
}
