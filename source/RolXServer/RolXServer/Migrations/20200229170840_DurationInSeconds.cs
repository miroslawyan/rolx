// -----------------------------------------------------------------------
// <copyright file="20200229170840_DurationInSeconds.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

namespace RolXServer.Migrations;

/// <summary>
/// Store the recorded durations in number of seconds.
/// </summary>
public partial class DurationInSeconds : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<long>(
            name: "DurationSeconds",
            table: "RecordEntries",
            nullable: false,
            defaultValue: 0L);

        migrationBuilder.Sql("UPDATE \"RecordEntries\" SET \"DurationSeconds\" = EXTRACT(epoch FROM \"Duration\")");

        migrationBuilder.DropColumn(
            name: "Duration",
            table: "RecordEntries");
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<TimeSpan>(
            name: "Duration",
            table: "RecordEntries",
            type: "interval",
            nullable: false,
            defaultValue: new TimeSpan(0, 0, 0, 0, 0));

        migrationBuilder.Sql("UPDATE \"RecordEntries\" SET \"Duration\" = (\"DurationSeconds\" || ' second')::interval");

        migrationBuilder.DropColumn(
            name: "DurationSeconds",
            table: "RecordEntries");
    }
}
