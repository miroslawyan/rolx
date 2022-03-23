// -----------------------------------------------------------------------
// <copyright file="ExportDataToCsvMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using System.Text;

using RolXServer.Common.Util;
using RolXServer.Reports.Domain.Model;

namespace RolXServer.Reports.WebApi.Mapping;

/// <summary>
/// Maps <see cref="ExportData"/> instances to CSV files.
/// </summary>
public static class ExportDataToCsvMapper
{
    /// <summary>
    /// Converts the specified export data into a CSV stream.
    /// </summary>
    /// <param name="exportData">The export data.</param>
    /// <returns>The stream.</returns>
    public static Stream ToCsvStream(this IEnumerable<ExportData> exportData)
        => exportData
            .OrderBy(data => data.Date)
            .ThenBy(data => data.ProjectNumber)
            .ThenBy(data => data.SubprojectNumber)
            .ThenBy(data => data.ActivityNumber)
            .Select(data => new[]
            {
                data.Date.ToString("yyyy-MM-dd"),
                data.ProjectNumber.ToString(),
                data.CustomerName.Sanitize(),
                data.ProjectName.Sanitize(),
                data.SubprojectNumber.ToString(),
                data.SubprojectName.Sanitize(),
                data.ActivityNumber.ToString(),
                data.ActivityName.Sanitize(),
                data.BillabilityName.Sanitize(),
                data.UserName.Sanitize(),
                data.Duration.TotalHours.ToString(CultureInfo.InvariantCulture),
                data.Comment.Sanitize(),
            })
            .Prepend(new[]
            {
                "Datum",
                "Projekt Nr",
                "Kunde",
                "Projekt",
                "Subprojekt Nr",
                "Subprojekt",
                "Aktivität Nr",
                "Aktivität",
                "Verrechenbarkeit",
                "Mitarbeiter",
                "Zeit [h]",
                "Kommentar",
            })
            .Select(data => string.Join(",", data))
            .ToStream(l => Encoding.Unicode.GetBytes(l + Environment.NewLine));

    private static string Sanitize(this string value)
        => value.Contains(',')
        ? $"\"{value.Replace("\"", "\"\"")}\""
        : value;
}
