// -----------------------------------------------------------------------
// <copyright file="UserMonthReport.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Immutable;

using RolXServer.Users.WebApi.Resource;

namespace RolXServer.Reports.WebApi.Resource;

/// <summary>
/// The monthly report of a single user.
/// </summary>
public sealed record UserMonthReport(
    string Month,
    User User,
    IImmutableList<PartTimeSetting> PartTimeSettings,
    IImmutableList<BalanceCorrection> BalanceCorrections,
    TimeSpan Overtime,
    TimeSpan OvertimeDelta,
    double VacationDays,
    double VacationDeltaDays,
    IImmutableList<Domain.Model.WorkItemGroup> WorkItemGroups);
