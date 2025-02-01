using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadStickWebAppTester.Migrations
{
    public partial class AddedEndpointPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndpointPermission",
                columns: table => new
                {
                    EndpointPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndpointPermission", x => x.EndpointPermissionId);
                    table.ForeignKey(
                        name: "FK_EndpointPermission_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Endpoint",
                columns: table => new
                {
                    EndpointId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAllowedAccess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsAllowedModification = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EndpointPermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endpoint", x => x.EndpointId);
                    table.ForeignKey(
                        name: "FK_Endpoint_EndpointPermission_EndpointPermissionId",
                        column: x => x.EndpointPermissionId,
                        principalTable: "EndpointPermission",
                        principalColumn: "EndpointPermissionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Endpoint_EndpointPermissionId",
                table: "Endpoint",
                column: "EndpointPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EndpointPermission_UserId",
                table: "EndpointPermission",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endpoint");

            migrationBuilder.DropTable(
                name: "EndpointPermission");
        }
    }
}
