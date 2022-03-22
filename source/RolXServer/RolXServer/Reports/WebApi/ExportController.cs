// -----------------------------------------------------------------------
// <copyright file="ExportController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RolXServer.Common.Util;
using RolXServer.Reports.Domain;
using RolXServer.Reports.WebApi.Mapping;

namespace RolXServer.Reports.WebApi;

/// <summary>
/// Controller for exporting data.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "Administrator, Supervisor", Policy = "ActiveUser")]
public class ExportController : ControllerBase
{
    private readonly IExportService exportService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportController" /> class.
    /// </summary>
    /// <param name="exportService">The export service.</param>
    public ExportController(IExportService exportService)
    {
        this.exportService = exportService;
    }

    /// <summary>
    /// Gets a report containing all data for the specified month.
    /// </summary>
    /// <param name="month">The optional month.</param>
    /// <param name="begin">The optional begin date.</param>
    /// <param name="end">The optional end date.</param>
    /// <returns>
    /// The report.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> All(string? month, string? begin, string? end)
    {
        var range = TryEvaluateRange(month, begin, end);
        if (!range.HasValue)
        {
            return this.BadRequest("A month or begin and end dates must be provided");
        }

        var data = await this.exportService.GetFor(range.Value);
        return this.File(
            data.ToCsvStream(),
            "text/csv;charset=utf-16",
            fileDownloadName: GetFileName(month, begin, end));
    }

    private static DateRange? TryEvaluateRange(string? month, string? begin, string? end)
    {
        if (month != null && IsoDate.TryParseMonth(month, out var monthDate))
        {
            return DateRange.ForMonth(monthDate);
        }

        if (begin != null && end != null
            && IsoDate.TryParse(begin, out var beginDate)
            && IsoDate.TryParse(end, out var endDate)
            && beginDate <= endDate)
        {
            return new DateRange(beginDate, endDate);
        }

        return null;
    }

    private static string GetFileName(string? month, string? begin, string? end)
    {
        if (month != null)
        {
            return $"rolx-{month}-all.csv";
        }

        if (begin != null && end != null)
        {
            return $"rolx-{begin}-{end}-all.csv";
        }

        return "rolx-all.csv";
    }
}
