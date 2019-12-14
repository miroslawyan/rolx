// -----------------------------------------------------------------------
// <copyright file="RecordEntryMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.WorkRecord.WebApi.Mapping
{
    /// <summary>
    /// Maps record entries from / to resource.
    /// </summary>
    internal static class RecordEntryMapper
    {
        /// <summary>
        /// Converts to resource.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The resource.</returns>
        public static Resource.RecordEntry ToResource(this DataAccess.RecordEntry domain)
        {
            return new Resource.RecordEntry
            {
                Id = domain.Id,
                PhaseId = domain.PhaseId,
                Duration = (long)domain.Duration.TotalSeconds,
            };
        }

        /// <summary>
        /// Converts to domain.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>
        /// The domain.
        /// </returns>
        public static DataAccess.RecordEntry ToDomain(this Resource.RecordEntry resource)
        {
            return new DataAccess.RecordEntry
            {
                Id = resource.Id,
                Duration = TimeSpan.FromSeconds(resource.Duration),
                PhaseId = resource.PhaseId,
            };
        }
    }
}
