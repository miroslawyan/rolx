// -----------------------------------------------------------------------
// <copyright file="SetupController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RolXServer.Setup.WebApi.Resource;

namespace RolXServer.Setup.WebApi
{
    /// <summary>
    /// Controller for accessing the setup.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public sealed class SetupController : ControllerBase
    {
        private readonly Account.Settings accountSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupController" /> class.
        /// </summary>
        /// <param name="accountSettingsAccessor">The account settings accessor.</param>
        public SetupController(IOptions<Account.Settings> accountSettingsAccessor)
        {
            this.accountSettings = accountSettingsAccessor.Value;
        }

        /// <summary>
        /// Gets the setup.
        /// </summary>
        /// <returns>All customers.</returns>
        [HttpGet]
        public Info Get()
        {
            return new Info
            {
                CustomerNumberPattern = this.accountSettings.CustomerNumberPattern,
            };
        }
    }
}
