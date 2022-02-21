// -----------------------------------------------------------------------
// <copyright file="SubprojectService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain.Detail;

/// <summary>
/// Provides access to <see cref="Subproject"/> instances.
/// </summary>
internal sealed class SubprojectService : ISubprojectService
{
    private readonly RolXContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubprojectService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public SubprojectService(RolXContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <summary>
    /// Gets all subprojects.
    /// </summary>
    /// <returns>
    /// The subprojects.
    /// </returns>
    public async Task<IEnumerable<Subproject>> GetAll()
        => await this.dbContext.Subprojects
            .AsNoTracking()
            .Include(p => p.Activities)
            .ThenInclude(a => a.Billability)
            .ToListAsync();

    /// <summary>
    /// Gets a subproject by the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// The subproject or <c>null</c> if none has been found.
    /// </returns>
    public async Task<Subproject?> GetById(int id)
        => await this.dbContext.Subprojects
            .AsNoTracking()
            .Include(p => p.Activities)
            .ThenInclude(a => a.Billability)
            .FirstOrDefaultAsync(p => p.Id == id);

    /// <summary>
    /// Adds the specified subproject.
    /// </summary>
    /// <param name="subproject">The subproject.</param>
    /// <returns>The async task.</returns>
    public async Task Add(Subproject subproject)
    {
        subproject.Activities.Sanitize();

        this.dbContext.Subprojects.Add(subproject);
        await this.dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Updates the specified subproject.
    /// </summary>
    /// <param name="subproject">The subproject.</param>
    /// <returns>The async task.</returns>
    public async Task Update(Subproject subproject)
    {
        subproject.Activities.Sanitize();

        var activityIds = subproject.Activities
            .Select(a => a.Id);

        var orphanActivities = await this.dbContext.Subprojects
            .Where(s => s.Id == subproject.Id)
            .SelectMany(s => s.Activities)
            .Where(a => !activityIds.Contains(a.Id))
            .ToListAsync();

        this.dbContext.Subprojects.Update(subproject);
        this.dbContext.RemoveRange(orphanActivities);

        await this.dbContext.SaveChangesAsync();
    }
}
