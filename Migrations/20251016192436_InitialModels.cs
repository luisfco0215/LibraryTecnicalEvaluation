using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryTecnicalEvaluation.Migrations
{
    /// <inheritdoc />
    public partial class InitialModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Autor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Autor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    Libro_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor_Id = table.Column<int>(type: "int", nullable: false),
                    Año_publicacion = table.Column<int>(type: "int", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.Libro_Id);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_Autor_Id",
                        column: x => x.Autor_Id,
                        principalTable: "Autores",
                        principalColumn: "Autor_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Prestamo_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libro_Id = table.Column<int>(type: "int", nullable: false),
                    Fecha_Prestamo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Devolucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Prestamo_Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Libros_Libro_Id",
                        column: x => x.Libro_Id,
                        principalTable: "Libros",
                        principalColumn: "Libro_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_Autor_Id",
                table: "Libros",
                column: "Autor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_Libro_Id",
                table: "Prestamos",
                column: "Libro_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
