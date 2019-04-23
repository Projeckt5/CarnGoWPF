using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstTryProject.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "Lejer",
                columns: table => new 
                {
                    Kontaktinformation = table.Column<string>(nullable: false)
                        .Annotation("Sqlserver:Autoincrement", false),
                    Navn = table.Column<string>(nullable: false),
                    Kørekortnummer = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Lejer", x => x.Kontaktinformation); });
                
            migrationBuilder.CreateTable(
                name: "Udlejer",
                columns: table => new
                {
                    Kontaktinformation = table.Column<string>(nullable: false)
                        .Annotation("Sqlserver:Autoincrement", false),
                    Navn = table.Column<string>(nullable: false),
                    Kørekortnummer = table.Column<string>(nullable: false),
                    Registreringsnummer = table.Column<string>(nullable: false),
                },
                constraints: table => { table.PrimaryKey("PK_Udlejer", x => x.Kontaktinformation); });

            migrationBuilder.CreateTable(
                name: "Bil",
                columns: table => new
                {
                    Nummerplade = table.Column<string>(nullable: false)
                        .Annotation("Sqlserver:Autoincrement", false),
                    Billede = table.Column<string>(nullable: false), // TODO, how to save a picture? -- Erik Mowinckel
                    Anhænger = table.Column<bool>(nullable: false),
                    Tilstand = table.Column<string>(nullable: false),
                    Reserveret = table.Column<bool>(nullable: false),
                    Vægt = table.Column<int>(nullable: false),
                    Højde = table.Column<int>(nullable: false),
                    Bredte = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Område = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bil", x => x.Nummerplade);
                });

            migrationBuilder.CreateTable(
                name: "UdlejedeDage",
                columns: table => new
                {
                    Dato = table.Column<DateTime>(nullable: false)
                        .Annotation("Sqlserver:Autoincrement", false),
                    LejerID = table.Column<string>(nullable: false),
                },
                constraints: table => { table.PrimaryKey("PK_UdlejedeDage", x => x.Dato); });

            migrationBuilder.CreateTable(
                name: "PossibleToRentDay",
                columns: table => new
                {
                    Dato = table.Column<DateTime>(nullable: false)
                        .Annotation("Sqlserver:Autoincrement", false),
                },
                constraints: table => { table.PrimaryKey("PK_PossibleToRentDay", x => x.Dato); });
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UdlejedeDage");

            migrationBuilder.DropTable(
                name: "PossibleToRentDay");

            migrationBuilder.DropTable(
                name: "Bil");

            migrationBuilder.DropTable(
                name: "Udlejer");

            migrationBuilder.DropTable(
                name: "Lejer");
        }
        
    }
}
