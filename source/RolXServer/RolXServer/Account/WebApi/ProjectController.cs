// -----------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolXServer.Account.WebApi.Resource;
using RolXServer.Common.DataAccess;

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
        private readonly IRepository<DataAccess.Project> projectRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController" /> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="mapper">The mapper.</param>
        public ProjectController(IRepository<DataAccess.Project> projectRepository, IMapper mapper)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>All projects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll()
        {
            return await this.mapper.ProjectTo<Project>(this.projectRepository.Entities)
                .ToListAsync();
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
            var project = await this.mapper.ProjectTo<Project>(this.projectRepository.Entities)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project is null)
            {
                return this.NotFound();
            }

            return project;
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
            var entity = this.mapper.Map<DataAccess.Project>(project);
            entity.Customer = null; // TODO: check if we may prevent this during mapping

            this.projectRepository.Entities.Add(entity);
            await this.projectRepository.SaveChanges();

            return this.mapper.Map<Project>(entity);
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

            var entity = this.mapper.Map<DataAccess.Project>(project);
            entity.Customer = null; // TODO: check if we may prevent this during mapping

            this.projectRepository.Entities.Attach(entity).State = EntityState.Modified;
            await this.projectRepository.SaveChanges();

            return this.NoContent();
        }
    }
}
