using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomAuthenticationSampleProject.Migrations
{
    public partial class addthumbnailfileextentionfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileExtention",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailFileExtention",
                table: "Users");
        }
    }
}
