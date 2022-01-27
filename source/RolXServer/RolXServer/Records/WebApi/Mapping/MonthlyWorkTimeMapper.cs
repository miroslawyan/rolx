// -----------------------------------------------------------------------
// <copyright file="MonthlyWorkTimeMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;

namespace RolXServer.Records.WebApi.Mapping
{
    /// <summary>
    /// Maps monthly work times from / to resource.
    /// </summary>
    internal static class MonthlyWorkTimeMapper
    {
        /// <summary>
        /// Converts to resource.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The resource.</returns>
        public static Resource.MonthlyWorkTime ToResource(this Domain.Model.MonthlyWorkTime domain)
            => new Resource.MonthlyWorkTime
            {
                Month = domain.Month.ToIsoDate(),
                Days = domain.Days,
                Hours = (int)domain.Hours.TotalSeconds,
            };
    }
}
