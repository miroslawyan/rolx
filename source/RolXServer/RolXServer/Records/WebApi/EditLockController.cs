// -----------------------------------------------------------------------
// <copyright file="EditLockController.cs" company="Christian Ewald">
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
/// Controller for accessing the edit-lock.
/// </summary>
[ApiController]
[Route("api/v1/edit-lock")]
[Authorize(Policy = "ActiveUser")]
public sealed class EditLockController : ControllerBase
{
    private readonly IEditLockService editLockService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EditLockController"/> class.
    /// </summary>
    /// <param name="editLockService">The edit lock service.</param>
    public EditLockController(IEditLockService editLockService)
    {
        this.editLockService = editLockService;
    }

    /// <summary>
    /// Gets the current edit lock.
    /// </summary>
    /// <returns>The edit lock.</returns>
    [HttpGet]
    public async Task<EditLock> Get()
        => (await this.editLockService.Get()).ToResource();

    /// <summary>
    /// Gets the current edit lock.
    /// </summary>
    /// <param name="editLock">The edit lock.</param>
    /// <returns>No content.</returns>
    [HttpPut]
    [Authorize(Roles = "Administrator, Backoffice")]
    public async Task<IActionResult> Set(EditLock editLock)
    {
        await this.editLockService.Set(editLock.ToDomain());
        return this.NoContent();
    }
}
