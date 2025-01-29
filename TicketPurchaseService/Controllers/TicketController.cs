using Microsoft.AspNetCore.Mvc;
using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Models;
using TicketEase.Service.TicketPurchase.Repositories;
using AutoMapper;
using TicketEase.Service.TicketPurchase.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TicketEase.Service.TicketPurchase.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();

            if (tickets == null || !tickets.Any())
                return NoContent();

            var ticketDtos = tickets.Select(ticket => new TicketDto
            {
                TicketId = ticket.TicketId,
                FunctionId = ticket.FunctionId,
                AdditionalPrice = ticket.AdditionalPrice,
                UserName = ticket.UserName
            }).ToList();

            return Ok(ticketDtos);
        }

        [HttpGet("{id:Guid}", Name = "GetTicketById")]
        public async Task<ActionResult<TicketDto>> GetTicketById(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound(new { message = $"Ticket with ID {id} not found." });

            var ticketDto = new TicketDto
            {
                TicketId = ticket.TicketId,
                FunctionId = ticket.FunctionId,
                AdditionalPrice = ticket.AdditionalPrice,
                UserName = ticket.UserName
            };

            return Ok(ticketDto);
        }

        [HttpPost("addTicket")]
        public async Task<ActionResult> AddTicket([FromBody] TicketForCreationDto ticketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid model data.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var ticket = new Ticket
            {
                FunctionId = ticketDto.FunctionId,
                AdditionalPrice = ticketDto.AdditionalPrice,
                UserName = ticketDto.UserName
            };

            var createdTicket = await _ticketService.AddTicketAsync(ticket);

            var createdTicketDto = new TicketDto
            {
                TicketId = createdTicket.TicketId,
                FunctionId = createdTicket.FunctionId,
                AdditionalPrice = createdTicket.AdditionalPrice,
                UserName = createdTicket.UserName
            };

            return CreatedAtRoute("GetTicketById", new { id = createdTicket.TicketId }, createdTicketDto);
        }
        [HttpPost("addMultipleTickets")]
        public async Task<ActionResult> AddTickets([FromBody] List<TicketForCreationDto> ticketDtos)
        {
            // Validar el modelo
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid model data.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            // Llamar al servicio para crear múltiples tickets de una vez
            await _ticketService.AddMultipleTicketsAsync(ticketDtos);

            return Ok(new { message = "Tickets created successfully." });

        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateTicket(Guid id, [FromBody] TicketForUpdateDto ticketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid model data.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var existingTicket = await _ticketService.GetTicketByIdAsync(id);
            if (existingTicket == null)
                return NotFound(new { message = $"Ticket with ID {id} not found." });

            existingTicket.UserName = ticketDto.UserName;

            await _ticketService.UpdateTicketAsync(existingTicket);

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteTicket(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound(new { message = $"Ticket with ID {id} not found." });

            await _ticketService.DeleteTicketAsync(id);

            return NoContent();
        }

        [HttpPost("cancelTickets")]
        [Authorize(Roles = "client")]
        public async Task<ActionResult> CancelTickets([FromBody] CancelTicketsDto cancelTicketsDto)
        {
            if (cancelTicketsDto == null || cancelTicketsDto.TicketIds == null || !cancelTicketsDto.TicketIds.Any())
                return BadRequest(new { message = "No valid ticket information provided." });
            
            var userRole = User?.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null || (userRole != "client"))
            {
                return Unauthorized(new { message = "You do not have permission to cancel tickets." });
            }

            // Llamar al servicio para eliminar los tickets
            await _ticketService.DeleteMultipleTicketsAsync(cancelTicketsDto.TicketIds);

            return Ok(new { message = "Tickets canceled successfully." });
        }

        [HttpGet("function/{functionId:Guid}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByFunctionId(Guid functionId)
        {
            var tickets = await _ticketService.GetTicketsByFunctionAsync(functionId);

            if (!tickets.Any())
                return NotFound(new { message = $"No tickets found for function with ID {functionId}." });

            var ticketDtos = tickets.Select(ticket => new TicketDto
            {
                TicketId = ticket.TicketId,
                FunctionId = ticket.FunctionId,
                AdditionalPrice = ticket.AdditionalPrice,
                UserName = ticket.UserName
            }).ToList();

            return Ok(ticketDtos);
        }

        [HttpGet("user/{userName}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByUserName(string userName)
        {
            var tickets = await _ticketService.GetTicketsByUserAsync(userName);

            if (!tickets.Any())
                return NotFound(new { message = $"No tickets found for user {userName}." });

            var ticketDtos = tickets.Select(ticket => new TicketDto
            {
                TicketId = ticket.TicketId,
                FunctionId = ticket.FunctionId,
                AdditionalPrice = ticket.AdditionalPrice,
                UserName = ticket.UserName
            }).ToList();

            return Ok(ticketDtos);
        }
    }
}