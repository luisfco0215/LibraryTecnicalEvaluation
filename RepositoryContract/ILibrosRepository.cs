using LibraryTecnicalEvaluation.Dtos;
using LibraryTecnicalEvaluation.Models;

namespace LibraryTecnicalEvaluation.RepositoryContract
{
    public interface ILibrosRepository
    {
        Task<IEnumerable<Libros>> GetAllLibros();
        Task<IEnumerable<LibrosDto>> GetLibrosAntesDe2000(int añoLimite);
        Task<Libros?> GetLibrosById(int id);
        Task CreateLibros(Libros libros);
        Task<Libros?> UpdateLibros(Libros libro);
        Task DeleteLibros(Libros libro);
    }
}
