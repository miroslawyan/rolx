// -----------------------------------------------------------------------
// <copyright file="20220214205049_SeedSomeProjects.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolXServer.Migrations;

/// <summary>
/// Seed some projects.
/// </summary>
public partial class SeedSomeProjects : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Projects",
            columns: new[] { "Id", "Name", "Number" },
            values: new object[] { 1, "Lockheed Martin", "P0001" });

        migrationBuilder.InsertData(
            table: "Projects",
            columns: new[] { "Id", "Name", "Number" },
            values: new object[] { 2, "SRF", "P0002" });

        migrationBuilder.InsertData(
            table: "Phases",
            columns: new[] { "Id", "Budget", "EndDate", "FullName", "IsBillable", "Name", "Number", "ProjectId", "StartDate" },
            values: new object[,]
            {
                    { 1, null, null, "P0001.001 - Lockheed Martin - F35", true, "F35", 1, 1, new DateTime(2021, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, null, "P0001.002 - Lockheed Martin - F117-A", false, "F117-A", 2, 1, new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "P0001.003 - Lockheed Martin - HaGaHuWa", true, "HaGaHuWa", 3, 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, null, "P0002.001 - SRF - Malony", true, "Malony", 1, 2, new DateTime(2021, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
            });
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Phases",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Phases",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Phases",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Phases",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "Projects",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Projects",
            keyColumn: "Id",
            keyValue: 2);
    }
}
