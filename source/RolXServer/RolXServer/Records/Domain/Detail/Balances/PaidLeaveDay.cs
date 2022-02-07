// -----------------------------------------------------------------------
// <copyright file="PaidLeaveDay.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records.Domain.Detail.Balances;

/// <summary>
/// A day where the user took a paid leave.
/// </summary>
internal sealed class PaidLeaveDay
{
    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the actual work time in seconds.
    /// </summary>
    public long ActualWorkTimeSeconds { get; set; }

    /// <summary>
    /// Gets or sets the actual work time.
    /// </summary>
    public TimeSpan ActualWorkTime
    {
        get => TimeSpan.FromSeconds(this.ActualWorkTimeSeconds);
        set => this.ActualWorkTimeSeconds = (long)value.TotalSeconds;
    }
}
