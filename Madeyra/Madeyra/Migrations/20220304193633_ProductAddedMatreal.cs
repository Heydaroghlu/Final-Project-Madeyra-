using Microsoft.EntityFrameworkCore.Migrations;

namespace Madeyra.Migrations
{
    public partial class ProductAddedMatreal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Material",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Preference",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "MatrealId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Matreals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matreals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductMatreal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    MatrealId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMatreal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMatreal_Matreals_MatrealId",
                        column: x => x.MatrealId,
                        principalTable: "Matreals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMatreal_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_MatrealId",
                table: "Products",
                column: "MatrealId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMatreal_MatrealId",
                table: "ProductMatreal",
                column: "MatrealId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMatreal_ProductId",
                table: "ProductMatreal",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Matreals_MatrealId",
                table: "Products",
                column: "MatrealId",
                principalTable: "Matreals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Matreals_MatrealId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductMatreal");

            migrationBuilder.DropTable(
                name: "Matreals");

            migrationBuilder.DropIndex(
                name: "IX_Products_MatrealId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MatrealId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Preference",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
