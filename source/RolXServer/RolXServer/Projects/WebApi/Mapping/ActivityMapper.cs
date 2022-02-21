// -----------------------------------------------------------------------
// <copyright file="ActivityMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;
using RolXServer.Projects.Domain;

namespace RolXServer.Projects.WebApi.Mapping;

/// <summary>
/// Maps activities from / to resource.
/// </summary>
internal static class ActivityMapper
{
    /// <summary>
    /// Converts to resource.
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The resource.</returns>
    public static Resource.Activity ToResource(this DataAccess.Activity domain)
    {
        return new Resource.Activity
        {
            Id = domain.Id,
            Number = domain.Number,
            Name = domain.Name,
            StartDate = domain.StartDate.ToIsoDate(),
            EndDate = domain.EndDate.ToIsoDate(),
            Billability = domain.Billability!,
            Budget = (long)(domain.Budget?.TotalSeconds ?? 0),
            FullNumber = domain.FullNumber(),
            FullName = domain.FullName(),
        };
    }

    /// <summary>
    /// Converts to domain.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <param name="subproject">The subproject.</param>
    /// <returns>
    /// The domain.
    /// </returns>
    public static DataAccess.Activity ToDomain(this Resource.Activity resource, DataAccess.Subproject? subproject = null)
    {
        return new DataAccess.Activity
        {
            Id = resource.Id,
            Number = resource.Number,
            SubprojectId = subproject?.Id ?? 0,
            Subproject = subproject,
            Name = resource.Name,
            StartDate = IsoDate.Parse(resource.StartDate),
            EndDate = IsoDate.ParseNullable(resource.EndDate),
            Budget = TimeSpan.FromSeconds(resource.Budget),
            BillabilityId = resource.Billability.Id,
            Billability = resource.Billability,
        };
    }
}
