// -----------------------------------------------------------------------
// <copyright file="UpdatableUserMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;

namespace RolXServer.Users.WebApi.Mapping
{
    /// <summary>
    /// Maps updatable users from / to resource.
    /// </summary>
    internal static class UpdatableUserMapper
    {
        /// <summary>
        /// Converts to domain.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>
        /// The domain.
        /// </returns>
        public static Domain.Model.UpdatableUser ToDomain(this Resource.UpdatableUser resource)
        {
            return new Domain.Model.UpdatableUser
            {
                Id = resource.Id,
                Role = resource.Role,
                EntryDate = IsoDate.ParseNullable(resource.EntryDate),
            };
        }
    }
}
