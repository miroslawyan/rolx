// -----------------------------------------------------------------------
// <copyright file="BalanceService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using RolXServer.Projects;
using RolXServer.Projects.Domain;
using RolXServer.Records.Domain.Detail.Balances;
using RolXServer.Records.Domain.Model;

namespace RolXServer.Records.Domain.Detail;

/// <summary>
/// The balance service.
/// </summary>
public sealed class BalanceService : IBalanceService
{
    private readonly RolXContext dbContext;
    private readonly IPaidLeaveActivities paidLeaveActivities;
    private readonly Settings settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="BalanceService" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="settingsAccessor">The settings accessor.</param>
    /// <param name="paidLeaveActivities">The paid leave activities.</param>
    public BalanceService(
        RolXContext dbContext,
        IPaidLeaveActivities paidLeaveActivities,
        IOptions<Settings> settingsAccessor)
    {
        this.dbContext = dbContext;
        this.paidLeaveActivities = paidLeaveActivities;
        this.settings = settingsAccessor.Value;
    }

    /// <summary>
    /// Gets the balance of the specified user by the specified date.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns>
    /// The balance.
    /// </returns>
    public async Task<Balance> GetByDate(DateOnly date, Guid userId)
    {
        var user = await this.dbContext.Users
            .Include(u => u.PartTimeSettings)
            .Include(u => u.BalanceCorrections)
            .SingleAsync(u => u.Id == userId);

        var vacationActivity = this.paidLeaveActivities[PaidLeaveType.Vacation];
        var data = await this.dbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => new BalanceData
            {
                ActualWorkTimeSeconds = u.Records
                    .Where(r => r.Date <= date)
                    .SelectMany(r => r.Entries)
                    .Sum(e => e.DurationSeconds),

                PaidLeaveDays = u.Records
                    .Where(r => r.Date <= date && r.PaidLeaveType != null)
                    .Select(r => new PaidLeaveDay
                    {
                        Date = r.Date,
                        ActualWorkTimeSeconds = r.Entries.Sum(e => e.DurationSeconds),
                    }).ToList(),

                VacationDays = u.Records
                    .Where(r => r.PaidLeaveType == PaidLeaveType.Vacation)
                    .Select(r => new PaidLeaveDay
                    {
                        Date = r.Date,
                        ActualWorkTimeSeconds = r.Entries.Sum(e => e.DurationSeconds),
                    }).ToList(),

                ManualVacationConsumedSeconds = u.Records
                    .Where(r => r.Date <= date)
                    .SelectMany(r => r.Entries)
                    .Where(e => e.ActivityId == vacationActivity.Id)
                    .Sum(e => e.DurationSeconds),

                ManualVacationPlannedSeconds = u.Records
                    .Where(r => r.Date > date)
                    .SelectMany(r => r.Entries)
                    .Where(e => e.ActivityId == vacationActivity.Id)
                    .Sum(e => e.DurationSeconds),
            })
            .SingleAsync();

        data.User = user;
        data.ByDate = date;
        data.NominalWorkTimePerDay = this.settings.NominalWorkTimePerDay;
        data.VacationDaysPerYear = this.settings.VacationDaysPerYear;

        return data.ToBalance();
    }
}
