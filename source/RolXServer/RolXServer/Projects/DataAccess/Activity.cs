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
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Gets or sets the ended date.
    /// </summary>
    /// <remarks>
    /// This marks the first day this activity is closed for adding records.
    /// </remarks>
    public DateOnly? EndedDate { get; set; }

    /// <summary>
    /// Gets or sets the time budget in seconds.
    /// </summary>
    public long? BudgetSeconds { get; set; }

    /// <summary>
    /// Gets or sets the time budget.
    /// </summary>
    [NotMapped]
    public TimeSpan? Budget
    {
        get => this.BudgetSeconds.HasValue ? TimeSpan.FromSeconds(this.BudgetSeconds.Value) : null;
        set => this.BudgetSeconds = (long?)value?.TotalSeconds;
    }

    /// <summary>
    /// Gets or sets the planned time in seconds.
    /// </summary>
    public long? PlannedSeconds { get; set; }

    /// <summary>
    /// Gets or sets the planned time.
    /// </summary>
    [NotMapped]
    public TimeSpan? Planned
    {
        get => this.PlannedSeconds.HasValue ? TimeSpan.FromSeconds(this.PlannedSeconds.Value) : null;
        set => this.PlannedSeconds = (long?)value?.TotalSeconds;
    }

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
