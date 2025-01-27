using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Models;
using TicketEase.Service.TicketPurchase.Services;

namespace TicketEase.Service.TicketPurchase.Controllers
{
    [Route("api/functions")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly IFunctionService _functionService;

        public FunctionController(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        // Obtener todas las funciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FunctionDto>>> GetAllFunctions()
        {
            var functions = await _functionService.GetAllFunctionsAsync();
            if (functions == null || !functions.Any())
                return NotFound(new { message = "No functions found." });

            return Ok(functions);
        }

        // Obtener una función por ID
        [HttpGet("{id:Guid}", Name = "GetFunctionById")]
        public async Task<ActionResult<FunctionDto>> GetFunctionById(Guid id)
        {
            try
            {
                var function = await _functionService.GetFunctionByIdAsync(id);
                return Ok(function);
            }
            catch (ArgumentException)
            {
                return NotFound(new { message = "Function not found." });
            }
        }

        // Crear una nueva función
        [HttpPost]
        public async Task<ActionResult> CreateFunction([FromBody] FunctionForCreationDto functionForCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var function = await _functionService.AddFunctionAsync(functionForCreation);
            return CreatedAtRoute("GetFunctionById", new { id = function.FunctionId }, function);
        }

        // Actualizar una función
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateFunction(Guid id, [FromBody] FunctionForUpdateDto functionForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _functionService.UpdateFunctionAsync(functionForUpdate);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound(new { message = "Function to update not found." });
            }
        }

        // Eliminar una función
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteFunction(Guid id)
        {
            try
            {
                var success = await _functionService.DeleteFunctionAsync(id);
                if (success)
                    return NoContent();

                return NotFound(new { message = "Function not found for deletion." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // Obtener funciones próximas a partir de una fecha
        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<FunctionDto>>> GetUpcomingFunctions([FromQuery] DateTime fromDate)
        {
            var functions = await _functionService.GetUpcomingFunctionsAsync(fromDate);
            if (functions == null || !functions.Any())
                return NotFound(new { message = "No upcoming functions found." });

            return Ok(functions);
        }
    }
}