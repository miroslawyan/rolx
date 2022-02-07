// -----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records;

/// <summary>
/// The settings for the Records package.
/// </summary>
public sealed class Settings
{
    /// <summary>
    /// Gets or sets the nominal work time per day.
    /// </summary>
    public TimeSpan NominalWorkTimePerDay { get; set; } = TimeSpan.FromHours(8);

    /// <summary>
    /// Gets or sets the vacation days per year.
    /// </summary>
    public int VacationDaysPerYear { get; set; } = 25;
}
