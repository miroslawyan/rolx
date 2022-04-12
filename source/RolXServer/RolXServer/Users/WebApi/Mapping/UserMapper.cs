// -----------------------------------------------------------------------
// <copyright file="UserMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;

namespace RolXServer.Users.WebApi.Mapping;

/// <summary>
/// Maps users from / to resource.
/// </summary>
internal static class UserMapper
{
    /// <summary>
    /// Converts the specified entity to resource.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The resource.</returns>
    public static Resource.User ToResource(this DataAccess.User entity)
    {
        return new Resource.User
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            AvatarUrl = entity.AvatarUrl,
            Role = entity.Role,
            EntryDate = entity.EntryDate.ToIsoDate(),
            LeftDate = entity.LeftDate?.ToIsoDate(),
            IsConfirmed = entity.IsConfirmed,
        };
    }

    /// <summary>
    /// Converts the specified resource to domain.
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
            EntryDate = IsoDate.Parse(resource.EntryDate),
            LeftDate = IsoDate.ParseNullable(resource.LeftDate),
        };
    }
}
