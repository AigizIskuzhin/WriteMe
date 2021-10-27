using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WriteMe.Database.DAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_Chats_ChatId",
                table: "ChatMessage");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessage_ChatId",
                table: "ChatMessage");

            migrationBuilder.AddColumn<int>(
                name: "ChatDialogId",
                table: "ChatMessage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "ChatMessage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatDialogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserOneId = table.Column<int>(type: "int", nullable: false),
                    UserTwoId = table.Column<int>(type: "int", nullable: false),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatDialogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatDialogs_Users_UserOneId",
                        column: x => x.UserOneId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatDialogs_Users_UserTwoId",
                        column: x => x.UserTwoId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChatParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    GroupChatId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JoinedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatParticipant_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatParticipant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatDialogId",
                table: "ChatMessage",
                column: "ChatDialogId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_GroupChatId",
                table: "ChatMessage",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatDialogs_UserOneId",
                table: "ChatDialogs",
                column: "UserOneId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatDialogs_UserTwoId",
                table: "ChatDialogs",
                column: "UserTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipant_GroupChatId",
                table: "ChatParticipant",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipant_UserId",
                table: "ChatParticipant",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_ChatDialogs_ChatDialogId",
                table: "ChatMessage",
                column: "ChatDialogId",
                principalTable: "ChatDialogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_GroupChats_GroupChatId",
                table: "ChatMessage",
                column: "GroupChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_ChatDialogs_ChatDialogId",
                table: "ChatMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_GroupChats_GroupChatId",
                table: "ChatMessage");

            migrationBuilder.DropTable(
                name: "ChatDialogs");

            migrationBuilder.DropTable(
                name: "ChatParticipant");

            migrationBuilder.DropTable(
                name: "GroupChats");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessage_ChatDialogId",
                table: "ChatMessage");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessage_GroupChatId",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "ChatDialogId",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "ChatMessage");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatId",
                table: "ChatMessage",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_Chats_ChatId",
                table: "ChatMessage",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
