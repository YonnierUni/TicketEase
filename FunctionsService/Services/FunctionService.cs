using TicketEase.Service.Functions.Repositories;
using TicketEase.Service.Functions.Entities;
using AutoMapper;
using MassTransit;
using TicketEase.Service.Functions.Models;
using TicketEase.Service.Functions.Events;

namespace TicketEase.Service.Functions.Services
{
    public class FunctionService : IFunctionsService
    {
        private readonly IFunctionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public FunctionService(IFunctionRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<MovieFunctionDto>> GetAllFunctionsAsync()
        {
            var functions = await _repository.GetAllFunctionsAsync();
            return _mapper.Map<IEnumerable<MovieFunctionDto>>(functions);
        }

        public async Task<MovieFunctionDto> GetFunctionByIdAsync(Guid id)
        {
            var function = await _repository.GetFunctionByIdAsync(id);
            if (function == null)
            {
                throw new KeyNotFoundException($"Función con ID {id} no encontrada.");
            }

            return _mapper.Map<MovieFunctionDto>(function);
        }
        public async Task<IEnumerable<MovieFunctionDto>> GetFunctionsByMovieIdAsync(Guid movieId)
        {
            // Obtén las funciones relacionadas con el movieId del repositorio
            var functions = await _repository.GetFunctionsByMovieIdAsync(movieId);

            // Mapea las funciones a la clase DTO y devuélvelas
            return _mapper.Map<IEnumerable<MovieFunctionDto>>(functions);
        }
        public async Task<IEnumerable<MovieFunctionDto>> GetFunctionsByIdsAsync(IEnumerable<Guid> ids)
        {
            // Consulta las funciones por sus IDs desde el repositorio
            var functions = await _repository.GetFunctionsByIdsAsync(ids);

            // Mapea las funciones a la clase DTO y las retorna
            return _mapper.Map<IEnumerable<MovieFunctionDto>>(functions);
        }
        public async Task AddFunctionAsync(MovieFunctionDto functionDto)
        {
            var function = _mapper.Map<MovieFunction>(functionDto);
            await _repository.AddFunctionAsync(function);

            await _publishEndpoint.Publish(new FunctionAddedEvent
            {
                MovieFunctionId = function.MovieFunctionId,
                MovieId = function.MovieId,
                Room = function.Room,
                StartTime = function.StartTime,
                EndTime = function.EndTime,
                TotalSeats = function.TotalSeats,
                AvailableSeats = function.AvailableSeats
            });
        }

        public async Task UpdateFunctionAsync(Guid id, MovieFunctionDto functionDto)
        {
            var function = await _repository.GetFunctionByIdAsync(id);
            if (function == null)
            {
                throw new KeyNotFoundException($"Función con ID {id} no encontrada.");
            }

            _mapper.Map(functionDto, function);
            await _repository.UpdateFunctionAsync(function);

            await _publishEndpoint.Publish(new FunctionUpdatedEvent
            {
                MovieFunctionId = function.MovieFunctionId,
                MovieId = function.MovieId,
                Room = function.Room,
                StartTime = function.StartTime,
                EndTime = function.EndTime,
                TotalSeats = function.TotalSeats,
                AvailableSeats = function.AvailableSeats
            });
        }

        public async Task DeleteFunctionAsync(Guid id)
        {
            var function = await _repository.GetFunctionByIdAsync(id);
            if (function == null)
            {
                throw new KeyNotFoundException($"Función con ID {id} no encontrada.");
            }

            await _repository.DeleteFunctionAsync(id);

            await _publishEndpoint.Publish(new FunctionDeletedEvent
            {
                MovieFunctionId = id
            });
        }
    }
}
