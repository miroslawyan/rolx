using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolXServer.Migrations
{
    public partial class AddActivityPlannedTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PlannedSeconds",
                table: "Activities",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedSeconds",
                table: "Activities");
        }
    }
}
