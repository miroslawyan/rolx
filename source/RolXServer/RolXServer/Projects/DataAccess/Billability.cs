// -----------------------------------------------------------------------
// <copyright file="Billability.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects.DataAccess;

/// <summary>
/// The billability of activities.
/// </summary>
public sealed class Billability
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is billable.
    /// </summary>
    public bool IsBillable { get; set; }

    /// <summary>
    /// Gets or sets the sorting weight.
    /// </summary>
    public int SortingWeight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Billability"/> is inactive.
    /// </summary>
    public bool Inactive { get; set; }
}
