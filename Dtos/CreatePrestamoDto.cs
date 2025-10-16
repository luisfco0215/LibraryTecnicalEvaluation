using LibraryTecnicalEvaluation.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryTecnicalEvaluation.Dtos
{
    public class CreatePrestamoDto
    {
        [JsonPropertyName("libro_Id")]
        public int Libro_Id { get; set; }

        [JsonPropertyName("fecha_Prestamo")]
        public required DateTime Fecha_Prestamo { get; set; }

        [JsonPropertyName("fecha_Devolucion")]
        public DateTime? Fecha_Devolucion { get; set; }
    }
}
