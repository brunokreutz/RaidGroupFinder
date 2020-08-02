using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RaidGroupFinder.Data.Migrations
{
    public partial class pokeRaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Number = table.Column<int>(nullable: false),
                    Form = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => new { x.Number, x.Form });
                });

            migrationBuilder.CreateTable(
                name: "Raids",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonNumber = table.Column<int>(nullable: true),
                    PokemonForm = table.Column<string>(nullable: true),
                    Tier = table.Column<byte>(nullable: false),
                    Available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raids_Pokemons_PokemonNumber_PokemonForm",
                        columns: x => new { x.PokemonNumber, x.PokemonForm },
                        principalTable: "Pokemons",
                        principalColumns: new[] { "Number", "Form" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaidBattles",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    RaidId = table.Column<short>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    Host = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaidBattles", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_RaidBattles_Raids_RaidId",
                        column: x => x.RaidId,
                        principalTable: "Raids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Available",
                table: "Pokemons",
                column: "Available");

            migrationBuilder.CreateIndex(
                name: "IX_RaidBattles_Date",
                table: "RaidBattles",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_RaidBattles_RaidId",
                table: "RaidBattles",
                column: "RaidId");

            migrationBuilder.CreateIndex(
                name: "IX_Raids_Available",
                table: "Raids",
                column: "Available");

            migrationBuilder.CreateIndex(
                name: "IX_Raids_PokemonNumber_PokemonForm",
                table: "Raids",
                columns: new[] { "PokemonNumber", "PokemonForm" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaidBattles");

            migrationBuilder.DropTable(
                name: "Raids");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
