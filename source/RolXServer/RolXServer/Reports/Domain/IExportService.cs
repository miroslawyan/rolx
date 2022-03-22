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
    /// Gets the export data for the specified range.
    /// </summary>
    /// <param name="range">The range.</param>
    /// <returns>The data.</returns>
    Task<IEnumerable<ExportData>> GetFor(DateRange range);
}
