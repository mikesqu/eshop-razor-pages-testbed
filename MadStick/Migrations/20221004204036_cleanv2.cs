using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadStickWebAppTester.Migrations
{
    public partial class cleanv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlugName",
                table: "MadStickProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SlugName",
                table: "MadStickProducts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
