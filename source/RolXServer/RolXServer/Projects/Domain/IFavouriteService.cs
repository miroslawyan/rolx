// -----------------------------------------------------------------------
// <copyright file="IFavouriteService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain
{
    /// <summary>
    /// Provides access to favourite <see cref="Phase"/> instances.
    /// </summary>
    public interface IFavouriteService
    {
        /// <summary>
        /// Gets all favourite phases of the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The favourite phases.
        /// </returns>
        Task<IEnumerable<Phase>> GetAll(Guid userId);

        /// <summary>
        /// Adds the specified phase to the favourites of the specified user.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The async task.</returns>
        Task Add(Phase phase, Guid userId);

        /// <summary>
        /// Removes the specified phase from the favourites of the specified user.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The async task.</returns>
        Task Remove(Phase phase, Guid userId);
    }
}
