// -----------------------------------------------------------------------
// <copyright file="20200206195248_AddEntryDate.cs" company="Christian Ewald">
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
    /// Adds the entry date to the users.
    /// </summary>
    public partial class AddEntryDate : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "Users",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "Users");
        }
    }
}
