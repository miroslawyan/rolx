// -----------------------------------------------------------------------
// <copyright file="PhaseController.cs" company="Christian Ewald">
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
using Microsoft.EntityFrameworkCore;
using RolXServer.Account.WebApi.Mapping;
using RolXServer.Account.WebApi.Resource;

namespace RolXServer.Account.WebApi
{
    /// <summary>
    /// Controller for phase resources.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public sealed class PhaseController : ControllerBase
    {
        private readonly RolXContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhaseController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PhaseController(RolXContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all phases.
        /// </summary>
        /// <returns>All phases.</returns>
        [HttpGet]
        public async Task<IEnumerable<Phase>> GetAll()
        {
            return (await this.context.Phases.ToListAsync()).Select(p => p.ToResource());
        }
    }
}
