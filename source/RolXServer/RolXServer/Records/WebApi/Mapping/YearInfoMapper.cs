// -----------------------------------------------------------------------
// <copyright file="YearInfoMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace RolXServer.Records.WebApi.Mapping
{
    /// <summary>
    /// Maps YearInfo from / to resource.
    /// </summary>
    internal static class YearInfoMapper
    {
        /// <summary>
        /// Converts to resource.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The resource.</returns>
        public static Resource.YearInfo ToResource(this Domain.Model.YearInfo domain)
            => new Resource.YearInfo
            {
                Holidays = domain.Holidays.Select(e => e.ToResource()).ToList(),
                MonthlyWorkTimes = domain.MonthlyWorkTimes.Select(e => e.ToResource()).ToList(),
            };
    }
}
