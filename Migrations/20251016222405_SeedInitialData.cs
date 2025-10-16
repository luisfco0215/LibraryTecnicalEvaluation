using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryTecnicalEvaluation.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Autor_Id", "Nacionalidad", "Nombre" },
                values: new object[,]
                {
                    { 1, "Colombiano", "Gabriel García Márquez" },
                    { 2, "Chilena", "Isabel Allende" },
                    { 3, "Británica", "J.K. Rowling" }
                });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Libro_Id", "Autor_Id", "Año_publicacion", "Genero", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, 1967, "Realismo Mágico", "Cien Años de Soledad" },
                    { 2, 2, 1982, "Novela", "La Casa de los Espíritus" },
                    { 3, 3, 1997, "Fantasía", "Harry Potter y la Piedra Filosofal" }
                });

            migrationBuilder.InsertData(
                table: "Prestamos",
                columns: new[] { "Prestamo_Id", "Fecha_Devolucion", "Fecha_Prestamo", "Libro_Id", "UsuarioId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "user1" },
                    { 2, new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "user2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Libro_Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Prestamo_Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Prestamo_Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Autor_Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Libro_Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Libro_Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Autor_Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Autor_Id",
                keyValue: 2);
        }
    }
}
