// -----------------------------------------------------------------------
// <copyright file="RolXRepository.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RolXServer.Account.DataAccess;
using RolXServer.Auth.DataAccess;
using RolXServer.Common.DataAccess;
using RolXServer.WorkRecord.DataAccess;

namespace RolXServer.Database
{
    /// <summary>
    /// The repository in use.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "DI")]
    internal sealed class RolXRepository :
        IRepository<Customer>,
        IRepository<Project>,
        IRepository<User>,
        IRepository<UserSetting>
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
        /// Gets the customer entities.
        /// </summary>
        DbSet<Customer> IRepository<Customer>.Entities => this.context.Customers;

        /// <summary>
        /// Gets the project entities.
        /// </summary>
        DbSet<Project> IRepository<Project>.Entities => this.context.Projects;

        /// <summary>
        /// Gets the user entities.
        /// </summary>
        DbSet<User> IRepository<User>.Entities => this.context.Users;

        /// <summary>
        /// Gets the user setting entities.
        /// </summary>
        DbSet<UserSetting> IRepository<UserSetting>.Entities => this.context.UserSettings;

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        public Task<int> SaveChanges() => this.context.SaveChangesAsync();
    }
}
