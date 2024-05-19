using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliothequeVieuxMontreal.Migrations
{
    public partial class bib : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Auteur = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Cetegorie = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Retard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_membre = table.Column<int>(type: "int", nullable: false),
                    Id_livre = table.Column<int>(type: "int", nullable: false),
                    Nbjour = table.Column<int>(type: "int", nullable: false),
                    DatePret = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pret",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_livre = table.Column<int>(type: "int", nullable: false),
                    Id_membre = table.Column<int>(type: "int", nullable: false),
                    date_debut = table.Column<DateTime>(type: "date", nullable: false),
                    date_fin = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pret", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Pret__Id_livre__5FB337D6",
                        column: x => x.Id_livre,
                        principalTable: "Livre",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Pret__Id_membre__60A75C0F",
                        column: x => x.Id_membre,
                        principalTable: "Membre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pret_Id_livre",
                table: "Pret",
                column: "Id_livre");

            migrationBuilder.CreateIndex(
                name: "IX_Pret_Id_membre",
                table: "Pret",
                column: "Id_membre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pret");

            migrationBuilder.DropTable(
                name: "Retard");

            migrationBuilder.DropTable(
                name: "Livre");

            migrationBuilder.DropTable(
                name: "Membre");
        }
    }
}
