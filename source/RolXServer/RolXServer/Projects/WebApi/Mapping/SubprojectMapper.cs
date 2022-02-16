// -----------------------------------------------------------------------
// <copyright file="SubprojectMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects.WebApi.Mapping;

/// <summary>
/// Maps subprojects from / to resource.
/// </summary>
internal static class SubprojectMapper
{
    /// <summary>
    /// Converts to resource.
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The resource.</returns>
    public static Resource.Subproject ToResource(this DataAccess.Subproject domain)
    {
        return new Resource.Subproject
        {
            Id = domain.Id,
            Number = domain.Number,
            Name = domain.Name,
            Activities = domain.Activities.Select(ph => ph.ToResource()).ToList(),
        };
    }

    /// <summary>
    /// Converts to domain.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <returns>
    /// The domain.
    /// </returns>
    public static DataAccess.Subproject ToDomain(this Resource.Subproject resource)
    {
        var domain = new DataAccess.Subproject
        {
            Id = resource.Id,
            Number = resource.Number,
            Name = resource.Name,
        };

        domain.Activities = resource.Activities
                .Select(ph => ph.ToDomain(domain))
                .ToList();

        return domain;
    }
}
