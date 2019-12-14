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
using RolXServer.Account.Domain;
using RolXServer.Account.WebApi.Mapping;
using RolXServer.Account.WebApi.Resource;
using RolXServer.Auth.Domain;
using RolXServer.Common.Util;

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
        private readonly IPhaseService phaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhaseController" /> class.
        /// </summary>
        /// <param name="phaseService">The phase service.</param>
        public PhaseController(IPhaseService phaseService)
        {
            this.phaseService = phaseService;
        }

        /// <summary>
        /// Gets all phases.
        /// </summary>
        /// <returns>All phases.</returns>
        [HttpGet]
        public async Task<IEnumerable<Phase>> GetAll()
        {
            return (await this.phaseService.GetAll()).Select(p => p.ToResource());
        }

        /// <summary>
        /// Gets the suitable phases for the specified date.
        /// </summary>
        /// <param name="date">The date as ISO-formatted string.</param>
        /// <returns>
        /// The suitable phases.
        /// </returns>
        [HttpGet("suitable/{date}")]
        public async Task<ActionResult<IEnumerable<Phase>>> GetSuitable(string date)
        {
            if (!IsoDate.TryParse(date, out var theDate))
            {
                return this.NotFound();
            }

            var result = (await this.phaseService.GetSuitable(this.User.GetUserId(), theDate))
                .Select(p => p.ToResource());

            return new ActionResult<IEnumerable<Phase>>(result);
        }
    }
}
