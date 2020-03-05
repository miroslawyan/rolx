// -----------------------------------------------------------------------
// <copyright file="20200304211226_RefactorUsers.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace RolXServer.Migrations
{
    /// <summary>
    /// Refactor the users module.
    /// </summary>
    public partial class RefactorUsers : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.RenameColumn(
                name: "LeavingDate",
                table: "Users",
                newName: "LeftDate");

            migrationBuilder.CreateTable(
                name: "UserPartTimeSettings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    Factor = table.Column<double>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPartTimeSettings", x => new { x.UserId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_UserPartTimeSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPartTimeSettings");

            migrationBuilder.RenameColumn(
                name: "LeftDate",
                table: "Users",
                newName: "LeavingDate");

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    PartTimeFactor = table.Column<double>(type: "double precision", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => new { x.UserId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
