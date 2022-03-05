using Microsoft.EntityFrameworkCore.Migrations;

namespace Madeyra.Migrations
{
    public partial class SettingsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderLogo = table.Column<string>(maxLength: 150, nullable: true),
                    FooterLogo = table.Column<string>(maxLength: 150, nullable: true),
                    TelIcon = table.Column<string>(maxLength: 70, nullable: true),
                    Tel = table.Column<string>(maxLength: 50, nullable: true),
                    FacebookIcon = table.Column<string>(maxLength: 70, nullable: true),
                    FacebookUrl = table.Column<string>(maxLength: 150, nullable: true),
                    YoutubeIcon = table.Column<string>(maxLength: 70, nullable: true),
                    YoutubeUrl = table.Column<string>(maxLength: 150, nullable: true),
                    InstagramIcon = table.Column<string>(maxLength: 70, nullable: true),
                    InstagramUrl = table.Column<string>(maxLength: 150, nullable: true),
                    WhatsappIcon = table.Column<string>(maxLength: 70, nullable: true),
                    WhatsappUrl = table.Column<string>(maxLength: 150, nullable: true),
                    Adress = table.Column<string>(maxLength: 150, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Faks = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
