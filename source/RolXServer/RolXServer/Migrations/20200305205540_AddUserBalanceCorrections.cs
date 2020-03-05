// -----------------------------------------------------------------------
// <copyright file="20200305205540_AddUserBalanceCorrections.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RolXServer.Migrations
{
    /// <summary>
    /// Add table for balance corrections.
    /// </summary>
    public partial class AddUserBalanceCorrections : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPartTimeSettings",
                table: "UserPartTimeSettings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPartTimeSettings",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPartTimeSettings",
                table: "UserPartTimeSettings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserBalanceCorrections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Overtime = table.Column<TimeSpan>(nullable: false),
                    Vacation = table.Column<TimeSpan>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBalanceCorrections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBalanceCorrections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPartTimeSettings_UserId_StartDate",
                table: "UserPartTimeSettings",
                columns: new[] { "UserId", "StartDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBalanceCorrections_UserId",
                table: "UserBalanceCorrections",
                column: "UserId");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBalanceCorrections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPartTimeSettings",
                table: "UserPartTimeSettings");

            migrationBuilder.DropIndex(
                name: "IX_UserPartTimeSettings_UserId_StartDate",
                table: "UserPartTimeSettings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPartTimeSettings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPartTimeSettings",
                table: "UserPartTimeSettings",
                columns: new[] { "UserId", "StartDate" });
        }
    }
}
