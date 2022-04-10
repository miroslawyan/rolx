// -----------------------------------------------------------------------
// <copyright file="20220407204955_AddProjectManager.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolXServer.Migrations;

/// <summary>
/// Add a manager to the subprojects.
/// </summary>
public partial class AddProjectManager : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "ManagerId",
            table: "Subprojects",
            type: "char(36)",
            nullable: true,
            collation: "ascii_general_ci");

        migrationBuilder.UpdateData(
            table: "Billabilities",
            keyColumn: "Id",
            keyValue: 4,
            column: "SortingWeight",
            value: 5);

        migrationBuilder.InsertData(
            table: "Billabilities",
            columns: new[] { "Id", "Inactive", "IsBillable", "Name", "SortingWeight" },
            values: new object[] { 6, false, true, "Verrechenbar Extern", 3 });

        migrationBuilder.InsertData(
            table: "Billabilities",
            columns: new[] { "Id", "Inactive", "IsBillable", "Name", "SortingWeight" },
            values: new object[] { 7, false, true, "Verrechenbar Nearshore", 4 });

        migrationBuilder.CreateIndex(
            name: "IX_Subprojects_ManagerId",
            table: "Subprojects",
            column: "ManagerId");

        migrationBuilder.AddForeignKey(
            name: "FK_Subprojects_Users_ManagerId",
            table: "Subprojects",
            column: "ManagerId",
            principalTable: "Users",
            principalColumn: "Id");
    }
}
