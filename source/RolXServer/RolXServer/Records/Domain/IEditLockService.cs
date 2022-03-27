// -----------------------------------------------------------------------
// <copyright file="IEditLockService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Records.DataAccess;

namespace RolXServer.Records.Domain;

/// <summary>
/// Provides access to the <see cref="EditLock"/>.
/// </summary>
public interface IEditLockService
{
    /// <summary>
    /// Gets the current edit-lock.
    /// </summary>
    /// <returns>The edit-lock.</returns>
    Task<EditLock> Get();

    /// <summary>
    /// Sets the current edit-lock.
    /// </summary>
    /// <param name="editLock">The edit-lock.</param>
    /// <returns>The async task.</returns>
    Task Set(EditLock editLock);
}
