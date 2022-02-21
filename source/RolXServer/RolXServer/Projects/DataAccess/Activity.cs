// -----------------------------------------------------------------------
// <copyright file="Activity.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace RolXServer.Projects.DataAccess;

/// <summary>
/// An activity in a subproject.
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
    /// Gets or sets the start date.
    /// </summary>
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the time budget in seconds.
    /// </summary>
    public TimeSpan? Budget { get; set; }

    /// <summary>
    /// Gets or sets the subproject identifier.
    /// </summary>
    public int SubprojectId { get; set; }

    /// <summary>
    /// Gets or sets the subproject.
    /// </summary>
    public Subproject? Subproject { get; set; }

    /// <summary>
    /// Gets or sets the billability identifier.
    /// </summary>
    public int BillabilityId { get; set; }

    /// <summary>
    /// Gets or sets the billability.
    /// </summary>
    public Billability? Billability { get; set; }
}
