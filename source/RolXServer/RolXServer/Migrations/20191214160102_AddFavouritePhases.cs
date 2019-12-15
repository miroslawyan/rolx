// -----------------------------------------------------------------------
// <copyright file="20191214160102_AddFavouritePhases.cs" company="Christian Ewald">
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
    /// Add the table for the favourite phases and a previously forgotten foreign key.
    /// </summary>
    public partial class AddFavouritePhases : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouritePhases",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    PhaseId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouritePhases", x => new { x.UserId, x.PhaseId });
                    table.ForeignKey(
                        name: "FK_FavouritePhases_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouritePhases_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_UserId",
                table: "Records",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritePhases_PhaseId",
                table: "FavouritePhases",
                column: "PhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records");

            migrationBuilder.DropTable(
                name: "FavouritePhases");

            migrationBuilder.DropIndex(
                name: "IX_Records_UserId",
                table: "Records");
        }
    }
}
