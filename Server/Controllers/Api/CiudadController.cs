using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIEvol.Server.Data;
using TIEvol.Shared.Entities;

namespace TIEvol.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadController : ControllerBase
    {
        private readonly ApplicationDataContext _dataContext;

        public CiudadController(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Peticion para obtener todos las ciudades.
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Ciudad>>> GetCiudades()
        {
            // Retornar lista con todas las personas
            return await _dataContext.Ciudades
                .ToListAsync();
        }

        [HttpGet("Get/{idCiudad}")]
        public async Task<IActionResult> GetCiudad(int? idCiudad)
        {
            Ciudad ciudad = await _dataContext.Ciudades
                .FirstOrDefaultAsync(c => c.Id == idCiudad);

            if (ciudad == null)
            {
                return NotFound($"La ciudad con la id {idCiudad} no existe");
            }

            return Ok(ciudad);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCiudad([FromBody] Ciudad nuevaCiudad)
        {
            try
            {
                // Validar si el objecto viene vacio
                if (nuevaCiudad == null)
                {
                    return BadRequest("Error: Modelo Ciudad vacio");
                }

                // Validar si no posee la misma id que otro objecto
                if (await _dataContext.Ciudades
                    .AsNoTracking()
                    .AnyAsync(ciudad => ciudad.Id == nuevaCiudad.Id))
                {
                    return BadRequest("Error: El id no es valido");
                }

                // Validar si no posee el mismo nombre que otra ciudad
                if (await _dataContext.Ciudades.AsNoTracking().AnyAsync(ciudad => ciudad.Nombre.ToLower() == nuevaCiudad.Nombre.ToLower()))
                {
                    return BadRequest("Error: Ya existe una ciudad con el mismo nombre");
                }

                // Pasa las validaciones y se guarda en la base de datos
                _dataContext.Add(nuevaCiudad);
                await _dataContext.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        [HttpDelete("Delete/{idCiudad}")]
        public async Task<IActionResult> DeleteCiudad(int? idCiudad)
        {
            Ciudad ciudad = await _dataContext.Ciudades
                .FirstOrDefaultAsync(c => c.Id == idCiudad);

            if (ciudad == null)
            {
                return BadRequest("La cuenta no existe");
            }
            else
            {
                _dataContext.Remove(ciudad);
                await _dataContext.SaveChangesAsync();
                return Ok("Ciudad eliminada");
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateCiudad([FromRoute] int idCiudad, [FromBody] Ciudad ciudad)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Entry(ciudad).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadExists(idCiudad))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CiudadExists(int id)
        {
            return _dataContext.Ciudades.Any(c => c.Id == id);
        }
    }
}
