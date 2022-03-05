using Microsoft.EntityFrameworkCore.Migrations;

namespace Madeyra.Migrations
{
    public partial class ProductUpdateForPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Preference",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preference",
                table: "Products");
        }
    }
}
