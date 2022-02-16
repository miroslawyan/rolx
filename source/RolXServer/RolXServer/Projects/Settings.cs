// -----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Projects;

/// <summary>
/// The settings for the Subprojects package.
/// </summary>
public sealed class Settings
{
    /// <summary>
    /// Gets or sets the pattern for project numbers.
    /// </summary>
    public string ProjectNumberPattern { get; set; } = @"C\d{3}";
}
