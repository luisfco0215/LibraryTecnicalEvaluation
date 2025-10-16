using System.Text.Json.Serialization;

namespace LibraryTecnicalEvaluation.Dtos
{
    public class CreateLibrosDto
    {
        [JsonPropertyName("titulo")]
        public required string Titulo { get; set; }

        [JsonPropertyName("autor_Id")]
        public int Autor_Id { get; set; }

        [JsonPropertyName("año_publicacion")]
        public int Año_publicacion { get; set; }

        [JsonPropertyName("genero")]
        public string? Genero { get; set; }
    }
}
