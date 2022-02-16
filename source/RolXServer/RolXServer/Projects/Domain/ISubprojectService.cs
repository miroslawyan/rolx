// -----------------------------------------------------------------------
// <copyright file="ISubprojectService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain;

/// <summary>
/// Provides access to <see cref="Subproject"/> instances.
/// </summary>
public interface ISubprojectService
{
    /// <summary>
    /// Gets all subprojects.
    /// </summary>
    /// <returns>The subprojects.</returns>
    Task<IEnumerable<Subproject>> GetAll();

    /// <summary>
    /// Gets a subproject by the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>The subproject or <c>null</c> if none has been found.</returns>
    Task<Subproject?> GetById(int id);

    /// <summary>
    /// Adds the specified subproject.
    /// </summary>
    /// <param name="subproject">The subproject.</param>
    /// <returns>The async task.</returns>
    Task Add(Subproject subproject);

    /// <summary>
    /// Updates the specified subproject.
    /// </summary>
    /// <param name="subproject">The subproject.</param>
    /// <returns>The async task.</returns>
    Task Update(Subproject subproject);
}
