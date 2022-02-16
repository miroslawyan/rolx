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
    /// Gets or sets the duration in seconds.
    /// </summary>
    public long DurationSeconds { get; set; }

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>
    /// <remarks>
    /// As we cannot sum-up durations in the database through TimeSpan,
    /// We have to map them into seconds manually.
    /// </remarks>
    [NotMapped]
    public TimeSpan Duration
    {
        get
        {
            return TimeSpan.FromSeconds(this.DurationSeconds);
        }

        set
        {
            this.DurationSeconds = (long)value.TotalSeconds;
        }
    }

    /// <summary>
    /// Gets or sets the begin as time since midnight.
    /// </summary>
    [Column(TypeName = "time")]
    public TimeSpan? Begin { get; set; }

    /// <summary>
    /// Gets or sets the pause duration.
    /// </summary>
    public TimeSpan? Pause { get; set; }

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
