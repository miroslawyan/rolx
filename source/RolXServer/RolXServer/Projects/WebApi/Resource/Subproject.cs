// -----------------------------------------------------------------------
// <copyright file="Subproject.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects.WebApi.Resource;

/// <summary>
/// A subproject we are working on.
/// </summary>
public class Subproject
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
    /// Gets or sets the project number.
    /// </summary>
    public int ProjectNumber { get; set; }

    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the activities.
    /// </summary>
    public List<Activity> Activities { get; set; } = new List<Activity>();

    /// <summary>
    /// Gets or sets the full-qualified number.
    /// </summary>
    public string FullNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full-qualified name.
    /// </summary>
    public string FullName { get; set; } = string.Empty;
}
