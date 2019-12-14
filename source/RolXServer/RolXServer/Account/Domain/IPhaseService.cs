// -----------------------------------------------------------------------
// <copyright file="IPhaseService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RolXServer.Account.DataAccess;

namespace RolXServer.Account.Domain
{
    /// <summary>
    /// Provides access to <see cref="Phase"/> instances.
    /// </summary>
    public interface IPhaseService
    {
        /// <summary>
        /// Gets all phases.
        /// </summary>
        /// <returns>The phases.</returns>
        Task<IEnumerable<Phase>> GetAll();

        /// <summary>
        /// Gets the suitable phases for the specified user at the specified date.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The suitable phases.
        /// </returns>
        Task<IEnumerable<Phase>> GetSuitable(Guid userId, DateTime date);
    }
}
