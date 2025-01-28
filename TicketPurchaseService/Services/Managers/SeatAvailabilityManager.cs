using MassTransit;
using TicketEase.Common.Commands;
using TicketEase.Common.Events;

namespace TicketEase.Service.TicketPurchase.Services.Managers
{
    public class SeatAvailabilityManager
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<CheckSeatsAvailabilityCommand> _requestClient;

        public SeatAvailabilityManager(IPublishEndpoint publishEndpoint, IRequestClient<CheckSeatsAvailabilityCommand> requestClient)
        {
            _publishEndpoint = publishEndpoint;
            _requestClient = requestClient;
        }

        public async Task<bool> CheckSeatsAvailabilityAsync(Guid functionId, int seatsRequested)
        {
            // Crear el comando de verificación de disponibilidad
            var checkSeatsCommand = new CheckSeatsAvailabilityCommand(functionId, seatsRequested);

            // Enviar el comando y esperar la respuesta del consumidor
            var response = await _requestClient.GetResponse<SeatsAvailabilityResponse>(checkSeatsCommand);

            // Retornar si los asientos están disponibles (en base a la respuesta)
            return response.Message.AreSeatsAvailable;
        }
    }

}
