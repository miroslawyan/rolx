// -----------------------------------------------------------------------
// <copyright file="UserMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Auth.Domain.Detail;
using RolXServer.Users.DataAccess;

namespace RolXServer.Auth.Domain.Mapping
{
    /// <summary>
    /// Maps authenticated users from / to resource.
    /// </summary>
    internal static class UserMapper
    {
        /// <summary>
        /// Converts to domain.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns>
        /// The domain.
        /// </returns>
        public static Model.AuthenticatedUser ToDomain(this User entity, BearerToken bearerToken)
        {
            return new Model.AuthenticatedUser
            {
                Id = entity.Id,
                GoogleId = entity.GoogleId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                AvatarUrl = entity.AvatarUrl,
                Role = entity.Role,
                EntryDate = entity.EntryDate,
                LeavingDate = entity.LeavingDate,
                BearerToken = bearerToken.Token,
                Expires = bearerToken.Expires,
            };
        }
    }
}
