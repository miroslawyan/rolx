// -----------------------------------------------------------------------
// <copyright file="UserMonthReportMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Immutable;

using RolXServer.Common.Util;
using RolXServer.Reports.WebApi.Resource;
using RolXServer.Users.WebApi.Mapping;

namespace RolXServer.Reports.WebApi.Mapping;

/// <summary>
/// Maps <see cref="UserMonthReport"/> instances.
/// </summary>
internal static class UserMonthReportMapper
{
    /// <summary>
    /// Maps the specified domain to resource.
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The mapped resource.</returns>
    public static UserMonthReport ToResource(this Domain.Model.UserMonthReport domain)
        => new(
            IsoDate.ToIsoDate(domain.Month),
            domain.User.ToResource(),
            domain.PartTimeSettings.Select(s => s.ToResource()).ToImmutableList(),
            domain.BalanceCorrections.Select(s => s.ToResource()).ToImmutableList(),
            domain.Overtime,
            domain.OvertimeDelta,
            domain.VacationDays,
            domain.VacationDeltaDays,
            domain.WorkItemGroups);

    private static PartTimeSetting ToResource(this Users.DataAccess.UserPartTimeSetting domain)
        => new(IsoDate.ToIsoDate(domain.StartDate), domain.Factor);

    private static BalanceCorrection ToResource(this Users.DataAccess.UserBalanceCorrection domain)
        => new(IsoDate.ToIsoDate(domain.Date), domain.Overtime, domain.Vacation);
}
