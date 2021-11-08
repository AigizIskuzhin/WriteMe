using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.DAL.Migrations
{
    public partial class chat8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemPost",
                table: "SystemPosts");

            migrationBuilder.DropColumn(
                name: "IsSystemPost",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystemPost",
                table: "SystemPosts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemPost",
                table: "Posts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
