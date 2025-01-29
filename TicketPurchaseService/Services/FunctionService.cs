using TicketEase.Service.TicketPurchase.Repositories;
using TicketEase.Service.TicketPurchase.Entities;
using RabbitMQ.Client;
using System.Text;
using MassTransit;
using AutoMapper;
using TicketEase.Service.TicketPurchase.Models;

namespace TicketEase.Service.TicketPurchase.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public FunctionService(IFunctionRepository functionRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _functionRepository = functionRepository;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FunctionDto>> GetAllFunctionsAsync()
        {
            var functions = await _functionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FunctionDto>>(functions);
        }

        public async Task<FunctionDto> GetFunctionByIdAsync(Guid functionId)
        {
            var function = await _functionRepository.GetByIdAsync(functionId);
            if (function == null)
            {
                throw new ArgumentException($"Función con ID {functionId} no encontrada.");
            }

            return _mapper.Map<FunctionDto>(function);
        }

        public async Task<FunctionDto> AddFunctionAsync(FunctionForCreationDto functionForCreationDto)
        {
            if (functionForCreationDto.Price <= 0)
            {
                throw new ArgumentException("El precio de la función debe ser mayor a cero.");
            }

            if (functionForCreationDto.FunctionDate <= DateTime.Now)
            {
                throw new ArgumentException("La fecha de la función debe ser en el futuro.");
            }

            var function = _mapper.Map<Function>(functionForCreationDto);
            await _functionRepository.AddAsync(function);

            //var eventMessage = new { EventType = "Ticket-created", FunctionId = function.FunctionId };
            //await _publishEndpoint.Publish(eventMessage);

            return _mapper.Map<FunctionDto>(function);
        }

        public async Task<FunctionDto> UpdateFunctionAsync(FunctionForUpdateDto forUpdateDto)
        {
            var existingFunction = await _functionRepository.GetByIdAsync(forUpdateDto.FunctionId);
            if (existingFunction == null)
            {
                throw new ArgumentException($"No existe la función con ID {forUpdateDto.FunctionId}.");
            }

            if (forUpdateDto.Price <= 0)
            {
                throw new ArgumentException("El precio de la función debe ser mayor a cero.");
            }

            var function = _mapper.Map<Function>(forUpdateDto);
            await _functionRepository.UpdateAsync(function);

            var eventMessage = new { EventType = "Ticket-updated", FunctionId = function.FunctionId };
            await _publishEndpoint.Publish(eventMessage);

            return _mapper.Map<FunctionDto>(function);
        }

        public async Task<bool> DeleteFunctionAsync(Guid functionId)
        {
            var function = await _functionRepository.GetByIdAsync(functionId);
            if (function == null)
            {
                throw new ArgumentException($"Función con ID {functionId} no encontrada.");
            }

            await _functionRepository.DeleteAsync(functionId);

            var eventMessage = new { EventType = "Ticket-deleted", FunctionId = functionId };
            await _publishEndpoint.Publish(eventMessage);

            return true;
        }

        public async Task<IEnumerable<FunctionDto>> GetUpcomingFunctionsAsync(DateTime fromDate)
        {
            var functions = await _functionRepository.GetUpcomingFunctionsAsync(fromDate);
            return _mapper.Map<IEnumerable<FunctionDto>>(functions);
        }
    }
}
