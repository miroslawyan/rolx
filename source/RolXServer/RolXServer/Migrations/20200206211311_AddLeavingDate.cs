// -----------------------------------------------------------------------
// <copyright file="20200206211311_AddLeavingDate.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

namespace RolXServer.Migrations;

/// <summary>
/// Adds the leaving date to the users.
/// </summary>
public partial class AddLeavingDate : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "LeavingDate",
            table: "Users",
            type: "date",
            nullable: true);
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "LeavingDate",
            table: "Users");
    }
}
