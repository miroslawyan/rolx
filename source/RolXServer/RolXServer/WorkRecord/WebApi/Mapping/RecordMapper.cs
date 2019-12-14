// -----------------------------------------------------------------------
// <copyright file="RecordMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;

using RolXServer.Common.Util;

namespace RolXServer.WorkRecord.WebApi.Mapping
{
    /// <summary>
    /// Maps records from / to resource.
    /// </summary>
    internal static class RecordMapper
    {
        /// <summary>
        /// Converts to resource.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The resource.</returns>
        public static Resource.Record ToResource(this Domain.Model.Record domain)
        {
            return new Resource.Record
            {
                Id = domain.Id,
                Date = domain.Date.ToIsoDate(),
                UserId = domain.UserId,
                DayType = domain.DayType,
                DayName = domain.DayName,
                NominalWorkTime = (long)domain.NominalWorkTime.TotalSeconds,
                Entries = domain.Entries.Select(e => e.ToResource()).ToList(),
            };
        }

        /// <summary>
        /// Converts to domain.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>
        /// The domain.
        /// </returns>
        public static Domain.Model.Record ToDomain(this Resource.Record resource)
        {
            return new Domain.Model.Record
            {
                Id = resource.Id,
                Date = IsoDate.Parse(resource.Date),
                UserId = resource.UserId,
                DayType = resource.DayType,
                DayName = resource.DayName,
                NominalWorkTime = TimeSpan.FromSeconds(resource.NominalWorkTime),
                Entries = resource.Entries.Select(e => e.ToDomain()).ToList(),
            };
        }
    }
}
