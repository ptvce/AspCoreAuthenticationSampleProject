using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomAuthenticationSampleProject.Migrations
{
    public partial class addareaname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaName",
                table: "Permission",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaName",
                table: "Permission");
        }
    }
}
