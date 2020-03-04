// -----------------------------------------------------------------------
// <copyright file="BalanceMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;

namespace RolXServer.Records.WebApi.Mapping
{
    /// <summary>
    /// Maps records from / to resource.
    /// </summary>
    public static class BalanceMapper
    {
        /// <summary>
        /// Converts to resource.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The resource.</returns>
        public static Resource.Balance ToResource(this Domain.Model.Balance domain)
        {
            return new Resource.Balance
            {
                ByDate = domain.ByDate.ToIsoDate(),
                Overtime = (long)domain.Overtime.TotalSeconds,
                VacationAvailableDays = domain.VacationAvailableDays,
                VacationPlannedDays = domain.VacationPlannedDays,
            };
        }
    }
}
