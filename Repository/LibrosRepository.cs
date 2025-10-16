using LibraryTecnicalEvaluation.Data;
using LibraryTecnicalEvaluation.Dtos;
using LibraryTecnicalEvaluation.Models;
using LibraryTecnicalEvaluation.RepositoryContract;
using Microsoft.EntityFrameworkCore;

namespace LibraryTecnicalEvaluation.Repository
{
    public class LibrosRepository(LibreriaContext context) : ILibrosRepository
    {
        private readonly LibreriaContext _context = context;


        public async Task<IEnumerable<Libros>> GetAllLibros()
        {
            try
            {
                return await _context.Libros.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los libros", ex);
            }
        }

        public async Task<IEnumerable<LibrosDto>> GetLibrosAntesDe2000(int añoLimite)
        {
            try
            {
                var libros = await _context.Libros
                    .Where(l => l.Año_publicacion < añoLimite)
                    .Select(l => new LibrosDto
                    {
                        Libro_Id = l.Libro_Id,
                        Titulo = l.Titulo,
                        Año_publicacion = l.Año_publicacion
                    })
                    .ToListAsync();

                return libros;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los libros antes del año 2000", ex);
            }
        }

        public async Task<Libros?> GetLibrosById(int id)
        {
            try
            {
                var libro = await _context.Libros
                    .Include(autor => autor)
                    .FirstOrDefaultAsync(libro => libro.Libro_Id == id);

                if (libro == null) return null;

                return libro;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los libros por Id", ex);
            }

        }
        public async Task CreateLibros(Libros libro)
        {
            try
            {
                var autorExiste = await _context.Autores
                    .AnyAsync(a => a.Autor_Id == libro.Autor_Id);

                if (!autorExiste)
                {
                    throw new KeyNotFoundException($"El autor con ID {libro.Autor_Id} no existe.");
                }

                await _context.Libros.AddAsync(libro);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear libros", ex);
            }
        }

        public async Task<Libros?> UpdateLibros(Libros libro)
        {
            try
            {
                var libroUpdate = await GetLibrosById(libro.Libro_Id);

                if (libroUpdate is null) return null;

                libroUpdate.Autores = libro.Autores;
                libroUpdate.Titulo = libro.Titulo;
                libroUpdate.Año_publicacion = libro.Año_publicacion;
                libroUpdate.Genero = libro.Genero;

                await _context.SaveChangesAsync();

                return libroUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar libros", ex);
            }
        }

        public async Task DeleteLibros(Libros libro)
        {
            try
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar libros", ex);
            }
        }

    }
}
