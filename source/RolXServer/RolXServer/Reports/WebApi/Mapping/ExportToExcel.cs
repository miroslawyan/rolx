// -----------------------------------------------------------------------
// <copyright file="ExportToExcel.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using ClosedXML.Excel;

using Microsoft.AspNetCore.Mvc;

using RolXServer.Reports.Domain.Model;

namespace RolXServer.Reports.WebApi.Mapping;

/// <summary>
/// Maps <see cref="Export"/> instances to Excel exports.
/// </summary>
public static class ExportToExcel
{
    /// <summary>
    /// Returns the specified data as Excel report with the specified file name.
    /// </summary>
    /// <param name="controller">The controller.</param>
    /// <param name="export">The export.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>The file stream result containing the Excel report.</returns>
    public static FileStreamResult ExcelExport(this ControllerBase controller, Export export, string fileName)
    {
        var stream = new MemoryStream();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Data");

            var row = worksheet.AddHeader(export);
            foreach (var entry in export.Entries)
            {
                worksheet.Add(entry, row++);
            }

            workbook.SaveAs(stream);
        }

        stream.Position = 0;
        return controller.File(
            stream,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: fileName);
    }

    private static int AddHeader(this IXLWorksheet worksheet, Export export)
    {
        var row = 1;
        worksheet.AddHeader("Subprojekt", export.Subproject, row++);
        worksheet.AddHeader("Zeitraum von", export.Range.Begin, row++);
        worksheet.AddHeader("Zeitraum bis", export.Range.End.AddDays(-1), row++);
        worksheet.AddHeader("Erzeugt von", export.Creator, row++);
        worksheet.AddHeader("Erzeugt am", export.CreationDate, row++);

        ++row;
        var column = 1;
        worksheet.Cell(row, column++).Value = "Datum";
        worksheet.Cell(row, column++).Value = "Projekt Nr";
        worksheet.Cell(row, column++).Value = "Kunde";
        worksheet.Cell(row, column++).Value = "Projekt";
        worksheet.Cell(row, column++).Value = "Subprojekt Nr";
        worksheet.Cell(row, column++).Value = "Subprojekt";
        worksheet.Cell(row, column++).Value = "Aktivität Nr";
        worksheet.Cell(row, column++).Value = "Aktivität";
        worksheet.Cell(row, column++).Value = "Verrechenbarkeit";
        worksheet.Cell(row, column++).Value = "Mitarbeiter";
        worksheet.Cell(row, column++).Value = "Zeit [h]";
        worksheet.Cell(row, column++).Value = "Kommentar";

        return ++row;
    }

    private static void AddHeader(this IXLWorksheet worksheet, string name, object value, int row)
    {
        var column = 1;
        worksheet.Cell(row, column++).Value = name;
        worksheet.Cell(row, column++).Value = value;
    }

    private static void Add(this IXLWorksheet worksheet, ExportEntry entry, int row)
    {
        var column = 1;
        worksheet.Cell(row, column++).Value = entry.Date;
        worksheet.Cell(row, column++).Value = entry.ProjectNumber;
        worksheet.Cell(row, column++).Value = entry.CustomerName;
        worksheet.Cell(row, column++).Value = entry.ProjectName;
        worksheet.Cell(row, column++).Value = entry.SubprojectNumber;
        worksheet.Cell(row, column++).Value = entry.SubprojectName;
        worksheet.Cell(row, column++).Value = entry.ActivityNumber;
        worksheet.Cell(row, column++).Value = entry.ActivityName;
        worksheet.Cell(row, column++).Value = entry.BillabilityName;
        worksheet.Cell(row, column++).Value = entry.UserName;
        worksheet.Cell(row, column++).Value = entry.Duration.TotalHours;
        worksheet.Cell(row, column++).Value = entry.Comment;
    }
}
