// -----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RolXServer.Common.Errors;
using RolXServer.Users.DataAccess;
using RolXServer.Users.Domain.Model;

namespace RolXServer.Users.Domain.Detail
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

        /// <summary>
        /// Gets a user by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user or <c>null</c> if none has been found.</returns>
        public async Task<User?> GetById(Guid id)
        {
            return await this.context.Users
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The async task.</returns>
        public async Task Update(UpdatableUser user)
        {
            var entity = new User
            {
                Id = user.Id,
                Role = user.Role,
                EntryDate = user.EntryDate,
                LeftDate = user.LeftDate,
            };

            var entry = this.context.Users.Attach(entity);
            entry.Property(e => e.Role).IsModified = true;
            entry.Property(e => e.EntryDate).IsModified = true;
            entry.Property(e => e.LeftDate).IsModified = true;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ItemNotFoundException($"No user with id '{user.Id}' found.", e);
            }
        }
    }
}
