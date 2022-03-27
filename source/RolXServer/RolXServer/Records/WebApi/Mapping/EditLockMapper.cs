// -----------------------------------------------------------------------
// <copyright file="EditLockMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi.Mapping;

/// <summary>
/// Maps edit-locks from / to resource.
/// </summary>
internal static class EditLockMapper
{
    /// <summary>
    /// Converts to resource.
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The resource.</returns>
    public static EditLock ToResource(this DataAccess.EditLock domain)
        => new EditLock(domain.Date.ToIsoDate());

    /// <summary>
    /// Converts to domain.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <returns>The domain.</returns>
    public static DataAccess.EditLock ToDomain(this EditLock resource)
        => new DataAccess.EditLock
        {
            Date = IsoDate.Parse(resource.Date),
        };
}
