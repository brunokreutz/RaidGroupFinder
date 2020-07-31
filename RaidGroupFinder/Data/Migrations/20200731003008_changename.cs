using Microsoft.EntityFrameworkCore.Migrations;

namespace RaidGroupFinder.Data.Migrations
{
    public partial class changename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatHistory",
                table: "ChatHistory");

            migrationBuilder.RenameTable(
                name: "ChatHistory",
                newName: "ChatHistories");

            migrationBuilder.RenameIndex(
                name: "IX_ChatHistory_Group",
                table: "ChatHistories",
                newName: "IX_ChatHistories_Group");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatHistories",
                table: "ChatHistories",
                column: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatHistories",
                table: "ChatHistories");

            migrationBuilder.RenameTable(
                name: "ChatHistories",
                newName: "ChatHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ChatHistories_Group",
                table: "ChatHistory",
                newName: "IX_ChatHistory_Group");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatHistory",
                table: "ChatHistory",
                column: "Guid");
        }
    }
}
