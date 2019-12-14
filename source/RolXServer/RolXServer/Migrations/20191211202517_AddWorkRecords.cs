// -----------------------------------------------------------------------
// <copyright file="20191211202517_AddWorkRecords.cs" company="Christian Ewald">
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
    /// Add the tables for the work records.
    /// </summary>
    public partial class AddWorkRecords : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetHours",
                table: "Phases");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Budget",
                table: "Phases",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecordId = table.Column<int>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    PhaseId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordEntries_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordEntries_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordEntries_PhaseId",
                table: "RecordEntries",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordEntries_RecordId",
                table: "RecordEntries",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_Date_UserId",
                table: "Records",
                columns: new[] { "Date", "UserId" },
                unique: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordEntries");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Phases");

            migrationBuilder.AddColumn<double>(
                name: "BudgetHours",
                table: "Phases",
                type: "double precision",
                nullable: true);
        }
    }
}
