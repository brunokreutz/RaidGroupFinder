using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaidGroupFinder.Data.Migrations
{
    public partial class dateTimeoffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                table: "ChatHistories",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistories_Date",
                table: "ChatHistories",
                column: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatHistories_Date",
                table: "ChatHistories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ChatHistories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
