// -----------------------------------------------------------------------
// <copyright file="IProjectService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain
{
    /// <summary>
    /// Provides access to <see cref="Project"/> instances.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>The projects.</returns>
        Task<IEnumerable<Project>> GetAll();

        /// <summary>
        /// Gets a project by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The project or <c>null</c> if none has been found.</returns>
        Task<Project?> GetById(int id);

        /// <summary>
        /// Adds the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The async task.</returns>
        Task Add(Project project);

        /// <summary>
        /// Updates the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The async task.</returns>
        Task Update(Project project);
    }
}
