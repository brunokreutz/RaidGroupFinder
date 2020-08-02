using Microsoft.EntityFrameworkCore.Migrations;

namespace RaidGroupFinder.Data.Migrations
{
    public partial class addHostIdToRaidbattle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Host",
                table: "RaidBattles",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "HostUserId",
                table: "RaidBattles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostUserId",
                table: "RaidBattles");

            migrationBuilder.AlterColumn<string>(
                name: "Host",
                table: "RaidBattles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450);
        }
    }
}
