using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomAuthenticationSampleProject.Migrations
{
    public partial class addthumbnailandthumbnailurlfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Users");
        }
    }
}
