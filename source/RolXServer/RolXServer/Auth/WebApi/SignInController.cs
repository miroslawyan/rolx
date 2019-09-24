// -----------------------------------------------------------------------
// <copyright file="SignInController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

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
        /// Signs in using the specified google identifier token.
        /// </summary>
        /// <param name="googleIdToken">The google identifier token.</param>
        /// <returns>The JWT bearer token for further authentication.</returns>
        public async Task<ActionResult<AuthenticatedUser>> Post([FromBody] string googleIdToken)
        {
            var user = await this.signInService.Authenticate(googleIdToken);
            if (user is null)
            {
                return this.Unauthorized();
            }

            return user;
        }
    }
}
