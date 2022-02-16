// -----------------------------------------------------------------------
// <copyright file="ActivityController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RolXServer.Auth.Domain;
using RolXServer.Common.Util;
using RolXServer.Projects.Domain;
using RolXServer.Projects.WebApi.Mapping;
using RolXServer.Projects.WebApi.Resource;

namespace RolXServer.Projects.WebApi;

/// <summary>
/// Controller for activity resources.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Policy = "ActiveUser")]
public sealed class ActivityController : ControllerBase
{
    private readonly IActivityService activityService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityController" /> class.
    /// </summary>
    /// <param name="activityService">The activity service.</param>
    public ActivityController(IActivityService activityService)
    {
        this.activityService = activityService;
    }

    /// <summary>
    /// Gets all activities.
    /// </summary>
    /// <param name="unlessEndedBeforeDate">The unless ended before date.</param>
    /// <returns>
    /// All activities.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Activity>>> GetAll([FromQuery] string? unlessEndedBeforeDate)
    {
        if (!IsoDate.TryParseNullable(unlessEndedBeforeDate, out var unlessEndedBefore))
        {
            return this.BadRequest("unlessEndedBeforeDate must be an ISO-date");
        }

        return (await this.activityService.GetAll(unlessEndedBefore)).Select(p => p.ToResource()).ToList();
    }

    /// <summary>
    /// Gets the suitable activities for the specified date.
    /// </summary>
    /// <param name="date">The date as ISO-formatted string.</param>
    /// <returns>
    /// The suitable activities.
    /// </returns>
    [HttpGet("suitable/{date}")]
    public async Task<ActionResult<IEnumerable<Activity>>> GetSuitable(string date)
    {
        if (!IsoDate.TryParse(date, out var theDate))
        {
            return this.NotFound();
        }

        return (await this.activityService.GetSuitable(this.User.GetUserId(), theDate))
            .Select(p => p.ToResource())
            .ToList();
    }
}
