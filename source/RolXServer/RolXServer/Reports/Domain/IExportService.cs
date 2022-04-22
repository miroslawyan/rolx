// -----------------------------------------------------------------------
// <copyright file="IExportService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Common.Util;
using RolXServer.Reports.Domain.Model;

namespace RolXServer.Reports.Domain;

/// <summary>
/// Provides data for exporting.
/// </summary>
public interface IExportService
{
    /// <summary>
    /// Gets the export for the specified range and subproject.
    /// </summary>
    /// <param name="range">The range.</param>
    /// <param name="creatorId">The creator.</param>
    /// <param name="subprojectId">The subproject identifier.</param>
    /// <returns>
    /// The data.
    /// </returns>
    /// <remarks>
    /// When no subproject identifier is provided, the data of all subprojects is returned.
    /// </remarks>
    Task<Export> GetFor(DateRange range, Guid creatorId, int? subprojectId);
}
