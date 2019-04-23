using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstTryProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biler",
                columns: table => new
                {
                    Nummerplade = table.Column<string>(nullable: false),
                    Billede = table.Column<string>(nullable: true),
                    Anhænger = table.Column<bool>(nullable: false),
                    Tilstand = table.Column<string>(nullable: true),
                    Reserveret = table.Column<bool>(nullable: false),
                    Vægt = table.Column<int>(nullable: false),
                    Højde = table.Column<int>(nullable: false),
                    Bredte = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Område = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biler", x => x.Nummerplade);
                });

            migrationBuilder.CreateTable(
                name: "PossibleToRentDayer",
                columns: table => new
                {
                    Dato = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PossibleToRentDayer", x => x.Dato);
                });

            migrationBuilder.CreateTable(
                name: "Lejere",
                columns: table => new
                {
                    Kontaktinformation = table.Column<string>(nullable: false),
                    Navn = table.Column<string>(nullable: true),
                    Kørekortnummer = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lejere", x => x.Kontaktinformation);
                });

            migrationBuilder.CreateTable(
                name: "UdlejedeDage",
                columns: table => new
                {
                    Dato = table.Column<DateTime>(nullable: false),
                    LejerID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UdlejedeDage", x => x.Dato);
                });

            migrationBuilder.CreateTable(
                name: "Udlejere",
                columns: table => new
                {
                    Kontaktinformation = table.Column<string>(nullable: false),
                    Navn = table.Column<string>(nullable: true),
                    Kørekortnummer = table.Column<string>(nullable: false),
                    Registreringsnummer = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Udlejere", x => x.Kontaktinformation);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biler");

            migrationBuilder.DropTable(
                name: "PossibleToRentDayer");

            migrationBuilder.DropTable(
                name: "Lejere");

            migrationBuilder.DropTable(
                name: "UdlejedeDage");

            migrationBuilder.DropTable(
                name: "Udlejere");
        }
    }
}
