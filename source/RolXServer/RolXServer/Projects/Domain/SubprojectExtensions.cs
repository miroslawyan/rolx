// -----------------------------------------------------------------------
// <copyright file="SubprojectExtensions.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain;

/// <summary>
/// Extension methods for <see cref="Subproject"/> instances.
/// </summary>
internal static class SubprojectExtensions
{
    /// <summary>
    /// Gets the full-qualified number of the specified subproject.
    /// </summary>
    /// <param name="subproject">The subproject.</param>
    /// <returns>The full-qualified number.</returns>
    public static string FullNumber(this Subproject subproject)
        => $"#{subproject.ProjectNumber:D4}.{subproject.Number:D3}";

    /// <summary>
    /// Gets the full-qualified name of the specified subproject.
    /// </summary>
    /// <param name="subproject">The subproject.</param>
    /// <returns>The full-qualified name.</returns>
    public static string FullName(this Subproject subproject)
        => $"{subproject.FullNumber()} - {subproject.AllNames()}";

    /// <summary>
    /// Gets all names of the specified subproject.
    /// </summary>
    /// <param name="subproject">The subproject.</param>
    /// <returns>All names.</returns>
    internal static string AllNames(this Subproject subproject)
        => $"{subproject.CustomerName} - {subproject.ProjectName} - {subproject.Name}";
}
