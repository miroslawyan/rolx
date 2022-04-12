// -----------------------------------------------------------------------
// <copyright file="Holiday.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Records.Domain.Model;

/// <summary>
/// An entry in a <see cref="YearInfo"/>.
/// </summary>
public class Holiday
{
    /// <summary>
    /// Gets or sets name of a holiday.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets date of a holiday.
    /// </summary>
    public DateOnly Date { get; set; }
}
