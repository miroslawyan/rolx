// -----------------------------------------------------------------------
// <copyright file="SignInController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolXServer.Auth.Domain;
using RolXServer.Auth.Domain.Model;

namespace RolXServer.Auth.WebApi
{
    /// <summary>
    /// Controller for signing in.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SignInController : ControllerBase
    {
        private readonly ISignInService signInService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInController" /> class.
        /// </summary>
        /// <param name="signInService">The sign-in service.</param>
        public SignInController(ISignInService signInService)
        {
            this.signInService = signInService;
        }

        /// <summary>
        /// Provides information on the sign-in capabilities of this server.
        /// </summary>
        /// <returns>The sign-in information.</returns>
        [HttpGet("info")]
        public async Task<ActionResult<Info>> GetInfo()
        {
            return await this.signInService.GetInfo();
        }

        /// <summary>
        /// Signs in using the specified google identifier token.
        /// </summary>
        /// <param name="googleIdToken">The google identifier token.</param>
        /// <returns>The authenticated user.</returns>
        [HttpPost]
        public async Task<ActionResult<AuthenticatedUser>> SignIn([FromBody] string googleIdToken)
        {
            var user = await this.signInService.Authenticate(googleIdToken);
            if (user is null)
            {
                return this.Unauthorized();
            }

            return user;
        }

        /// <summary>
        /// Extends the authentication of the calling user.
        /// </summary>
        /// <returns>The extended, authenticated user.</returns>
        [HttpGet("extend")]
        [Authorize]
        public async Task<ActionResult<AuthenticatedUser>> Extend()
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await this.signInService.Extend(Guid.Parse(userId));
            if (user is null)
            {
                return this.Unauthorized();
            }

            return user;
        }
    }
}
