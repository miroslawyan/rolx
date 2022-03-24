// -----------------------------------------------------------------------
// <copyright file="WorkRecordController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RolXServer.Auth.Domain;
using RolXServer.Common.Util;
using RolXServer.Records.Domain;
using RolXServer.Records.WebApi.Mapping;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi;

/// <summary>
/// Controller for work records.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Policy = "ActiveUser")]
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
    /// Gets all records of the specified range (begin..end].
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="beginDate">The begin date.</param>
    /// <param name="endDate">The end date.</param>
    /// <returns>
    /// The requested records.
    /// </returns>
    [HttpGet("{userId}/range/{beginDate}..{endDate}")]
    public async Task<ActionResult<IEnumerable<Record>>> GetRange(Guid userId, string beginDate, string endDate)
    {
        if (!IsoDate.TryParse(beginDate, out var begin) || !IsoDate.TryParse(endDate, out var end))
        {
            return this.NotFound();
        }

        if (userId != this.User.GetUserId() && this.User.GetRole() < Users.Role.Supervisor)
        {
            return this.Forbid();
        }

        return (await this.recordService.GetRange(new DateRange(begin, end), userId))
            .Select(r => r.ToResource())
            .ToList();
    }

    /// <summary>
    /// Updates the record with the specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="date">The date.</param>
    /// <param name="record">The record.</param>
    /// <returns>
    /// No content.
    /// </returns>
    [HttpPut("{userId}/{date}")]
    public async Task<IActionResult> Update(Guid userId, string date, Record record)
    {
        if (!IsoDate.TryParse(date, out var theDate))
        {
            return this.NotFound();
        }

        if (userId != this.User.GetUserId() && this.User.GetRole() < Users.Role.Supervisor)
        {
            return this.Forbid();
        }

        if (userId == this.User.GetUserId() && !this.User.IsActiveAt(theDate))
        {
            return this.Forbid();
        }

        if (record.Date != date)
        {
            return this.BadRequest("non-matching date");
        }

        if (record.UserId != userId)
        {
            return this.BadRequest("non-matching user id");
        }

        await this.recordService.Update(record.ToDomain());

        return this.NoContent();
    }
}
