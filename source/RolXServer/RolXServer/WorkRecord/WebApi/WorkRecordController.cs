// -----------------------------------------------------------------------
// <copyright file="WorkRecordController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RolXServer.WorkRecord.Domain;
using RolXServer.WorkRecord.WebApi.Resource;

namespace RolXServer.WorkRecord.WebApi
{
    /// <summary>
    /// Controller for work records.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class WorkRecordController : ControllerBase
    {
        private readonly IRecordService recordService;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkRecordController" /> class.
        /// </summary>
        /// <param name="recordService">The record service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public WorkRecordController(
            IRecordService recordService,
            IMapper mapper,
            ILogger<WorkRecordController> logger)
        {
            this.recordService = recordService;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the records for the specified month.
        /// </summary>
        /// <param name="month">The month in kinda ISO format, YYYY-MM.</param>
        /// <returns>The work records.</returns>
        [HttpGet("month/{month}")]
        public async Task<ActionResult<IEnumerable<Record>>> GetMonth(string month)
        {
            if (!TryParseMonth(month, out var monthDate))
            {
                return this.NotFound();
            }

            return (await this.recordService.GetAllOfMonth(monthDate))
                .Select(r => this.mapper.Map<Record>(r))
                .ToList();
        }

        private static bool TryParseMonth(string candidate, out DateTime result)
        {
            return DateTime.TryParseExact(
                candidate,
                "yyyy-MM",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal,
                out result);
        }
    }
}
