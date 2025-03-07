﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadStickWebAppTester.Migrations
{
    public partial class AddedNullableToSlugName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SlugName",
                table: "MadStickProducts",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MadStickProducts",
                keyColumn: "SlugName",
                keyValue: null,
                column: "SlugName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SlugName",
                table: "MadStickProducts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
