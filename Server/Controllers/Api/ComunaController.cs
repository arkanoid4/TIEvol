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
    public class ComunaController : ControllerBase
    {
        private readonly ApplicationDataContext _dataContext;

        public ComunaController(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Peticion para obtener todos las Comunas.
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Comuna>>> GetComuna()
        {
            // Retornar lista con todas las comunas
            return await _dataContext.Comunas.ToListAsync();
        }

        [HttpGet("Get/{idComuna}")]
        public async Task<IActionResult> GetComuna(int? idComuna)
        {
            Comuna comuna = await _dataContext.Comunas.FirstOrDefaultAsync(c => c.Id == idComuna);
            if (comuna == null)
            {
                return NotFound($"La comuna con la id {idComuna} no existe");
            }
            return Ok(comuna);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateComuna([FromBody] Comuna nuevaComuna)
        {
            try
            {
                // Validar si el objecto viene vacio
                if (nuevaComuna == null)
                {
                    return BadRequest("Error: Modelo Comuna vacio");
                }

                // Validar si no posee la misma id que otro objecto
                if (await _dataContext.Comunas.AsNoTracking().AnyAsync(comuna => comuna.Id == nuevaComuna.Id))
                {
                    return BadRequest("Error: El id no es valido");
                }

                // Validar si no posee el mismo nombre que otra comuna
                if (await _dataContext.Comunas.AsNoTracking().AnyAsync(comuna => comuna.Nombre.ToLower() == nuevaComuna.Nombre.ToLower()))
                {
                    return BadRequest("Error: Ya existe una comuna con el mismo nombre");
                }

                // Pasa las validaciones y se guarda en la base de datos
                _dataContext.Add(nuevaComuna);
                await _dataContext.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        [HttpDelete("Delete/{idComuna}")]
        public async Task<IActionResult> DeleteComuna(int? idComuna)
        {
            Comuna comuna = await _dataContext.Comunas.FirstOrDefaultAsync(c => c.Id == idComuna);

            if (comuna == null)
            {
                return BadRequest("La comuna no existe");
            }
            else
            {
                _dataContext.Remove(comuna);
                await _dataContext.SaveChangesAsync();
                return Ok("Comuna eliminada");
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateComuna([FromRoute] int idComuna, [FromBody] Comuna comuna)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Entry(comuna).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodegaExists(idComuna))
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

        private bool BodegaExists(int id)
        {
            return _dataContext.Comunas.Any(c => c.Id == id);
        }
    }
}