// -----------------------------------------------------------------------
// <copyright file="ProjectService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RolXServer.Account.DataAccess;

namespace RolXServer.Account.Domain.Detail
{
    /// <summary>
    /// Provides access to <see cref="Project"/> instances.
    /// </summary>
    internal sealed class ProjectService : IProjectService
    {
        private readonly RolXContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ProjectService(RolXContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>
        /// The projects.
        /// </returns>
        public async Task<IEnumerable<Project>> GetAll()
        {
            return await this.dbContext.Projects
                .Include(p => p.Phases)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a project by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The project or <c>null</c> if none has been found.
        /// </returns>
        public async Task<Project?> GetById(int id)
        {
            return await this.dbContext.Projects
                .Include(p => p.Phases)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Adds the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The async task.</returns>
        public async Task Add(Project project)
        {
            project.Phases.Sanitize();

            this.dbContext.Projects.Add(project);
            await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The async task.</returns>
        public async Task Update(Project project)
        {
            project.Phases.Sanitize();

            this.dbContext.Projects.Update(project);

            var phaseIds = project.Phases
                .Select(ph => ph.Id);

            this.dbContext.RemoveRange(
                this.dbContext.Projects
                .Where(p => p.Id == project.Id)
                .SelectMany(p => p.Phases)
                .Where(ph => !phaseIds.Contains(ph.Id)));

            await this.dbContext.SaveChangesAsync();
        }
    }
}
