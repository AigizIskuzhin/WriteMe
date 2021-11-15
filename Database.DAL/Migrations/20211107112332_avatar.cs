using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.DAL.Migrations
{
    public partial class avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_ReportStates_ReportStateId",
                table: "PostReports");

            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ReportStateId",
                table: "PostReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_ReportStates_ReportStateId",
                table: "PostReports",
                column: "ReportStateId",
                principalTable: "ReportStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_ReportStates_ReportStateId",
                table: "PostReports");

            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ReportStateId",
                table: "PostReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_ReportStates_ReportStateId",
                table: "PostReports",
                column: "ReportStateId",
                principalTable: "ReportStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
