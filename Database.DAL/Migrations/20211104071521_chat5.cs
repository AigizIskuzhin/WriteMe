using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.DAL.Migrations
{
    public partial class chat5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystemPost",
                table: "Posts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemPost",
                table: "Posts");
        }
    }
}
