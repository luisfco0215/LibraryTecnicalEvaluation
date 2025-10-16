using System.ComponentModel.DataAnnotations;

namespace LibraryTecnicalEvaluation.Models
{
    public class Autores
    {
        [Key]
        public int Autor_Id { get; set; }
        public required string Nombre { get; set; }
        public required string Nacionalidad { get; set; }
        public ICollection<Libros> Libros { get; set; } = [];
    }
}
