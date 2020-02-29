// -----------------------------------------------------------------------
// <copyright file="BalanceController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolXServer.Auth.Domain;
using RolXServer.Common.Util;
using RolXServer.Records.Domain;
using RolXServer.Records.WebApi.Mapping;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi
{
    /// <summary>
    /// Controller for time balances.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "ActiveUser")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService balanceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalanceController"/> class.
        /// </summary>
        /// <param name="balanceService">The balance service.</param>
        public BalanceController(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }

        /// <summary>
        /// Gets the balance by the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The balance.</returns>
        [HttpGet("{date}")]
        public async Task<ActionResult<Balance>> GetByDate(string date)
        {
            if (!IsoDate.TryParse(date, out var byDate))
            {
                return this.NotFound();
            }

            var domain = await this.balanceService.GetByDate(byDate, this.User.GetUserId());
            return domain.ToResource();
        }
    }
}
