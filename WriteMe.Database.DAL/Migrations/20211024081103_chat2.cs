using Microsoft.EntityFrameworkCore.Migrations;

namespace WriteMe.Database.DAL.Migrations
{
    public partial class chat2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatParticipant_Chats_GroupChatId",
                table: "ChatParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_ReceiverId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_SenderId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ReceiverId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_SenderId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_ChatParticipant_GroupChatId",
                table: "ChatParticipant");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "ChatParticipant");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Messages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipant_ChatId",
                table: "ChatParticipant",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatParticipant_Chats_ChatId",
                table: "ChatParticipant",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatParticipant_Chats_ChatId",
                table: "ChatParticipant");

            migrationBuilder.DropIndex(
                name: "IX_ChatParticipant_ChatId",
                table: "ChatParticipant");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Chats",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Chats",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "ChatParticipant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ReceiverId",
                table: "Chats",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SenderId",
                table: "Chats",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipant_GroupChatId",
                table: "ChatParticipant",
                column: "GroupChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatParticipant_Chats_GroupChatId",
                table: "ChatParticipant",
                column: "GroupChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_ReceiverId",
                table: "Chats",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_SenderId",
                table: "Chats",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
