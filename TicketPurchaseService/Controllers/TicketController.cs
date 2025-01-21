using Microsoft.AspNetCore.Mvc;
using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Models;
using TicketEase.Service.TicketPurchase.Repositories;
using AutoMapper;

namespace TicketEase.Service.TicketPurchase.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketController(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TicketDto>>(tickets));
        }

        [HttpGet("{id:Guid}", Name = "GetTicketById")]
        public async Task<ActionResult<TicketDto>> GetTicketById(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return Ok(_mapper.Map<TicketDto>(ticket));
        }

        [HttpPost]
        public async Task<ActionResult> CreateTicket([FromBody] TicketForCreationDto ticketForCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticketEntity = _mapper.Map<Ticket>(ticketForCreation);
            await _ticketRepository.AddAsync(ticketEntity);

            var ticketToReturn = _mapper.Map<TicketDto>(ticketEntity);

            return CreatedAtRoute("GetTicketById", new { id = ticketToReturn.TicketId }, ticketToReturn);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateTicket(Guid id, [FromBody] TicketForUpdateDto ticketForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticketEntity = await _ticketRepository.GetByIdAsync(id);
            if (ticketEntity == null)
                return NotFound();

            _mapper.Map(ticketForUpdate, ticketEntity);
            await _ticketRepository.UpdateAsync(ticketEntity);

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteTicket(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                return NotFound();

            await _ticketRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("function/{functionId:Guid}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByFunctionId(Guid functionId)
        {
            var tickets = await _ticketRepository.GetTicketsByFunctionIdAsync(functionId);

            if (tickets == null || !tickets.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<TicketDto>>(tickets));
        }

        [HttpGet("user/{userName}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByUserNameAsync(string userName)
        {
            var tickets = await _ticketRepository.GetTicketsByUserNameAsync(userName);

            if (tickets == null || !tickets.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<TicketDto>>(tickets));
        }
    }
}