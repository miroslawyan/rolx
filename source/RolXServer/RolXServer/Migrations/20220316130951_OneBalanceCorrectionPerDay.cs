// -----------------------------------------------------------------------
// <copyright file="20220316130951_OneBalanceCorrectionPerDay.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolXServer.Migrations;

/// <summary>
/// Allow at most one balance correction per day.
/// </summary>
public partial class OneBalanceCorrectionPerDay : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "Date",
            table: "UserBalanceCorrections",
            type: "date",
            precision: 0,
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.Sql("update UserBalanceCorrections set Date = DateTime;");

        migrationBuilder.DropColumn(
            name: "DateTime",
            table: "UserBalanceCorrections");

        migrationBuilder.CreateIndex(
            name: "IX_UserBalanceCorrections_UserId_Date",
            table: "UserBalanceCorrections",
            columns: new[] { "UserId", "Date" },
            unique: true);
    }
}
