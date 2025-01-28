using MassTransit;
using TicketEase.Common.Events;
using TicketEase.Service.Functions.Models;
using TicketEase.Service.Functions.Services;

namespace TicketEase.Service.Functions.Consumers
{
    public class TicketsCancelledEventConsumer : IConsumer<TicketsCancelledEvent>
    {
        private readonly IFunctionsService _functionsService;

        public TicketsCancelledEventConsumer(IFunctionsService functionsService)
        {
            _functionsService = functionsService;
        }
        public async Task Consume(ConsumeContext<TicketsCancelledEvent> context)
        {
            var ticketsCancelledEvent = context.Message;  // Obtén los datos del evento TicketsCancelledEvent
            var functionDto = await _functionsService.GetFunctionByIdAsync(ticketsCancelledEvent.TicketItems.FirstOrDefault().FunctionId);

            if (functionDto != null)
            {
                // Restar los asientos disponibles de la función con la cantidad de tickets cancelados
                functionDto.AvailableSeats += ticketsCancelledEvent.TicketItems.Count();  // Aumenta la disponibilidad por cada ticket cancelado

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
                await _functionsService.UpdateFunctionAsync(ticketsCancelledEvent.TicketItems.FirstOrDefault().FunctionId, updatedFunctionDto);
            }

        }
    }
}
