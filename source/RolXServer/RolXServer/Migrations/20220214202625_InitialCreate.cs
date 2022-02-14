// -----------------------------------------------------------------------
// <copyright file="20220214202625_InitialCreate.cs" company="Christian Ewald">
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
/// Initially creates the database.
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
public partial class InitialCreate : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Projects",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Number = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Projects", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                GoogleId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FirstName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                LastName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Email = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                AvatarUrl = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Role = table.Column<int>(type: "int", nullable: false),
                EntryDate = table.Column<DateTime>(type: "date", nullable: true),
                LeftDate = table.Column<DateTime>(type: "date", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Phases",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Number = table.Column<int>(type: "int", nullable: false),
                ProjectId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FullName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                StartDate = table.Column<DateTime>(type: "date", nullable: false),
                EndDate = table.Column<DateTime>(type: "date", nullable: true),
                IsBillable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                Budget = table.Column<TimeSpan>(type: "time(6)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Phases", x => x.Id);
                table.ForeignKey(
                    name: "FK_Phases_Projects_ProjectId",
                    column: x => x.ProjectId,
                    principalTable: "Projects",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Records",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Date = table.Column<DateTime>(type: "date", nullable: false),
                UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                PaidLeaveType = table.Column<int>(type: "int", nullable: true),
                PaidLeaveReason = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Records", x => x.Id);
                table.ForeignKey(
                    name: "FK_Records_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "UserBalanceCorrections",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                Overtime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                Vacation = table.Column<TimeSpan>(type: "time(6)", nullable: false),
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
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "UserPartTimeSettings",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                StartDate = table.Column<DateTime>(type: "date", nullable: false),
                Factor = table.Column<double>(type: "double", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserPartTimeSettings", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserPartTimeSettings_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "FavouritePhases",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                PhaseId = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FavouritePhases", x => new { x.UserId, x.PhaseId });
                table.ForeignKey(
                    name: "FK_FavouritePhases_Phases_PhaseId",
                    column: x => x.PhaseId,
                    principalTable: "Phases",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_FavouritePhases_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "RecordEntries",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                RecordId = table.Column<int>(type: "int", nullable: false),
                DurationSeconds = table.Column<long>(type: "bigint", nullable: false),
                Begin = table.Column<TimeSpan>(type: "time", nullable: true),
                Pause = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                Comment = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PhaseId = table.Column<int>(type: "int", nullable: false),
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
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_FavouritePhases_PhaseId",
            table: "FavouritePhases",
            column: "PhaseId");

        migrationBuilder.CreateIndex(
            name: "IX_Phases_ProjectId_Number",
            table: "Phases",
            columns: new[] { "ProjectId", "Number" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Projects_Number",
            table: "Projects",
            column: "Number",
            unique: true);

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

        migrationBuilder.CreateIndex(
            name: "IX_Records_UserId",
            table: "Records",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserBalanceCorrections_UserId",
            table: "UserBalanceCorrections",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserPartTimeSettings_UserId_StartDate",
            table: "UserPartTimeSettings",
            columns: new[] { "UserId", "StartDate" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_GoogleId",
            table: "Users",
            column: "GoogleId",
            unique: true);
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FavouritePhases");

        migrationBuilder.DropTable(
            name: "RecordEntries");

        migrationBuilder.DropTable(
            name: "UserBalanceCorrections");

        migrationBuilder.DropTable(
            name: "UserPartTimeSettings");

        migrationBuilder.DropTable(
            name: "Phases");

        migrationBuilder.DropTable(
            name: "Records");

        migrationBuilder.DropTable(
            name: "Projects");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
