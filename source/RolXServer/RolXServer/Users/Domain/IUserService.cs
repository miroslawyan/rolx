// -----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Users.DataAccess;
using RolXServer.Users.Domain.Model;

namespace RolXServer.Users.Domain;

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

    /// <summary>
    /// Gets a user by the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>The user or <c>null</c> if none has been found.</returns>
    Task<User?> GetById(Guid id);

    /// <summary>
    /// Updates the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The async task.</returns>
    Task Update(UpdatableUser user);
}
