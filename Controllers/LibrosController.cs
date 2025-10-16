using Azure.Core;
using LibraryTecnicalEvaluation.Dtos;
using LibraryTecnicalEvaluation.Models;
using LibraryTecnicalEvaluation.Repository;
using LibraryTecnicalEvaluation.RepositoryContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryTecnicalEvaluation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController(ILibrosRepository librosRepository) : ControllerBase
    {
        private readonly ILibrosRepository _librosRepository = librosRepository;
        private string? GetCurrentUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _librosRepository.GetAllLibros());
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

        [HttpGet("antes-de/{anioLimite}")]
        public async Task<IActionResult> GetLibrosAntesDe2000(int anioLimite)
        {
            try
            {
                var libros = await _librosRepository.GetLibrosAntesDe2000(anioLimite);

                if (libros == null || !libros.Any())
                    return NotFound(new { mensaje = $"No se encontraron libros publicados antes del año {anioLimite}." });

                return Ok(libros);
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


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateLibrosDto request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var libro = new Libros
                {
                    Titulo = request.Titulo,
                    Autor_Id = request.Autor_Id,
                    Año_publicacion = request.Año_publicacion,
                    Genero = request.Genero
                };

                await _librosRepository.CreateLibros(libro);

                return CreatedAtAction(nameof(GetById), new { id = libro.Libro_Id }, libro);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensaje = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al crear libros.",
                    detalle = ex.Message
                });
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var libro = await _librosRepository.GetLibrosById(id);
                if (libro == null) return NotFound();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al obtener los libros por Id.",
                    detalle = ex.Message
                });
            }
        }
    }
}
