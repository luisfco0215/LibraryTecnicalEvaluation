using LibraryTecnicalEvaluation.Dtos;
using LibraryTecnicalEvaluation.Models;
using LibraryTecnicalEvaluation.RepositoryContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LibraryTecnicalEvaluation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController(IPrestamosRepository prestamosRepository, ILibrosRepository librosRepository) : ControllerBase
    {
        private readonly IPrestamosRepository _prestamosRepository = prestamosRepository;
        private readonly ILibrosRepository _librosRepository = librosRepository;
        private string? GetCurrentUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        [HttpGet]
        public async Task<IActionResult> GetAllPrestamos()
        {
            try
            {
                return Ok(await _prestamosRepository.GetAllPrestamos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al obtener los libros.",
                    detalle = ex.Message
                });
            }
        }


        [HttpGet("no-devueltos")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetNoDevueltos()
        {
            try
            {
                var result = await _prestamosRepository.GetPrestamosNoDevueltos();

                if (!result.Any())
                    return NotFound(new { mensaje = "No hay préstamos pendientes de devolución." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al obtener los préstamos pendientes de devolución.",
                    detalle = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var prestamo = await _prestamosRepository.GetPrestamoById(id);

                if (prestamo is null) return NotFound();
                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al obtener los préstamos por Id.",
                    detalle = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePrestamoDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var currentUser = GetCurrentUserId();

                if (currentUser is null)
                    return Unauthorized();

                var libroExists = await _librosRepository.GetLibrosById(request.Libro_Id);

                if (libroExists is null)
                    return NotFound(new { mensaje = "El libro especificado no existe." });

                var prestamo = new Prestamos
                {
                    Libro_Id = request.Libro_Id,
                    Fecha_Prestamo = request.Fecha_Prestamo,
                    Fecha_Devolucion = request.Fecha_Devolucion,
                    UsuarioId = currentUser
                };

                await _prestamosRepository.CreatePrestamos(prestamo);

                return CreatedAtAction(nameof(GetById), new { id = prestamo.Prestamo_Id }, prestamo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al crear el préstamo.",
                    detalle = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePrestamoDto request)
        {
            try
            {
                if (request.Fecha_Devolucion == null)
                    return BadRequest(new { mensaje = "Debe especificar una fecha de devolución válida." });

                var prestamo = await _prestamosRepository.UpdateFechaDevolucion(id, request.Fecha_Devolucion.Value);

                if (prestamo is null)
                    return NotFound(new { mensaje = $"No se encontró un préstamo con ID {id}." });

                return Ok(new
                {
                    mensaje = "Fecha de devolución actualizada correctamente.",
                    prestamo.Prestamo_Id,
                    prestamo.Fecha_Devolucion
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al editar los préstamos.",
                    detalle = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var prestamo = await _prestamosRepository.GetPrestamoById(id);
                if (prestamo is null)
                    return NotFound(new { mensaje = $"No se encontró un préstamo con ID {id}." });

                var currentUser = GetCurrentUserId();
                if (!User.IsInRole("Admin"))
                {
                    if (currentUser == null || prestamo.UsuarioId != currentUser)
                        return Forbid();
                }

                await _prestamosRepository.DeletePrestamos(prestamo);

                return Ok(new { mensaje = $"El préstamo con ID {id} fue eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al eliminar los préstamos.",
                    detalle = ex.Message
                });
            }
        }
    }
}
