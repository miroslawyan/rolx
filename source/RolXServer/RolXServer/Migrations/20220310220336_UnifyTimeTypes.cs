// -----------------------------------------------------------------------
// <copyright file="20220310220336_UnifyTimeTypes.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolXServer.Migrations;

/// <summary>
/// Unify the mapping of the time-related properties.
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
public partial class UnifyTimeTypes : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Overtime",
            table: "UserBalanceCorrections");

        migrationBuilder.DropColumn(
            name: "Vacation",
            table: "UserBalanceCorrections");

        migrationBuilder.DropColumn(
            name: "Pause",
            table: "RecordEntries");

        migrationBuilder.RenameColumn(
            name: "Budget",
            table: "Activities",
            newName: "BudgetSeconds");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DateTime",
            table: "UserBalanceCorrections",
            type: "datetime(0)",
            precision: 0,
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime(6)");

        migrationBuilder.AddColumn<long>(
            name: "OvertimeSeconds",
            table: "UserBalanceCorrections",
            type: "bigint",
            nullable: false,
            defaultValue: 0L);

        migrationBuilder.AddColumn<long>(
            name: "VacationSeconds",
            table: "UserBalanceCorrections",
            type: "bigint",
            nullable: false,
            defaultValue: 0L);

        migrationBuilder.AlterColumn<TimeSpan>(
            name: "Begin",
            table: "RecordEntries",
            type: "time(0)",
            precision: 0,
            nullable: true,
            oldClrType: typeof(TimeSpan),
            oldType: "time",
            oldNullable: true);

        migrationBuilder.AddColumn<long>(
            name: "PauseSeconds",
            table: "RecordEntries",
            type: "bigint",
            nullable: true);
    }
}
