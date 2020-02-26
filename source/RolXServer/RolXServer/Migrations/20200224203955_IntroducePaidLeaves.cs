// -----------------------------------------------------------------------
// <copyright file="20200224203955_IntroducePaidLeaves.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

namespace RolXServer.Migrations
{
    /// <summary>
    /// Introduce paid leaves.
    /// </summary>
    public partial class IntroducePaidLeaves : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaidLeaveReason",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaidLeaveType",
                table: "Records",
                nullable: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidLeaveReason",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "PaidLeaveType",
                table: "Records");
        }
    }
}
