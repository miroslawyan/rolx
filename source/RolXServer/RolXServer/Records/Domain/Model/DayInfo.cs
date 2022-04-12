// -----------------------------------------------------------------------
// <copyright file="DayInfo.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records.Domain.Model;

/// <summary>
/// The information for a specific day.
/// </summary>
public sealed class DayInfo
{
    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the type of the day.
    /// </summary>
    public DayType DayType { get; set; }

    /// <summary>
    /// Gets or sets the name of the day.
    /// </summary>
    public string DayName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the nominal work time.
    /// </summary>
    public TimeSpan NominalWorkTime { get; set; }
}
