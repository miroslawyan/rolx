// -----------------------------------------------------------------------
// <copyright file="YearInfoController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RolXServer.Records.Domain;
using RolXServer.Records.WebApi.Mapping;
using RolXServer.Records.WebApi.Resource;

namespace RolXServer.Records.WebApi;

/// <summary>
/// Controller for year infos, as holidays and monthly work times.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Policy = "ActiveUser")]
public class YearInfoController : ControllerBase
{
    private readonly IYearInfoService yearInfoService;

    /// <summary>
    /// Initializes a new instance of the <see cref="YearInfoController"/> class.
    /// </summary>
    /// <param name="yearInfoService">The record service.</param>
    public YearInfoController(IYearInfoService yearInfoService)
    {
        this.yearInfoService = yearInfoService;
    }

    /// <summary>
    /// Gets the year work schedule for the specified year, consisting of holidays and monthly work times.
    /// </summary>
    /// <param name="year">The year in kinda ISO format, YYYY.</param>
    /// <returns>The work year info.</returns>
    [HttpGet("{year}")]
    public YearInfo GetYear(int year)
        => this.yearInfoService.GetFor(year).ToResource();
}
