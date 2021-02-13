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
    public class BodegaController : ControllerBase
    {
        private readonly ApplicationDataContext _dataContext;

        public BodegaController(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Peticion para obtener todos las Bodegas.
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Bodega>>> GetBodega()
        {
            // Retornar lista con todas las personas
            return await _dataContext.Bodegas.ToListAsync();
        }

        [HttpGet("Get/{idBodega}")]
        public async Task<IActionResult> GetBodega(int? idBodega)
        {
            Bodega bodega = await _dataContext.Bodegas.FirstOrDefaultAsync(b => b.Id == idBodega);
            if (bodega == null)
            {
                return NotFound($"La ciudad con la id {idBodega} no existe");
            }
            return Ok(bodega);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBodega([FromBody] Bodega nuevaBodega)
        {
            try
            {
                // Validar si el objecto viene vacio
                if (nuevaBodega == null)
                {
                    return BadRequest("Error: Modelo Bodega vacio");
                }

                // Validar si no posee la misma id que otro objecto
                if (await _dataContext.Bodegas.AsNoTracking().AnyAsync(bodega => bodega.Id == nuevaBodega.Id))
                {
                    return BadRequest("Error: El id no es valido");
                }

                // Validar si no posee el mismo nombre que otra ciudad
                if (await _dataContext.Bodegas.AsNoTracking().AnyAsync(bodega => bodega.Codigo.ToLower() == nuevaBodega.Codigo.ToLower()))
                {
                    return BadRequest("Error: Ya existe una bodega con el mismo nombre");
                }

                // Pasa las validaciones y se guarda en la base de datos
                _dataContext.Add(nuevaBodega);
                await _dataContext.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        [HttpDelete("Delete/{idBodega}")]
        public async Task<IActionResult> DeleteBodega(int? idBodega)
        {
            Bodega bodega = await _dataContext.Bodegas.FirstOrDefaultAsync(b => b.Id == idBodega);

            if (bodega == null)
            {
                return BadRequest("La cuenta no existe");
            }
            else
            {
                _dataContext.Remove(bodega);
                await _dataContext.SaveChangesAsync();
                return Ok("Bodega eliminada");
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateBodega([FromRoute] int idBodega, [FromBody] Bodega bodega)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Entry(bodega).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodegaExists(idBodega))
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
            return _dataContext.Bodegas.Any(b => b.Id == id);
        }
    }
}
