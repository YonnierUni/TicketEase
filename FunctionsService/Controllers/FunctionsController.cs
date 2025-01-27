using Microsoft.AspNetCore.Mvc;
using TicketEase.Service.Functions.Entities;
using TicketEase.Service.Functions.Models;
using TicketEase.Service.Functions.Repositories;
using TicketEase.Service.Functions.Services;

namespace TicketEase.Service.Functions.Controllers
{
    [Route("api/functions")]
    [ApiController]
    public class FunctionsController : ControllerBase
    {
        private readonly IFunctionsService _service;

        public FunctionsController(IFunctionsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFunctions()
        {
            try
            {
                var functions = await _service.GetAllFunctionsAsync();
                return Ok(functions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las funciones", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFunctionById(Guid id)
        {
            try
            {
                var function = await _service.GetFunctionByIdAsync(id);
                return Ok(function);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Función con ID {id} no encontrada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener la función", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFunction([FromBody] MovieFunctionDto functionDto)
        {
            try
            {
                await _service.AddFunctionAsync(functionDto);
                return CreatedAtAction(nameof(GetFunctionById), new { id = functionDto.MovieFunctionId }, functionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al agregar la función", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFunction(Guid id, [FromBody] MovieFunctionDto functionDto)
        {
            try
            {
                await _service.UpdateFunctionAsync(id, functionDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Función con ID {id} no encontrada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar la función", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFunction(Guid id)
        {
            try
            {
                await _service.DeleteFunctionAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Función con ID {id} no encontrada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar la función", error = ex.Message });
            }
        }
    }
}
