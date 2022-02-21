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
    private readonly RolXContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityService"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public ActivityService(RolXContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Gets all activities open in the specified range (begin..end].
    /// </summary>
    /// <param name="unlessEndedBefore">The unless ended before date.</param>
    /// <returns>
    /// The activities.
    /// </returns>
    public async Task<IEnumerable<Activity>> GetAll(DateTime? unlessEndedBefore)
    {
        var query = this.context.Activities
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

    /// <summary>
    /// Gets the suitable activities for the specified user at the specified date.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="date">The date.</param>
    /// <returns>
    /// The suitable activities.
    /// </returns>
    public async Task<IEnumerable<Activity>> GetSuitable(Guid userId, DateTime date)
    {
        var begin = date.AddMonths(-2);
        var end = date.AddMonths(1);

        return await this.context.Records
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
}
