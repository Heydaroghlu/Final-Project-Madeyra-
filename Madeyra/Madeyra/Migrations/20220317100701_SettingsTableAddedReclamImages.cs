using Microsoft.EntityFrameworkCore.Migrations;

namespace Madeyra.Migrations
{
    public partial class SettingsTableAddedReclamImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "ProductComments");

            migrationBuilder.AddColumn<string>(
                name: "Reclam1",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reclam2",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reclam1",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Reclam2",
                table: "Settings");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "ProductComments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
