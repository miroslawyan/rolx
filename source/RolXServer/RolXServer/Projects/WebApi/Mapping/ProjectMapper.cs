// -----------------------------------------------------------------------
// <copyright file="ProjectMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace RolXServer.Projects.WebApi.Mapping
{
    /// <summary>
    /// Maps projects from / to resource.
    /// </summary>
    internal static class ProjectMapper
    {
        /// <summary>
        /// Converts to resource.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The resource.</returns>
        public static Resource.Project ToResource(this DataAccess.Project domain)
        {
            return new Resource.Project
            {
                Id = domain.Id,
                Number = domain.Number,
                Name = domain.Name,
                Phases = domain.Phases.Select(ph => ph.ToResource()).ToList(),
            };
        }

        /// <summary>
        /// Converts to domain.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>
        /// The domain.
        /// </returns>
        public static DataAccess.Project ToDomain(this Resource.Project resource)
        {
            var domain = new DataAccess.Project
            {
                Id = resource.Id,
                Number = resource.Number,
                Name = resource.Name,
            };

            domain.Phases = resource.Phases
                    .Select(ph => ph.ToDomain(domain))
                    .ToList();

            return domain;
        }
    }
}
