// -----------------------------------------------------------------------
// <copyright file="FavouriteActivityController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RolXServer.Auth.Domain;
using RolXServer.Projects.Domain;
using RolXServer.Projects.WebApi.Mapping;
using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi;

/// <summary>
/// Controller for favourite activities.
/// </summary>
[ApiController]
[Route("api/v1/activity/favourite")]
[Authorize(Policy = "ActiveUser")]
public class FavouriteActivityController : ControllerBase
{
    private readonly IFavouriteService favouriteService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FavouriteActivityController"/> class.
    /// </summary>
    /// <param name="favouriteService">The favourite service.</param>
    public FavouriteActivityController(IFavouriteService favouriteService)
    {
        this.favouriteService = favouriteService;
    }

    /// <summary>
    /// Gets the favourite activities.
    /// </summary>
    /// <returns>The favourite activities.</returns>
    [HttpGet]
    public async Task<IEnumerable<Activity>> GetFavourites()
        => (await this.favouriteService.GetAll(this.User.GetUserId()))
            .Select(p => p.ToResource())
            .ToList();

    /// <summary>
    /// Adds the specified activity to the favourites.
    /// </summary>
    /// <param name="activity">The activity.</param>
    /// <returns>No content.</returns>
    [HttpPut]
    public async Task<IActionResult> AddFavourite(Activity activity)
    {
        await this.favouriteService.Add(activity.ToDomain(), this.User.GetUserId());
        return this.NoContent();
    }

    /// <summary>
    /// Removes the specified activity from the favourites.
    /// </summary>
    /// <param name="activity">The activity.</param>
    /// <returns>No content.</returns>
    [HttpDelete]
    public async Task<IActionResult> RemoveFavourite(Activity activity)
    {
        await this.favouriteService.Remove(activity.ToDomain(), this.User.GetUserId());
        return this.NoContent();
    }
}
