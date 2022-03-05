using Microsoft.EntityFrameworkCore.Migrations;

namespace Madeyra.Migrations
{
    public partial class DesignTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_DesignId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DesignId",
                table: "Products",
                column: "DesignId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_DesignId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DesignId",
                table: "Products",
                column: "DesignId",
                unique: true);
        }
    }
}
