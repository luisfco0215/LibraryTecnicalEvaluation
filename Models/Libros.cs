using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryTecnicalEvaluation.Models
{
    public class Libros
    {
        [Key]
        public int Libro_Id { get; set; }

        public required string Titulo { get; set; }

        [ForeignKey(nameof(Autores))]
        public int Autor_Id { get; set; }

        public Autores Autores { get; set; } = null!;

        public required int Año_publicacion { get; set; }

        public string? Genero { get; set; }

        public ICollection<Prestamos> Prestamos { get; set; } = [];
    }
}
