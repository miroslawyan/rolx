// -----------------------------------------------------------------------
// <copyright file="UserMonthReport.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Users.DataAccess;

namespace RolXServer.Reports.Domain.Model;

/// <summary>
/// The monthly report of a single user.
/// </summary>
public sealed record UserMonthReport(
    DateOnly Month,
    User User,
    IImmutableList<UserPartTimeSetting> PartTimeSettings,
    IImmutableList<UserBalanceCorrection> BalanceCorrections,
    TimeSpan Overtime,
    TimeSpan OvertimeDelta,
    double VacationDays,
    double VacationDeltaDays,
    IImmutableList<WorkItemGroup> WorkItemGroups);
