// -----------------------------------------------------------------------
// <copyright file="RecordEntry.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

using RolXServer.Projects.DataAccess;

namespace RolXServer.Records.DataAccess;

/// <summary>
/// An entry in a <see cref="Record"/>.
/// </summary>
public sealed class RecordEntry
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the record identifier.
    /// </summary>
    public int RecordId { get; set; }

    /// <summary>
    /// Gets or sets the record.
    /// </summary>
    public Record? Record { get; set; }

    /// <summary>
    /// Gets or sets the duration in seconds.
    /// </summary>
    public long DurationSeconds { get; set; }

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>
    [NotMapped]
    public TimeSpan Duration
    {
        get => TimeSpan.FromSeconds(this.DurationSeconds);
        set => this.DurationSeconds = (long)value.TotalSeconds;
    }

    /// <summary>
    /// Gets or sets the begin time.
    /// </summary>
    public TimeOnly? Begin { get; set; }

    /// <summary>
    /// Gets or sets the pause duration in seconds.
    /// </summary>
    public long? PauseSeconds { get; set; }

    /// <summary>
    /// Gets or sets the pause duration.
    /// </summary>
    [NotMapped]
    public TimeSpan? Pause
    {
        get => this.PauseSeconds.HasValue ? TimeSpan.FromSeconds(this.PauseSeconds.Value) : null;
        set => this.PauseSeconds = (long?)value?.TotalSeconds;
    }

    /// <summary>
    /// Gets or sets the comment.
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the activity identifier.
    /// </summary>
    public int ActivityId { get; set; }

    /// <summary>
    /// Gets or sets the activity.
    /// </summary>
    public Activity? Activity { get; set; }
}
