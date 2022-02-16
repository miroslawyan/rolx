// -----------------------------------------------------------------------
// <copyright file="20220216212255_InitialCreate.cs" company="Christian Ewald">
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
            name: "Subprojects",
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
                SubprojectId = table.Column<int>(type: "int", nullable: false),
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
                table.PrimaryKey("PK_Activities", x => x.Id);
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
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_RecordEntries_Records_RecordId",
                    column: x => x.RecordId,
                    principalTable: "Records",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.InsertData(
            table: "Subprojects",
            columns: new[] { "Id", "Name", "Number" },
            values: new object[] { 1, "Lockheed Martin", "P0001" });

        migrationBuilder.InsertData(
            table: "Subprojects",
            columns: new[] { "Id", "Name", "Number" },
            values: new object[] { 2, "SRF", "P0002" });

        migrationBuilder.InsertData(
            table: "Activities",
            columns: new[] { "Id", "Budget", "EndDate", "FullName", "IsBillable", "Name", "Number", "StartDate", "SubprojectId" },
            values: new object[,]
            {
                    { 1, null, null, "P0001.001 - Lockheed Martin - F35", true, "F35", 1, new DateTime(2021, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, null, null, "P0001.002 - Lockheed Martin - F117-A", false, "F117-A", 2, new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, null, new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "P0001.003 - Lockheed Martin - HaGaHuWa", true, "HaGaHuWa", 3, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, null, null, "P0002.001 - SRF - Malony", true, "Malony", 1, new DateTime(2021, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
            });

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
            name: "IX_Subprojects_Number",
            table: "Subprojects",
            column: "Number",
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
            name: "Subprojects");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
