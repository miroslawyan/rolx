// -----------------------------------------------------------------------
// <copyright file="YearInfo.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records.WebApi.Resource;

/// <summary>
/// Information about the schedule of a year.
/// </summary>
public sealed class YearInfo
{
    /// <summary>
    /// Gets or sets the holidays.
    /// </summary>
    public List<Holiday> Holidays { get; set; } = new List<Holiday>();

    /// <summary>
    /// Gets or sets the monthly work times.
    /// </summary>
    public List<MonthlyWorkTime> MonthlyWorkTimes { get; set; } = new List<MonthlyWorkTime>();
}
