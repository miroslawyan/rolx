// -----------------------------------------------------------------------
// <copyright file="20220326145000_AddEditLocks.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolXServer.Migrations;

/// <summary>
/// Adds and seeds the edit-locks table.
/// </summary>
public partial class AddEditLocks : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "EditLocks",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Date = table.Column<DateTime>(type: "date", precision: 0, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EditLocks", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.InsertData(
            table: "EditLocks",
            columns: new[] { "Id", "Date" },
            values: new object[] { 1, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
    }
}
