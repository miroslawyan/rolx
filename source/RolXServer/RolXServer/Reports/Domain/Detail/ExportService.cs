// -----------------------------------------------------------------------
// <copyright file="ExportService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using RolXServer.Common.Util;
using RolXServer.Common.WebApi;
using RolXServer.Projects.Domain;
using RolXServer.Records;
using RolXServer.Reports.Domain.Model;
using RolXServer.Users.Domain;

namespace RolXServer.Reports.Domain.Detail;

/// <summary>
/// Provides data for exporting.
/// </summary>
internal sealed class ExportService : IExportService
{
    private readonly RolXContext dbContext;
    private readonly IPaidLeaveActivities paidLeaveActivities;
    private readonly Settings settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportService" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="paidLeaveActivities">The paid leave activities.</param>
    /// <param name="settingsAccessor">The settings accessor.</param>
    public ExportService(
        RolXContext dbContext,
        IPaidLeaveActivities paidLeaveActivities,
        IOptions<Settings> settingsAccessor)
    {
        this.dbContext = dbContext;
        this.paidLeaveActivities = paidLeaveActivities;
        this.settings = settingsAccessor.Value;
    }

    /// <inheritdoc/>
    public async Task<Export> GetFor(DateRange range, Guid creatorId, int? subprojectId)
    {
        var creator = await this.dbContext.Users.FirstOrThrowNotFoundAsync(u => u.Id == creatorId);

        var subproject = subprojectId.HasValue
            ? (await this.dbContext.Subprojects.FirstOrThrowNotFoundAsync(s => s.Id == subprojectId)).FullName()
            : "Alle";

        return new Export(
            subproject,
            range,
            creator.FullName(),
            DateTime.Now,
            await this.GetEntries(range, subprojectId));
    }

    private async Task<IEnumerable<ExportEntry>> GetEntries(DateRange range, int? subprojectId)
    {
        var entries = this.dbContext.RecordEntries
            .AsNoTracking()
            .Where(entry => entry.Record!.Date >= range.Begin && entry.Record!.Date < range.End);

        if (subprojectId.HasValue)
        {
            entries = entries.Where(entry => entry.Activity!.SubprojectId == subprojectId.Value);
        }

        var exportEntries = (await entries
            .Select(entry => new ExportEntry(
                entry.Record!.Date,
                entry.Activity!.Subproject!.ProjectNumber,
                entry.Activity!.Subproject!.CustomerName,
                entry.Activity!.Subproject!.ProjectName,
                entry.Activity!.Subproject!.Number,
                entry.Activity!.Subproject!.Name,
                entry.Activity!.Number,
                entry.Activity!.Name,
                entry.Activity.Billability!.Name,
                entry.Record.User!.FirstName + " " + entry.Record.User.LastName,
                entry.Duration,
                entry.Comment))
            .ToListAsync())
            .AsEnumerable();

        if (!subprojectId.HasValue)
        {
            exportEntries = exportEntries
                .Concat(await this.GetPaidLeaveEntries(range));
        }

        return exportEntries;
    }

    private async Task<IEnumerable<ExportEntry>> GetPaidLeaveEntries(DateRange range)
    {
        var items = await this.dbContext.Records
            .AsNoTracking()
            .Where(record => record.Date >= range.Begin && record.Date < range.End && record.PaidLeaveType.HasValue)
            .Where(record => record.PaidLeaveType.HasValue)
            .Select(record => new
            {
                Date = record.Date,
                UserId = record.UserId,
                PaidLeaveType = record.PaidLeaveType!.Value,
                WorkDuration = TimeSpan.FromSeconds(record.Entries.Sum(entry => entry.DurationSeconds)),
                Reason = record.PaidLeaveReason,
            })
            .ToListAsync();

        var userIds = items.Select(item => item.UserId).Distinct().ToHashSet();
        var users = await this.dbContext.Users
            .AsNoTracking()
            .Include(user => user.PartTimeSettings)
            .Where(user => userIds.Contains(user.Id))
            .ToDictionaryAsync(user => user.Id, user => user);

        return items
            .Select(item => new
            {
                Date = item.Date,
                User = users[item.UserId],
                Activity = this.paidLeaveActivities[item.PaidLeaveType],
                WorkDuration = item.WorkDuration,
                Reason = item.Reason,
            })
            .Select(item => new ExportEntry(
                item.Date,
                item.Activity.Subproject!.ProjectNumber,
                item.Activity.Subproject!.CustomerName,
                item.Activity.Subproject!.ProjectName,
                item.Activity.Subproject!.Number,
                item.Activity.Subproject!.Name,
                item.Activity.Number,
                item.Activity.Name,
                "Abwesenheit",
                item.User.FirstName + " " + item.User.LastName,
                (this.settings.NominalWorkTimePerDay * item.User.PartTimeFactorAt(item.Date)) - item.WorkDuration,
                item.Reason ?? string.Empty));
    }
}
