using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaidGroupFinder.Data.Migrations
{
    public partial class pokebattler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RaidBattles_Date",
                table: "RaidBattles");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RaidBattles");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "RaidBattles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RaidBattles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Hatched",
                table: "RaidBattles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_RaidBattles_Active",
                table: "RaidBattles",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_RaidBattles_Created",
                table: "RaidBattles",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_RaidBattles_Hatched",
                table: "RaidBattles",
                column: "Hatched");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RaidBattles_Active",
                table: "RaidBattles");

            migrationBuilder.DropIndex(
                name: "IX_RaidBattles_Created",
                table: "RaidBattles");

            migrationBuilder.DropIndex(
                name: "IX_RaidBattles_Hatched",
                table: "RaidBattles");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "RaidBattles");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "RaidBattles");

            migrationBuilder.DropColumn(
                name: "Hatched",
                table: "RaidBattles");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RaidBattles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_RaidBattles_Date",
                table: "RaidBattles",
                column: "Date");
        }
    }
}
