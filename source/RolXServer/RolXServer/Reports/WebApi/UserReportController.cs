// -----------------------------------------------------------------------
// <copyright file="UserReportController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RolXServer.Auth.Domain;
using RolXServer.Common.Util;
using RolXServer.Reports.Domain;
using RolXServer.Reports.WebApi.Mapping;
using RolXServer.Reports.WebApi.Resource;

namespace RolXServer.Reports.WebApi;

/// <summary>
/// Controller for user reports.
/// </summary>
[ApiController]
[Route("api/v1/user-report")]
[Authorize(Policy = "ActiveUser")]
public class UserReportController : ControllerBase
{
    private readonly IUserMonthReportService userMonthReportService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserReportController" /> class.
    /// </summary>
    /// <param name="userMonthReportService">The user month report service.</param>
    public UserReportController(IUserMonthReportService userMonthReportService)
    {
        this.userMonthReportService = userMonthReportService;
    }

    /// <summary>
    /// Gets the monthly user report for the specified user and month.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="month">The month in kinda ISO format, YYYY-MM.</param>
    /// <returns>
    /// The report.
    /// </returns>
    [HttpGet("{userId}/month/{month}")]
    public async Task<ActionResult<UserMonthReport>> GetMonthReport(Guid userId, string month)
    {
        if (!IsoDate.TryParseMonth(month, out var monthDate))
        {
            return this.NotFound();
        }

        if (userId != this.User.GetUserId() && this.User.GetRole() < Users.Role.Supervisor)
        {
            return this.Forbid();
        }

        var domain = await this.userMonthReportService.GetFor(userId, monthDate);
        if (domain == null)
        {
            return this.NotFound();
        }

        return domain.ToResource();
    }
}
