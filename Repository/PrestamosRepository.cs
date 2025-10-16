using LibraryTecnicalEvaluation.Data;
using LibraryTecnicalEvaluation.Models;
using LibraryTecnicalEvaluation.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryTecnicalEvaluation.Repository
{
    public class PrestamosRepository(LibreriaContext context) : IPrestamosRepository
    {
        private readonly LibreriaContext _context = context;

        public async Task<IEnumerable<Prestamos>> GetAllPrestamos()
        {
            try
            {
                return await _context.Prestamos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener prestamos", ex);
            }
        }

        public async Task<IEnumerable<object>> GetPrestamosNoDevueltos()
        {
            try
            {
                var sw = Stopwatch.StartNew();

                var prestamos = await _context.Prestamos
                    .Where(p => p.Fecha_Devolucion == null)
                    .Select(p => new
                    {
                        Autor_Id = p.Libros.Autores.Autor_Id,
                        Nombre = p.Libros.Autores.Nombre,
                        Libro_Id = p.Libros.Libro_Id,
                        Titulo = p.Libros.Titulo
                    })
                    .ToListAsync();

                sw.Stop();
                Console.WriteLine($"Tiempo ejecución: {sw.ElapsedMilliseconds} ms");

                return prestamos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener prestamos no devueltos", ex);
            }
        }

        public async Task<Prestamos?> GetPrestamoById(int id)
        {
            try
            {
                var prestamo = await _context.Prestamos
                    .Include(p => p.Libros)
                    .FirstOrDefaultAsync(p => p.Prestamo_Id == id);

                if (prestamo is null) return null;

                return prestamo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar prestamos", ex);
            }
        }

        public async Task CreatePrestamos(Prestamos prestamo)
        {
            try
            {
                await _context.Prestamos.AddAsync(prestamo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear prestamos", ex);
            }
        }

        public async Task<Prestamos?> UpdateFechaDevolucion(int id, DateTime fechaDevolucion)
        {
            try
            {
                var prestamo = await _context.Prestamos.FindAsync(id);

                if (prestamo is null)
                    return null;

                prestamo.Fecha_Devolucion = fechaDevolucion;
                await _context.SaveChangesAsync();

                return prestamo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar prestamos", ex);
            }

        }

        public async Task DeletePrestamos(Prestamos prestamo)
        {
            try
            {
                _context.Prestamos.Remove(prestamo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar prestamos", ex);
            }
        }
    }
}
