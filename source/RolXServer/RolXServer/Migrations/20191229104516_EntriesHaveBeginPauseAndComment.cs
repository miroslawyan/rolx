// -----------------------------------------------------------------------
// <copyright file="20191229104516_EntriesHaveBeginPauseAndComment.cs" company="Christian Ewald">
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
    /// Adds the begin, pause and comment columns to the record entries.
    /// </summary>
    public partial class EntriesHaveBeginPauseAndComment : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Begin",
                table: "RecordEntries",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "RecordEntries",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Pause",
                table: "RecordEntries",
                nullable: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Begin",
                table: "RecordEntries");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "RecordEntries");

            migrationBuilder.DropColumn(
                name: "Pause",
                table: "RecordEntries");
        }
    }
}
