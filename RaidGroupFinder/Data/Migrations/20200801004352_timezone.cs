using Microsoft.EntityFrameworkCore.Migrations;

namespace RaidGroupFinder.Data.Migrations
{
    public partial class timezone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "AspNetUsers");
        }
    }
}
