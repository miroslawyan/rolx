// -----------------------------------------------------------------------
// <copyright file="ValuesController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace RolXServer.Controllers
{
    /// <summary>
    /// Demo controller providing some values.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns>The values.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
