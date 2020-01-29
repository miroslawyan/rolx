// -----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using RolXServer.Auth.DataAccess;

namespace RolXServer.UserManagement.Domain
{
    /// <summary>
    /// Provides access to <see cref="User"/> instances.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>
        /// The Users.
        /// </returns>
        Task<IEnumerable<User>> GetAll();
    }
}
