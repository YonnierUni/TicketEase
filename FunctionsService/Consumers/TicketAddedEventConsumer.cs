using MassTransit;
using TicketEase.Common.Events;
using TicketEase.Service.Functions.Models;
using TicketEase.Service.Functions.Services;

namespace TicketEase.Service.Functions.Consumers
{
    public class TicketAddedEventConsumer : IConsumer<TicketAddedEvent>
    {
        private readonly IFunctionsService _functionsService;

        public TicketAddedEventConsumer(IFunctionsService functionsService)
        {
            _functionsService = functionsService;
        }
        public async Task Consume(ConsumeContext<TicketAddedEvent> context)
        {
            var ticket = context.Message;  // Obtén los datos del Ticket desde el evento

            var functionDto = await _functionsService.GetFunctionByIdAsync(ticket.FunctionId);

            if (functionDto != null)
            {
                // Restar 1 de los asientos disponibles
                functionDto.AvailableSeats -= 1;

                // Crear un nuevo MovieFunctionDto con los detalles actualizados
                var updatedFunctionDto = new MovieFunctionDto
                {
                    MovieFunctionId = functionDto.MovieFunctionId,
                    MovieId = functionDto.MovieId,
                    Room = functionDto.Room,
                    StartTime = functionDto.StartTime,
                    EndTime = functionDto.EndTime,
                    TotalSeats = functionDto.TotalSeats,
                    AvailableSeats = functionDto.AvailableSeats // Asientos actualizados
                };

                // Llamar al servicio de actualización pasando el nuevo DTO con los asientos actualizados
                await _functionsService.UpdateFunctionAsync(ticket.FunctionId, updatedFunctionDto);
            }
        }
    }
}