// -----------------------------------------------------------------------
// <copyright file="BillabilityController.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.WebApi;

/// <summary>
/// Controller for subproject resources.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Policy = "ActiveUser")]
public class BillabilityController : ControllerBase
{
    private readonly RolXContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BillabilityController"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public BillabilityController(RolXContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Gets all subprojects.
    /// </summary>
    /// <returns>All subprojects.</returns>
    [HttpGet]
    public async Task<IEnumerable<Billability>> GetAll()
        => await this.context.Billabilities
        .AsNoTracking()
        .Where(b => !b.Inactive)
        .OrderBy(b => b.SortingWeight)
        .ToListAsync();
}
