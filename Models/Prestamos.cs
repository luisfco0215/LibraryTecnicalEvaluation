using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryTecnicalEvaluation.Models
{
    public class Prestamos
    {
        [Key]
        public int Prestamo_Id { get; set; }

        [ForeignKey(nameof(Libros))]
        public int Libro_Id { get; set; }

        public Libros Libros { get; set; } = null!;

        public required DateTime Fecha_Prestamo { get; set; }

        public DateTime? Fecha_Devolucion { get; set; }
        public string? UsuarioId { get; set; }
    }
}
