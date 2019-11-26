// -----------------------------------------------------------------------
// <copyright file="RecordMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

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
                Date = domain.Date.ToIsoDate(),
                DayType = domain.DayType,
                DayName = domain.DayName,
                NominalWorkTimeHours = domain.NominalWorkTime.TotalHours,
            };
        }
    }
}
