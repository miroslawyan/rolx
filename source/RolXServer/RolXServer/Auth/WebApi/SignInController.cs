// -----------------------------------------------------------------------
// <copyright file="SignInController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RolXServer.Auth.WebApi
{
    /// <summary>
    /// Controller for signing in.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SignInController : ControllerBase
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public SignInController(ILogger<SignInController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Signs in using the specified google identifier token.
        /// </summary>
        /// <param name="googleIdToken">The google identifier token.</param>
        /// <returns>The JWT bearer token for further authentication.</returns>
        public async Task<ActionResult<string>> Post([FromBody] string googleIdToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleIdToken);
                if (payload is null)
                {
                    return "failed";
                }

                this.logger.LogInformation("Successfully validated request from {0}", payload.Name);

                await Task.CompletedTask;
                return "success";
            }
            catch (InvalidJwtException e)
            {
                this.logger.LogWarning(e, "While validating googleIdToken");
                return this.BadRequest(e.Message);
            }
        }
    }
}
