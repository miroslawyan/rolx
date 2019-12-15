// -----------------------------------------------------------------------
// <copyright file="FavouritePhaseController.cs" company="Christian Ewald">
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

namespace RolXServer.Account.WebApi
{
    /// <summary>
    /// Controller for favourite phases.
    /// </summary>
    [ApiController]
    [Route("api/v1/phase/favourite")]
    [Authorize]
    public class FavouritePhaseController : ControllerBase
    {
        private readonly IFavouriteService favouriteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavouritePhaseController"/> class.
        /// </summary>
        /// <param name="favouriteService">The favourite service.</param>
        public FavouritePhaseController(IFavouriteService favouriteService)
        {
            this.favouriteService = favouriteService;
        }

        /// <summary>
        /// Gets the favourite phases.
        /// </summary>
        /// <returns>The favourite phases.</returns>
        [HttpGet]
        public async Task<IEnumerable<Phase>> GetFavourites()
        {
            return (await this.favouriteService.GetAll(this.User.GetUserId()))
                .Select(p => p.ToResource());
        }

        /// <summary>
        /// Adds the specified phase to the favourites.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <returns>No content.</returns>
        [HttpPut]
        public async Task<IActionResult> AddFavourite(Phase phase)
        {
            await this.favouriteService.Add(phase.ToDomain(), this.User.GetUserId());
            return this.NoContent();
        }

        /// <summary>
        /// Removes the specified phase from the favourites.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <returns>No content.</returns>
        [HttpDelete]
        public async Task<IActionResult> RemoveFavourite(Phase phase)
        {
            await this.favouriteService.Remove(phase.ToDomain(), this.User.GetUserId());
            return this.NoContent();
        }
    }
}
