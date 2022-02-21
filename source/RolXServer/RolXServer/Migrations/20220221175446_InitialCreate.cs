// -----------------------------------------------------------------------
// <copyright file="20220221175446_InitialCreate.cs" company="Christian Ewald">
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
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Subprojects", x => x.Id);
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
            name: "Activities",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Number = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                StartDate = table.Column<DateTime>(type: "date", nullable: false),
                EndDate = table.Column<DateTime>(type: "date", nullable: true),
                Budget = table.Column<long>(type: "bigint", nullable: true),
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
                Begin = table.Column<TimeSpan>(type: "time", nullable: true),
                Pause = table.Column<TimeSpan>(type: "time(6)", nullable: true),
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
                    { 2, false, true, "Verrechenbar Engineering", 1 },
                    { 3, false, true, "Verrechenbar TP", 2 },
                    { 4, false, true, "Verrechenbar 50+", 3 },
            });

        migrationBuilder.InsertData(
            table: "Subprojects",
            columns: new[] { "Id", "CustomerName", "Name", "Number", "ProjectName", "ProjectNumber" },
            values: new object[,]
            {
                    { 1, "Lockheed Martin", "F35", 1, "Auto Pilot", 4711 },
                    { 2, "Lockheed Martin", "F117A", 2, "Auto Pilot", 4711 },
                    { 3, "SRF", "Fragengenerator", 1, "ABC SRF 3", 3141 },
            });

        migrationBuilder.InsertData(
            table: "Activities",
            columns: new[] { "Id", "BillabilityId", "Budget", "EndDate", "Name", "Number", "StartDate", "SubprojectId" },
            values: new object[,]
            {
                    { 11, 2, null, null, "Take off", 1, new DateTime(2021, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, 1, null, null, "Cruise", 2, new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 13, 3, null, new DateTime(2022, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Landing", 3, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 21, 4, null, null, "Take off", 1, new DateTime(2021, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 22, 1, null, null, "Cruise", 2, new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 23, 2, null, new DateTime(2022, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Landing", 3, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 31, 3, null, null, "Analyse", 1, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 32, 4, null, null, "Umsetzung", 2, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 33, 2, null, null, "Übergabe", 3, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
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
            name: "IX_Subprojects_ProjectNumber_Number",
            table: "Subprojects",
            columns: new[] { "ProjectNumber", "Number" },
            unique: true);

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
            name: "FavouriteActivities");

        migrationBuilder.DropTable(
            name: "RecordEntries");

        migrationBuilder.DropTable(
            name: "UserBalanceCorrections");

        migrationBuilder.DropTable(
            name: "UserPartTimeSettings");

        migrationBuilder.DropTable(
            name: "Activities");

        migrationBuilder.DropTable(
            name: "Records");

        migrationBuilder.DropTable(
            name: "Billabilities");

        migrationBuilder.DropTable(
            name: "Subprojects");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
