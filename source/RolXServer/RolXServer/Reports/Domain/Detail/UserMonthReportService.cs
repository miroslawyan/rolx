// -----------------------------------------------------------------------
// <copyright file="UserMonthReportService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using RolXServer.Common.Util;
using RolXServer.Projects.Domain;
using RolXServer.Records.Domain;
using RolXServer.Reports.Domain.Model;
using RolXServer.Users.Domain;

namespace RolXServer.Reports.Domain.Detail;

/// <summary>
/// Provides <see cref="UserMonthReport"/> instances.
/// </summary>
internal sealed class UserMonthReportService : IUserMonthReportService
{
    private readonly RolXContext dbContext;
    private readonly IBalanceService balanceService;
    private readonly IRecordService recordService;
    private readonly IPaidLeaveActivities paidLeaveActivities;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserMonthReportService" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="balanceService">The balance service.</param>
    /// <param name="recordService">The record service.</param>
    /// <param name="paidLeaveActivities">The paid leave activities.</param>
    public UserMonthReportService(
        RolXContext dbContext,
        IBalanceService balanceService,
        IRecordService recordService,
        IPaidLeaveActivities paidLeaveActivities)
    {
        this.dbContext = dbContext;
        this.balanceService = balanceService;
        this.recordService = recordService;
        this.paidLeaveActivities = paidLeaveActivities;
    }

    /// <inheritdoc/>
    public async Task<UserMonthReport?> GetFor(Guid userId, DateOnly month)
    {
        var range = DateRange.ForMonth(month);

        var user = await this.dbContext.Users
            .Include(u => u.PartTimeSettings)
            .Include(u => u.BalanceCorrections)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return null;
        }

        var endBalance = await this.balanceService.GetByDate(range.End.AddDays(-1), userId);
        if (endBalance == null)
        {
            return null;
        }

        var startBalance = await this.balanceService.GetByDate(range.Begin.AddDays(-1), userId);
        if (startBalance == null)
        {
            return null;
        }

        var records = (await this.recordService.GetRange(range, userId))
            .ToList();

        var workItemGroups = records
            .SelectMany(record => record.Entries)
            .ToWorkItemGroups();

        if (records.Any(record => record.PaidLeaveType.HasValue))
        {
            workItemGroups = workItemGroups
                .Merge(records.ToPaidLeaveWorkItemGroup(this.paidLeaveActivities));
        }

        return new UserMonthReport(
            month,
            user,
            user.PartTimeSettingsFor(range)
                .ToImmutableList(),
            user.BalanceCorrections
                .Where(correction => correction.Date >= range.Begin && correction.Date < range.End)
                .ToImmutableList(),
            endBalance.Overtime,
            endBalance.Overtime - startBalance.Overtime,
            endBalance.VacationAvailableDays,
            endBalance.VacationAvailableDays - startBalance.VacationAvailableDays,
            workItemGroups.AppendTotalGroup().ToImmutableList());
    }
}
