// -----------------------------------------------------------------------
// <copyright file="RolXRepository.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RolXServer.Auth.DataAccess.Entity;
using RolXServer.Common.DataAccess;

namespace RolXServer.Database
{
    /// <summary>
    /// The repository in use.
    /// </summary>
    internal sealed class RolXRepository : IRepository<User>
    {
        private RolXContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolXRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RolXRepository(RolXContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the target entities.
        /// </summary>
        DbSet<User> IRepository<User>.Entities => this.context.Users;

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        public Task<int> SaveChanges() => this.context.SaveChangesAsync();
    }
}
