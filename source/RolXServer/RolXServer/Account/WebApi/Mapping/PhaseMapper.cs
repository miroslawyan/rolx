// -----------------------------------------------------------------------
// <copyright file="PhaseMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;

namespace RolXServer.Account.WebApi.Mapping
{
    /// <summary>
    /// Maps phases from / to resource.
    /// </summary>
    internal static class PhaseMapper
    {
        /// <summary>
        /// Converts to resource.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The resource.</returns>
        public static Resource.Phase ToResource(this DataAccess.Phase domain)
        {
            return new Resource.Phase
            {
                Id = domain.Id,
                Number = domain.Number,
                Name = domain.Name,
                FullName = domain.FullName,
                StartDate = domain.StartDate.ToIsoDate(),
                EndDate = domain.EndDate.ToIsoDate(),
                IsBillable = domain.IsBillable,
                BudgetHours = domain.BudgetHours,
            };
        }

        /// <summary>
        /// Converts to domain.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="project">The project.</param>
        /// <returns>
        /// The domain.
        /// </returns>
        public static DataAccess.Phase ToDomain(this Resource.Phase resource, DataAccess.Project? project = null)
        {
            return new DataAccess.Phase
            {
                Id = resource.Id,
                Number = resource.Number,
                ProjectId = project?.Id ?? 0,
                Project = project,
                Name = resource.Name,
                FullName = resource.FullName,
                StartDate = IsoDate.Parse(resource.StartDate),
                EndDate = IsoDate.ParseNullable(resource.EndDate),
                IsBillable = resource.IsBillable,
                BudgetHours = resource.BudgetHours,
            };
        }
    }
}
