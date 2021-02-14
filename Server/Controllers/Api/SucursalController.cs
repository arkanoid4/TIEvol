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
    public class SucursalController : ControllerBase
    {
        private readonly ApplicationDataContext _dataContext;

        public SucursalController(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Peticion para obtener todos las Sucursal.
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Sucursal>>> GetSucursal()
        {
            // Retornar lista con todas las sucursal.
            return await _dataContext.Sucursales.ToListAsync();
        }

        [HttpGet("Get/{idSucursal}")]
        public async Task<IActionResult> GetSucursal(int? idSucursal)
        {
            Sucursal sucursal = await _dataContext.Sucursales.FirstOrDefaultAsync(s => s.Id == idSucursal);
            if (sucursal == null)
            {
                return NotFound($"La Sucursal con la id {idSucursal} no existe");
            }
            return Ok(sucursal);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateSucursal([FromBody] Sucursal nuevaSucursal)
        {
            try
            {
                // Validar si el objecto viene vacio
                if (nuevaSucursal == null)
                {
                    return BadRequest("Error: Modelo Sucursal vacio");
                }

                // Validar si no posee la misma id que otro objecto
                if (await _dataContext.Sucursales.AsNoTracking().AnyAsync(sucursal => sucursal.Id == nuevaSucursal.Id))
                {
                    return BadRequest("Error: El id no es valido");
                }

                // Validar si no posee el mismo nombre que otra ciudad
                if (await _dataContext.Sucursales.AsNoTracking().AnyAsync(sucursal => sucursal.Nombre.ToLower() == nuevaSucursal.Nombre.ToLower()))
                {
                    return BadRequest("Error: Ya existe una sucursal con el mismo nombre");
                }

                // Pasa las validaciones y se guarda en la base de datos
                _dataContext.Add(nuevaSucursal);
                await _dataContext.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        [HttpDelete("Delete/{idSucursal}")]
        public async Task<IActionResult> DeleteSucursal(int? idSucursal)
        {
            Sucursal sucursal = await _dataContext.Sucursales.FirstOrDefaultAsync(s => s.Id == idSucursal);

            if (sucursal == null)
            {
                return BadRequest("La cuenta no existe");
            }
            else
            {
                _dataContext.Remove(sucursal);
                await _dataContext.SaveChangesAsync();
                return Ok("Sucursal eliminada");
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateSucursal([FromRoute] int idSucursal, [FromBody] Sucursal sucursal)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(idSucursal))
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

        private bool SucursalExists(int id)
        {
            return _dataContext.Sucursales.Any(s => s.Id == id);
        }
    }
}
