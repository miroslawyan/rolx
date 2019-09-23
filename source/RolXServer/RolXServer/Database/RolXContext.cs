// -----------------------------------------------------------------------
// <copyright file="RolXContext.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using RolXServer.Auth.DataAccess.Entity;

namespace RolXServer.Database
{
    /// <summary>
    /// The database context in use.
    /// </summary>
    /// <seealso cref="DbContext" />
    internal sealed class RolXContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RolXContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public RolXContext(DbContextOptions<RolXContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
