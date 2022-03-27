// -----------------------------------------------------------------------
// <copyright file="EditLockService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

using RolXServer.Records.DataAccess;

namespace RolXServer.Records.Domain.Detail;

/// <summary>
/// Provides access to the <see cref="EditLock"/>.
/// </summary>
internal sealed class EditLockService : IEditLockService
{
    /// <summary>
    /// The one and only edit-lock identifier.
    /// </summary>
    public const int OneAndOnlyId = 1;
    private readonly RolXContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="EditLockService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public EditLockService(RolXContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc/>
    public Task<EditLock> Get()
        => this.dbContext.EditLocks.FirstAsync(x => x.Id == OneAndOnlyId);

    /// <inheritdoc/>
    public async Task Set(EditLock editLock)
    {
        editLock.Id = OneAndOnlyId;
        this.dbContext.EditLocks.Attach(editLock).State = EntityState.Modified;
        await this.dbContext.SaveChangesAsync();
    }
}
