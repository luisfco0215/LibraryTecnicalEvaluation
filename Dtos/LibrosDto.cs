using LibraryTecnicalEvaluation.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryTecnicalEvaluation.Dtos
{
    public class LibrosDto
    {
        public int Libro_Id { get; set; }

        public string? Titulo { get; set; }

        public int Año_publicacion { get; set; }

    }
}
