// -----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RolXServer.Auth.DataAccess;

namespace RolXServer.UserManagement.Domain.Detail
{
    /// <summary>
    /// Provides access to <see cref="User"/> instances.
    /// </summary>
    internal sealed class UserService : IUserService
    {
        private readonly RolXContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserService(RolXContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>
        /// The Users.
        /// </returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            return await this.context.Users.ToListAsync();
        }
    }
}
