using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flixdi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class actualizaciondedatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actores_Paises_PaisId",
                table: "Actores");

            migrationBuilder.DropForeignKey(
                name: "FK_Directores_Paises_PaisId",
                table: "Directores");

            migrationBuilder.DropForeignKey(
                name: "FK_EstudioCinematografico_Paises_PaisId",
                table: "EstudioCinematografico");

            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_EstudioCinematografico_EstudioCinematograficoId",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_EstudioCinematograficoId",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_EstudioCinematografico_PaisId",
                table: "EstudioCinematografico");

            migrationBuilder.DropIndex(
                name: "IX_Directores_PaisId",
                table: "Directores");

            migrationBuilder.DropIndex(
                name: "IX_Actores_PaisId",
                table: "Actores");

            migrationBuilder.DropColumn(
                name: "EstudioCinematograficoId",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "PaisId",
                table: "EstudioCinematografico");

            migrationBuilder.DropColumn(
                name: "PaisId",
                table: "Directores");

            migrationBuilder.DropColumn(
                name: "PaisId",
                table: "Actores");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_IdEstudioCinematografico",
                table: "Peliculas",
                column: "IdEstudioCinematografico");

            migrationBuilder.CreateIndex(
                name: "IX_EstudioCinematografico_IdPais",
                table: "EstudioCinematografico",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Directores_IdPais",
                table: "Directores",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Actores_IdPais",
                table: "Actores",
                column: "IdPais");

            migrationBuilder.AddForeignKey(
                name: "FK_Actores_Paises_IdPais",
                table: "Actores",
                column: "IdPais",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Directores_Paises_IdPais",
                table: "Directores",
                column: "IdPais",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EstudioCinematografico_Paises_IdPais",
                table: "EstudioCinematografico",
                column: "IdPais",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_EstudioCinematografico_IdEstudioCinematografico",
                table: "Peliculas",
                column: "IdEstudioCinematografico",
                principalTable: "EstudioCinematografico",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actores_Paises_IdPais",
                table: "Actores");

            migrationBuilder.DropForeignKey(
                name: "FK_Directores_Paises_IdPais",
                table: "Directores");

            migrationBuilder.DropForeignKey(
                name: "FK_EstudioCinematografico_Paises_IdPais",
                table: "EstudioCinematografico");

            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_EstudioCinematografico_IdEstudioCinematografico",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_IdEstudioCinematografico",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_EstudioCinematografico_IdPais",
                table: "EstudioCinematografico");

            migrationBuilder.DropIndex(
                name: "IX_Directores_IdPais",
                table: "Directores");

            migrationBuilder.DropIndex(
                name: "IX_Actores_IdPais",
                table: "Actores");

            migrationBuilder.AddColumn<int>(
                name: "EstudioCinematograficoId",
                table: "Peliculas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaisId",
                table: "EstudioCinematografico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaisId",
                table: "Directores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaisId",
                table: "Actores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_EstudioCinematograficoId",
                table: "Peliculas",
                column: "EstudioCinematograficoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudioCinematografico_PaisId",
                table: "EstudioCinematografico",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Directores_PaisId",
                table: "Directores",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Actores_PaisId",
                table: "Actores",
                column: "PaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actores_Paises_PaisId",
                table: "Actores",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Directores_Paises_PaisId",
                table: "Directores",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EstudioCinematografico_Paises_PaisId",
                table: "EstudioCinematografico",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_EstudioCinematografico_EstudioCinematograficoId",
                table: "Peliculas",
                column: "EstudioCinematograficoId",
                principalTable: "EstudioCinematografico",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
