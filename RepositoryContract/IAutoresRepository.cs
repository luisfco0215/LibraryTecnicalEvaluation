using LibraryTecnicalEvaluation.Models;

namespace LibraryTecnicalEvaluation.RepositoryContract
{
    public interface IAutoresRepository
    {
        Task<IEnumerable<Autores>> GetAllAutores();
        Task<Autores?> GetAutoresById(int id);
        Task CreateAutores(Autores autores);
        Task<Autores?> UpdateAutores(Autores autores);
        Task DeleteAutores(Autores id);
    }
}
