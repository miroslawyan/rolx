// -----------------------------------------------------------------------
// <copyright file="UserController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolXServer.Users.Domain;
using RolXServer.Users.WebApi.Mapping;
using RolXServer.Users.WebApi.Resource;

namespace RolXServer.Users.WebApi;

/// <summary>
/// Controller for user resources.
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Administrator, Supervisor", Policy = "ActiveUser")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="userService">The user service.</param>
    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>
    /// The Users.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return (await this.userService.GetAll())
            .Select(d => d.ToResource())
            .ToList();
    }

    /// <summary>
    /// Gets the user with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// The requested user.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(Guid id)
    {
        var domain = await this.userService.GetById(id);
        if (domain is null)
        {
            return this.NotFound();
        }

        return domain.ToResource();
    }

    /// <summary>
    /// Updates the user with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="user">The user.</param>
    /// <returns>
    /// No content.
    /// </returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator", Policy = "ActiveUser")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdatableUser user)
    {
        if (id != user.Id)
        {
            return this.BadRequest();
        }

        await this.userService.Update(user.ToDomain());

        return this.NoContent();
    }
}
