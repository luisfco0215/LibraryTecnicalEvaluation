using LibraryTecnicalEvaluation.Models;

namespace LibraryTecnicalEvaluation.RepositoryContract
{
    public interface IPrestamosRepository
    {
        Task<IEnumerable<Prestamos>> GetAllPrestamos();
        Task<IEnumerable<object>> GetPrestamosNoDevueltos();
        Task<Prestamos?> GetPrestamoById(int id);
        Task CreatePrestamos(Prestamos prestamos);
        Task<Prestamos?> UpdateFechaDevolucion(int id, DateTime fechaDevolucion);
        Task DeletePrestamos(Prestamos id);
    }
}
