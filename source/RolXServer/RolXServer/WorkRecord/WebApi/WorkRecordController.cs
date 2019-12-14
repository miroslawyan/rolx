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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolXServer.Auth.Domain;
using RolXServer.Common.Util;
using RolXServer.WorkRecord.Domain;
using RolXServer.WorkRecord.WebApi.Mapping;
using RolXServer.WorkRecord.WebApi.Resource;

namespace RolXServer.WorkRecord.WebApi
{
    /// <summary>
    /// Controller for work records.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public sealed class WorkRecordController : ControllerBase
    {
        private readonly IRecordService recordService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkRecordController" /> class.
        /// </summary>
        /// <param name="recordService">The record service.</param>
        public WorkRecordController(IRecordService recordService)
        {
            this.recordService = recordService;
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

            return (await this.recordService.GetRange(DateRange.ForMonth(monthDate), this.User.GetUserId()))
                .Select(r => r.ToResource())
                .ToList();
        }

        /// <summary>
        /// Gets all records of the specified range (begin..end].
        /// </summary>
        /// <param name="beginDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// The requested records.
        /// </returns>
        [HttpGet("range/{beginDate}..{endDate}")]
        public async Task<ActionResult<IEnumerable<Record>>> GetRange(string beginDate, string endDate)
        {
            if (!IsoDate.TryParse(beginDate, out var begin) || !IsoDate.TryParse(endDate, out var end))
            {
                return this.NotFound();
            }

            return (await this.recordService.GetRange(new DateRange(begin, end), this.User.GetUserId()))
                .Select(r => r.ToResource())
                .ToList();
        }

        /// <summary>
        /// Creates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>The created record.</returns>
        [HttpPost]
        public async Task<ActionResult<Record>> Create(Record record)
        {
            return (await this.recordService.Create(record.ToDomain())).ToResource();
        }

        /// <summary>
        /// Updates the record with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="record">The record.</param>
        /// <returns>
        /// No content.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Record record)
        {
            if (id == 0 || record.Id != id)
            {
                return this.BadRequest();
            }

            await this.recordService.Update(record.ToDomain());

            return this.NoContent();
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
