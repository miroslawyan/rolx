// -----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace RolXServer.Common.DataAccess
{
    /// <summary>
    /// Repository providing access to entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets the entities.
        /// </summary>
        DbSet<TEntity> Entities { get; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChanges();
    }
}
