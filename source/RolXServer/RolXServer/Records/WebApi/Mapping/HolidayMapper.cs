// -----------------------------------------------------------------------
// <copyright file="HolidayMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;

namespace RolXServer.Records.WebApi.Mapping;

/// <summary>
/// Maps holidays from / to resource.
/// </summary>
internal static class HolidayMapper
{
    /// <summary>
    /// Converts to resource.
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The resource.</returns>
    public static Resource.Holiday ToResource(this Domain.Model.Holiday domain)
        => new Resource.Holiday
        {
            Name = domain.Name,
            Date = domain.Date.ToIsoDate(),
        };
}
