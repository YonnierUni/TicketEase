using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Models;
using TicketEase.Service.TicketPurchase.Repositories;

namespace TicketEase.Service.TicketPurchase.Controllers
{
    [Route("api/functions")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly IFunctionRepository _functionRepository;
        private readonly IMapper _mapper;

        public FunctionController(IFunctionRepository functionRepository, IMapper mapper)
        {
            _functionRepository = functionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FunctionDto>>> GetAllFunctions()
        {
            var functions = await _functionRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<FunctionDto>>(functions));
        }

        [HttpGet("{id:Guid}", Name = "GetFunctionById")]
        public async Task<ActionResult<FunctionDto>> GetFunctionById(Guid id)
        {
            var function = await _functionRepository.GetByIdAsync(id);

            if (function == null)
                return NotFound();

            return Ok(_mapper.Map<FunctionDto>(function));
        }

        [HttpPost]
        public async Task<ActionResult> CreateFunction([FromBody] FunctionForCreationDto functionForCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var functionEntity = _mapper.Map<Function>(functionForCreation);
            await _functionRepository.AddAsync(functionEntity);

            var functionToReturn = _mapper.Map<FunctionDto>(functionEntity);

            return CreatedAtRoute("GetFunctionById", new { id = functionToReturn.FunctionId }, functionToReturn);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateFunction(Guid id, [FromBody] FunctionForUpdateDto functionForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var functionEntity = await _functionRepository.GetByIdAsync(id);
            if (functionEntity == null)
                return NotFound();

            _mapper.Map(functionForUpdate, functionEntity);
            await _functionRepository.UpdateAsync(functionEntity);

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteFunction(Guid id)
        {
            var function = await _functionRepository.GetByIdAsync(id);
            if (function == null)
                return NotFound();

            await _functionRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<FunctionDto>>> GetUpcomingFunctions([FromQuery] DateTime fromDate)
        {
            var functions = await _functionRepository.GetUpcomingFunctionsAsync(fromDate);

            if (functions == null || !functions.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<FunctionDto>>(functions));
        }
    }
}