// -----------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolXServer.Account.Domain;
using RolXServer.Account.WebApi.Mapping;
using RolXServer.Account.WebApi.Resource;

namespace RolXServer.Account.WebApi
{
    /// <summary>
    /// Controller for project..
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public sealed class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController" /> class.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>All projects.</returns>
        [HttpGet]
        public async Task<IEnumerable<Project>> GetAll()
        {
            return (await this.projectService.GetAll()).Select(p => p.ToResource());
        }

        /// <summary>
        /// Gets the project with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// All project.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetById(int id)
        {
            var domain = await this.projectService.GetById(id);
            if (domain is null)
            {
                return this.NotFound();
            }

            return domain.ToResource();
        }

        /// <summary>
        /// Creates the specified new project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>
        /// The created project.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<Project>> Create(Project project)
        {
            var domain = project.ToDomain();
            await this.projectService.Add(domain);

            return domain.ToResource();
        }

        /// <summary>
        /// Updates the project with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="project">The project.</param>
        /// <returns>
        /// No content.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Project project)
        {
            if (id != project.Id)
            {
                return this.BadRequest();
            }

            await this.projectService.Update(project.ToDomain());

            return this.NoContent();
        }
    }
}
