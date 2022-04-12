// -----------------------------------------------------------------------
// <copyright file="20220412161055_InitialCreate.cs" company="Christian Ewald">
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
public partial class InitialCreate : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Billabilities",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                IsBillable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                SortingWeight = table.Column<int>(type: "int", nullable: false),
                Inactive = table.Column<bool>(type: "tinyint(1)", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Billabilities", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "EditLocks",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Date = table.Column<DateOnly>(type: "date", precision: 0, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EditLocks", x => x.Id);
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
                EntryDate = table.Column<DateOnly>(type: "date", precision: 0, nullable: false),
                LeftDate = table.Column<DateOnly>(type: "date", precision: 0, nullable: true),
                IsConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Records",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Date = table.Column<DateOnly>(type: "date", precision: 0, nullable: false),
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
            name: "Subprojects",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Number = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                ProjectNumber = table.Column<int>(type: "int", nullable: false),
                ProjectName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                CustomerName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                ManagerId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Subprojects", x => x.Id);
                table.ForeignKey(
                    name: "FK_Subprojects_Users_ManagerId",
                    column: x => x.ManagerId,
                    principalTable: "Users",
                    principalColumn: "Id");
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "UserBalanceCorrections",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Date = table.Column<DateOnly>(type: "date", precision: 0, nullable: false),
                OvertimeSeconds = table.Column<long>(type: "bigint", nullable: false),
                VacationSeconds = table.Column<long>(type: "bigint", nullable: false),
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
                StartDate = table.Column<DateOnly>(type: "date", precision: 0, nullable: false),
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
            name: "Activities",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Number = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                StartDate = table.Column<DateOnly>(type: "date", precision: 0, nullable: false),
                EndedDate = table.Column<DateOnly>(type: "date", precision: 0, nullable: true),
                BudgetSeconds = table.Column<long>(type: "bigint", nullable: true),
                SubprojectId = table.Column<int>(type: "int", nullable: false),
                BillabilityId = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Activities", x => x.Id);
                table.ForeignKey(
                    name: "FK_Activities_Billabilities_BillabilityId",
                    column: x => x.BillabilityId,
                    principalTable: "Billabilities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Activities_Subprojects_SubprojectId",
                    column: x => x.SubprojectId,
                    principalTable: "Subprojects",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "FavouriteActivities",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                ActivityId = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FavouriteActivities", x => new { x.UserId, x.ActivityId });
                table.ForeignKey(
                    name: "FK_FavouriteActivities_Activities_ActivityId",
                    column: x => x.ActivityId,
                    principalTable: "Activities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_FavouriteActivities_Users_UserId",
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
                Begin = table.Column<TimeOnly>(type: "time(0)", precision: 0, nullable: true),
                PauseSeconds = table.Column<long>(type: "bigint", nullable: true),
                Comment = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                ActivityId = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RecordEntries", x => x.Id);
                table.ForeignKey(
                    name: "FK_RecordEntries_Activities_ActivityId",
                    column: x => x.ActivityId,
                    principalTable: "Activities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_RecordEntries_Records_RecordId",
                    column: x => x.RecordId,
                    principalTable: "Records",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.InsertData(
            table: "Billabilities",
            columns: new[] { "Id", "Inactive", "IsBillable", "Name", "SortingWeight" },
            values: new object[,]
            {
                { 1, false, false, "Nicht verrechenbar", 100 },
                { 2, false, true, "Verrechenbar Engineering", 10 },
                { 3, false, true, "Verrechenbar TP", 20 },
                { 4, false, true, "Verrechenbar Extern", 30 },
                { 5, false, true, "Verrechenbar Nearshore", 40 },
                { 6, false, true, "Verrechenbar 50+", 50 },
                { 7, false, false, "Abwesenheit", 200 },
            });

        migrationBuilder.InsertData(
            table: "EditLocks",
            columns: new[] { "Id", "Date" },
            values: new object[] { 1, new DateOnly(2022, 1, 1) });

        migrationBuilder.InsertData(
            table: "Subprojects",
            columns: new[] { "Id", "CustomerName", "ManagerId", "Name", "Number", "ProjectName", "ProjectNumber" },
            values: new object[] { 1, "M&F", null, "Bezahlte Abwesenheiten", 1, "Allgemein", 8900 });

        migrationBuilder.InsertData(
            table: "Activities",
            columns: new[] { "Id", "BillabilityId", "BudgetSeconds", "EndedDate", "Name", "Number", "StartDate", "SubprojectId" },
            values: new object[,]
            {
                { 1, 7, null, null, "Ferien", 1, new DateOnly(2020, 1, 1), 1 },
                { 2, 7, null, null, "Krank", 2, new DateOnly(2020, 1, 1), 1 },
                { 3, 7, null, null, "Militär", 3, new DateOnly(2020, 1, 1), 1 },
                { 4, 7, null, null, "Sonstige", 4, new DateOnly(2020, 1, 1), 1 },
            });

        migrationBuilder.CreateIndex(
            name: "IX_Activities_BillabilityId",
            table: "Activities",
            column: "BillabilityId");

        migrationBuilder.CreateIndex(
            name: "IX_Activities_SubprojectId_Number",
            table: "Activities",
            columns: new[] { "SubprojectId", "Number" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_FavouriteActivities_ActivityId",
            table: "FavouriteActivities",
            column: "ActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_RecordEntries_ActivityId",
            table: "RecordEntries",
            column: "ActivityId");

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
            name: "IX_Subprojects_ManagerId",
            table: "Subprojects",
            column: "ManagerId");

        migrationBuilder.CreateIndex(
            name: "IX_Subprojects_ProjectNumber_Number",
            table: "Subprojects",
            columns: new[] { "ProjectNumber", "Number" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserBalanceCorrections_UserId",
            table: "UserBalanceCorrections",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserBalanceCorrections_UserId_Date",
            table: "UserBalanceCorrections",
            columns: new[] { "UserId", "Date" },
            unique: true);

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
}
