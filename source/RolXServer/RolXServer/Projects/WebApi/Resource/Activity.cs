// -----------------------------------------------------------------------
// <copyright file="Activity.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects.WebApi.Resource;

/// <summary>
/// A activity in a subproject.
/// </summary>
public sealed class Activity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full name.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public string StartDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public string? EndDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is billable.
    /// </summary>
    public bool IsBillable { get; set; }

    /// <summary>
    /// Gets or sets the budget in seconds.
    /// </summary>
    public long Budget { get; set; }
}
