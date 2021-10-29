using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Database.DAL.Migrations
{
    public partial class chat4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_GroupChat_GroupChatId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "GroupChatParticipant");

            migrationBuilder.DropTable(
                name: "GroupChat");

            migrationBuilder.DropIndex(
                name: "IX_Message_GroupChatId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "RegistrationDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChangeDateTime",
                table: "PrivateChats");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "PrivateChats");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PrivateChats");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SentDateTime",
                table: "Message");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PrivateChats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "PrivateChats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Message",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Message",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "PrivateChats");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "PrivateChats");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Message");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDateTime",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 29, 11, 13, 47, 581, DateTimeKind.Local).AddTicks(98));

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDateTime",
                table: "PrivateChats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 29, 11, 13, 47, 581, DateTimeKind.Local).AddTicks(9610));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "PrivateChats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 29, 11, 13, 47, 581, DateTimeKind.Local).AddTicks(8849));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PrivateChats",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 29, 11, 13, 47, 577, DateTimeKind.Local).AddTicks(8765));

            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "Message",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentDateTime",
                table: "Message",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 29, 11, 13, 47, 585, DateTimeKind.Local).AddTicks(2179));

            migrationBuilder.CreateTable(
                name: "GroupChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2021, 10, 29, 11, 13, 47, 585, DateTimeKind.Local).AddTicks(1085)),
                    CreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2021, 10, 29, 11, 13, 47, 585, DateTimeKind.Local).AddTicks(416)),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChat", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupChatParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GroupChatId = table.Column<int>(type: "int", nullable: true),
                    JoinedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatParticipant_GroupChat_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupChatParticipant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Message_GroupChatId",
                table: "Message",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatParticipant_GroupChatId",
                table: "GroupChatParticipant",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatParticipant_UserId",
                table: "GroupChatParticipant",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_GroupChat_GroupChatId",
                table: "Message",
                column: "GroupChatId",
                principalTable: "GroupChat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
