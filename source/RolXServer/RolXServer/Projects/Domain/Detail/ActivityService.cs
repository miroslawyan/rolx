// -----------------------------------------------------------------------
// <copyright file="ActivityService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain.Detail;

/// <summary>
/// Provides access to <see cref="Activity"/> instances.
/// </summary>
internal sealed class ActivityService : IActivityService
{
    private readonly RolXContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityService" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public ActivityService(RolXContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Activity>> GetAll(DateTime? unlessEndedBefore)
    {
        var query = this.dbContext.Activities
            .AsNoTracking()
            .Include(a => a.Subproject)
            .Include(a => a.Billability)
            .AsQueryable();

        if (unlessEndedBefore.HasValue)
        {
            query = query.Where(a => !a.EndDate.HasValue || a.EndDate.Value >= unlessEndedBefore.Value);
        }

        return await query.ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Activity>> GetSuitable(Guid userId, DateTime date)
    {
        var begin = date.AddMonths(-2);
        var end = date.AddMonths(1);

        return await this.dbContext.Records
            .Where(r => r.UserId == userId && r.Date >= begin && r.Date < end)
            .Include(r => r.Entries)
                .ThenInclude(e => e.Activity)
                    .ThenInclude(a => a!.Subproject)
            .Include(r => r.Entries)
                .ThenInclude(e => e.Activity)
                    .ThenInclude(a => a!.Billability)
            .SelectMany(r => r.Entries)
            .Select(e => e.Activity!)
            .Distinct()
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<IDictionary<int, TimeSpan>> GetActualSums(int subprojectId)
        => await this.dbContext.RecordEntries
            .Where(entry => entry.Activity!.SubprojectId == subprojectId)
            .GroupBy(entry => entry.ActivityId)
            .Select(group => new
            {
                ActivityId = group.Key,
                Sum = group.Sum(entry => entry.DurationSeconds),
            })
            .ToDictionaryAsync(item => item.ActivityId, item => TimeSpan.FromSeconds(item.Sum));
}
