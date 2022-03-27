// -----------------------------------------------------------------------
// <copyright file="20220325081103_SeedMoreBillabilities.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolXServer.Migrations;

/// <summary>
/// Seeds more billabilities.
/// </summary>
public partial class SeedMoreBillabilities : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Billabilities",
            columns: new[] { "Id", "Inactive", "IsBillable", "Name", "SortingWeight" },
            values: new object[] { 5, false, false, "Abwesenheit", 200 });
    }
}
