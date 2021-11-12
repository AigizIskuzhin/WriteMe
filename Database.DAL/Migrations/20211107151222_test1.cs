using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.DAL.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipApplications_Users_UserOneId",
                table: "FriendshipApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipApplications_Users_UserTwoId",
                table: "FriendshipApplications");

            migrationBuilder.AlterColumn<int>(
                name: "UserTwoId",
                table: "FriendshipApplications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserOneId",
                table: "FriendshipApplications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationStateUserTwo",
                table: "FriendshipApplications",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationStateUserOne",
                table: "FriendshipApplications",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<int>(
                name: "UserOneFriendshipTypeId",
                table: "FriendshipApplications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserTwoFriendshipTypeId",
                table: "FriendshipApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FriendshipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipApplications_UserOneFriendshipTypeId",
                table: "FriendshipApplications",
                column: "UserOneFriendshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipApplications_UserTwoFriendshipTypeId",
                table: "FriendshipApplications",
                column: "UserTwoFriendshipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipApplications_FriendshipTypes_UserOneFriendshipType~",
                table: "FriendshipApplications",
                column: "UserOneFriendshipTypeId",
                principalTable: "FriendshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipApplications_FriendshipTypes_UserTwoFriendshipType~",
                table: "FriendshipApplications",
                column: "UserTwoFriendshipTypeId",
                principalTable: "FriendshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipApplications_Users_UserOneId",
                table: "FriendshipApplications",
                column: "UserOneId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipApplications_Users_UserTwoId",
                table: "FriendshipApplications",
                column: "UserTwoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipApplications_FriendshipTypes_UserOneFriendshipType~",
                table: "FriendshipApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipApplications_FriendshipTypes_UserTwoFriendshipType~",
                table: "FriendshipApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipApplications_Users_UserOneId",
                table: "FriendshipApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipApplications_Users_UserTwoId",
                table: "FriendshipApplications");

            migrationBuilder.DropTable(
                name: "FriendshipTypes");

            migrationBuilder.DropIndex(
                name: "IX_FriendshipApplications_UserOneFriendshipTypeId",
                table: "FriendshipApplications");

            migrationBuilder.DropIndex(
                name: "IX_FriendshipApplications_UserTwoFriendshipTypeId",
                table: "FriendshipApplications");

            migrationBuilder.DropColumn(
                name: "UserOneFriendshipTypeId",
                table: "FriendshipApplications");

            migrationBuilder.DropColumn(
                name: "UserTwoFriendshipTypeId",
                table: "FriendshipApplications");

            migrationBuilder.AlterColumn<int>(
                name: "UserTwoId",
                table: "FriendshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserOneId",
                table: "FriendshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ApplicationStateUserTwo",
                table: "FriendshipApplications",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "ApplicationStateUserOne",
                table: "FriendshipApplications",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipApplications_Users_UserOneId",
                table: "FriendshipApplications",
                column: "UserOneId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipApplications_Users_UserTwoId",
                table: "FriendshipApplications",
                column: "UserTwoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
