using MassTransit;
using TicketEase.Common.Events;
using TicketEase.Service.Functions.Models;
using TicketEase.Service.Functions.Services;

namespace TicketEase.Service.Functions.Consumers
{
    public class TicketsPurchasedEventConsumer : IConsumer<TicketsPurchasedEvent>
    {
        private readonly IFunctionsService _functionsService;

        public TicketsPurchasedEventConsumer(IFunctionsService functionsService)
        {
            _functionsService = functionsService;
        }
        public async Task Consume(ConsumeContext<TicketsPurchasedEvent> context)
        {
            var ticket = context.Message;  // Obtén los datos del Ticket desde el evento
            var functionDto = await _functionsService.GetFunctionByIdAsync(ticket.Tickets[0].FunctionId);

            if (functionDto != null)
            {
                // Restar 1 de los asientos disponibles
                functionDto.AvailableSeats -= ticket.Tickets.Count;

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
                await _functionsService.UpdateFunctionAsync(ticket.Tickets[0].FunctionId, updatedFunctionDto);
            }
        }
    }
}
