using MassTransit;
using TicketEase.Common.Events;
using TicketEase.Service.Email.Interfaces;
using TicketEase.Service.Email.Messages;

namespace TicketEase.Service.Email.Consumers
{
    public class TicketAddedEventConsumer : IConsumer<TicketAddedEvent>
    {
        private readonly IEmailSenderService _emailService;

        public TicketAddedEventConsumer(IEmailSenderService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<TicketAddedEvent> context)
        {
            var ticket = context.Message;  // Obtén los datos del Ticket desde el evento

            // Construir el correo
            var emailSubject = "Nuevo Ticket Adquirido";
            var emailBody = $"Ticket ID {ticket.TicketId} added: {ticket.UserName} purchased a ticket for function {ticket.FunctionId}\nPrecio adicional: {ticket.AdditionalPrice}.";

            // Usar un servicio de envío de correos para enviar el correo
            //await _emailService.SendEmailAsync(ticket.UserName, emailSubject, emailBody);
        }
    }
}