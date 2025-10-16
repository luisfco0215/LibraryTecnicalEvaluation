using LibraryTecnicalEvaluation.Data;
using LibraryTecnicalEvaluation.Models;
using LibraryTecnicalEvaluation.RepositoryContract;
using Microsoft.EntityFrameworkCore;

namespace LibraryTecnicalEvaluation.Repository
{
    public class AutoresRepository(LibreriaContext context) : IAutoresRepository
    {
        private readonly LibreriaContext _context = context;

        public async Task<IEnumerable<Autores>> GetAllAutores()
        {
            return await _context.Autores.ToListAsync();
        }

        public async Task<Autores?> GetAutoresById(int id)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(p => p.Autor_Id == id);

            if (autor is null) return null;

            return autor;
        }

        public async Task CreateAutores(Autores autor)
        {
            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
        }

        public async Task<Autores?> UpdateAutores(Autores autores)
        {
            var autor = await GetAutoresById(autores.Autor_Id);

            if (autor is null) return null;

            autor.Libros = autores.Libros;
            autor.Nombre = autores.Nombre;
            autor.Nacionalidad = autores.Nacionalidad;

            await _context.SaveChangesAsync();
            return autor;
        }

        public async Task DeleteAutores(Autores autor)
        {
            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
        }

    }
}
