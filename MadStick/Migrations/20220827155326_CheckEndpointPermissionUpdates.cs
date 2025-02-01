using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadStickWebAppTester.Migrations
{
    public partial class CheckEndpointPermissionUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endpoint_EndpointPermission_EndpointPermissionId",
                table: "Endpoint");

            migrationBuilder.DropForeignKey(
                name: "FK_EndpointPermission_AspNetUsers_UserId",
                table: "EndpointPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EndpointPermission",
                table: "EndpointPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endpoint",
                table: "Endpoint");

            migrationBuilder.RenameTable(
                name: "EndpointPermission",
                newName: "EndpointPermissions");

            migrationBuilder.RenameTable(
                name: "Endpoint",
                newName: "Endpoints");

            migrationBuilder.RenameIndex(
                name: "IX_EndpointPermission_UserId",
                table: "EndpointPermissions",
                newName: "IX_EndpointPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Endpoint_EndpointPermissionId",
                table: "Endpoints",
                newName: "IX_Endpoints_EndpointPermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EndpointPermissions",
                table: "EndpointPermissions",
                column: "EndpointPermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endpoints",
                table: "Endpoints",
                column: "EndpointId");

            migrationBuilder.AddForeignKey(
                name: "FK_EndpointPermissions_AspNetUsers_UserId",
                table: "EndpointPermissions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Endpoints_EndpointPermissions_EndpointPermissionId",
                table: "Endpoints",
                column: "EndpointPermissionId",
                principalTable: "EndpointPermissions",
                principalColumn: "EndpointPermissionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EndpointPermissions_AspNetUsers_UserId",
                table: "EndpointPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Endpoints_EndpointPermissions_EndpointPermissionId",
                table: "Endpoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endpoints",
                table: "Endpoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EndpointPermissions",
                table: "EndpointPermissions");

            migrationBuilder.RenameTable(
                name: "Endpoints",
                newName: "Endpoint");

            migrationBuilder.RenameTable(
                name: "EndpointPermissions",
                newName: "EndpointPermission");

            migrationBuilder.RenameIndex(
                name: "IX_Endpoints_EndpointPermissionId",
                table: "Endpoint",
                newName: "IX_Endpoint_EndpointPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_EndpointPermissions_UserId",
                table: "EndpointPermission",
                newName: "IX_EndpointPermission_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endpoint",
                table: "Endpoint",
                column: "EndpointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EndpointPermission",
                table: "EndpointPermission",
                column: "EndpointPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Endpoint_EndpointPermission_EndpointPermissionId",
                table: "Endpoint",
                column: "EndpointPermissionId",
                principalTable: "EndpointPermission",
                principalColumn: "EndpointPermissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EndpointPermission_AspNetUsers_UserId",
                table: "EndpointPermission",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
