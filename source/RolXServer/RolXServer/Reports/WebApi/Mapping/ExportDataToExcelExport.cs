// -----------------------------------------------------------------------
// <copyright file="ExportDataToExcelExport.cs" company="Christian Ewald">
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
/// Maps <see cref="ExportData"/> instances to Excel exports.
/// </summary>
public static class ExportDataToExcelExport
{
    /// <summary>
    /// Returns the specified data as Excel report with the specified file name.
    /// </summary>
    /// <param name="controller">The controller.</param>
    /// <param name="exportData">The export data.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>The file stream result containing the Excel report.</returns>
    public static FileStreamResult ExcelExport(this ControllerBase controller, IEnumerable<ExportData> exportData, string fileName)
    {
        var stream = new MemoryStream();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Data");
            worksheet.AddHeader();

            var row = 2;
            foreach (var item in exportData)
            {
                worksheet.AddData(item, row++);
            }

            workbook.SaveAs(stream);
        }

        stream.Position = 0;
        return controller.File(
            stream,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: fileName);
    }

    private static void AddHeader(this IXLWorksheet worksheet)
    {
        var column = 1;
        worksheet.Cell(1, column++).Value = "Datum";
        worksheet.Cell(1, column++).Value = "Projekt Nr";
        worksheet.Cell(1, column++).Value = "Kunde";
        worksheet.Cell(1, column++).Value = "Projekt";
        worksheet.Cell(1, column++).Value = "Subprojekt Nr";
        worksheet.Cell(1, column++).Value = "Subprojekt";
        worksheet.Cell(1, column++).Value = "Aktivität Nr";
        worksheet.Cell(1, column++).Value = "Aktivität";
        worksheet.Cell(1, column++).Value = "Verrechenbarkeit";
        worksheet.Cell(1, column++).Value = "Mitarbeiter";
        worksheet.Cell(1, column++).Value = "Zeit [h]";
        worksheet.Cell(1, column++).Value = "Kommentar";
    }

    private static void AddData(this IXLWorksheet worksheet, ExportData data, int row)
    {
        var column = 1;
        worksheet.Cell(row, column++).Value = data.Date;
        worksheet.Cell(row, column++).Value = data.ProjectNumber;
        worksheet.Cell(row, column++).Value = data.CustomerName;
        worksheet.Cell(row, column++).Value = data.ProjectName;
        worksheet.Cell(row, column++).Value = data.SubprojectNumber;
        worksheet.Cell(row, column++).Value = data.SubprojectName;
        worksheet.Cell(row, column++).Value = data.ActivityNumber;
        worksheet.Cell(row, column++).Value = data.ActivityName;
        worksheet.Cell(row, column++).Value = data.BillabilityName;
        worksheet.Cell(row, column++).Value = data.UserName;
        worksheet.Cell(row, column++).Value = data.Duration.TotalHours;
        worksheet.Cell(row, column++).Value = data.Comment;
    }
}
