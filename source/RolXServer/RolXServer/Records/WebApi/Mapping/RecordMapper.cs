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

namespace RolXServer.Records.WebApi.Mapping
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
                Date = domain.DayInfo.Date.ToIsoDate(),
                UserId = domain.UserId,
                DayType = domain.DayInfo.DayType,
                DayName = domain.DayInfo.DayName,
                NominalWorkTime = (long)domain.DayInfo.NominalWorkTime.TotalSeconds,
                PaidLeaveType = domain.PaidLeaveType,
                PaidLeaveReason = domain.PaidLeaveReason,
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
            var dayInfo = new Domain.Model.DayInfo
            {
                Date = IsoDate.Parse(resource.Date),
                DayType = resource.DayType,
                DayName = resource.DayName,
                NominalWorkTime = TimeSpan.FromSeconds(resource.NominalWorkTime),
            };

            return new Domain.Model.Record(dayInfo)
            {
                UserId = resource.UserId,
                PaidLeaveType = resource.PaidLeaveType,
                PaidLeaveReason = resource.PaidLeaveReason,
                Entries = resource.Entries.Select(e => e.ToDomain()).ToList(),
            };
        }
    }
}
