using MassTransit;
using TicketEase.Common.Commands;
using TicketEase.Common.Events;
using TicketEase.Service.Functions.Services;

namespace TicketEase.Service.Functions.Consumers
{
    public class CheckSeatsAvailabilityConsumer : IConsumer<CheckSeatsAvailabilityCommand>
    {
        private readonly IFunctionsService _functionsService;

        public CheckSeatsAvailabilityConsumer(IFunctionsService functionsService)
        {
            _functionsService = functionsService;
        }

        public async Task Consume(ConsumeContext<CheckSeatsAvailabilityCommand> context)
        {
            var checkSeatsAvailability = context.Message;
            var function = await _functionsService.GetFunctionByIdAsync(checkSeatsAvailability.FunctionId);

            if (function == null)
            {
                // Aquí deberías manejar el error, por ejemplo, enviando una respuesta de error
                throw new ArgumentException("Function does not exist.");
            }

            // Lógica de verificación de disponibilidad de asientos
            bool areSeatsAvailable = function.AvailableSeats >= checkSeatsAvailability.NumberOfSeats;

            // Crear y enviar la respuesta
            var response = new SeatsAvailabilityResponse
            {
                FunctionId = checkSeatsAvailability.FunctionId,
                AreSeatsAvailable = areSeatsAvailable
            };

            // Publicar la respuesta de disponibilidad de asientos
            await context.RespondAsync(response);
        }
    }
}