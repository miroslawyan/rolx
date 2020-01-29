// -----------------------------------------------------------------------
// <copyright file="UserController.cs" company="Christian Ewald">
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
using RolXServer.Auth.DataAccess;
using RolXServer.UserManagement.Domain;

namespace RolXServer.UserManagement.WebApi
{
    /// <summary>
    /// Controller for user resources.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator, Supervisor")]
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
            return (await this.userService.GetAll()).ToList();
        }
    }
}
