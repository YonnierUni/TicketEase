using MassTransit;
using System.Text;
using TicketEase.Common.Events;
using TicketEase.Service.Email.Interfaces;
using TicketEase.Service.Email.Models;

namespace TicketEase.Service.Email.Consumers
{
    public class TicketPurchasedEventConsumer : IConsumer<TicketsPurchasedEvent>
    {
        private readonly IEmailSenderService _emailSenderService;

        public TicketPurchasedEventConsumer(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public async Task Consume(ConsumeContext<TicketsPurchasedEvent> context)
        {
            var @event = context.Message;  // Obtén los datos del Ticket desde el evento

            // Establecer el asunto del correo
            string subject = "Ticket Purchase Confirmation";

            // Crear el cuerpo del correo
            var emailBodyBuilder = new StringBuilder();
            emailBodyBuilder.AppendLine("Dear customer,");
            emailBodyBuilder.AppendLine("You have purchased the following tickets:");

            // Agregar detalles de cada ticket al cuerpo del correo
            foreach (var ticket in @event.Tickets)
            {
                emailBodyBuilder.AppendLine($"Ticket ID: {ticket.TicketId}");
                emailBodyBuilder.AppendLine($"Movie: {ticket.FunctionId}");
                emailBodyBuilder.AppendLine($"User Name: {ticket.UserName}");
                emailBodyBuilder.AppendLine($"Additional Price: {ticket.AdditionalPrice:C}");
                emailBodyBuilder.AppendLine();
            }

            emailBodyBuilder.AppendLine("Thank you for your purchase!");

            // Enviar el correo electrónico
            string body = emailBodyBuilder.ToString();
            await _emailSenderService.SendEmailAsync(@event.Tickets.FirstOrDefault().UserName, subject, body); // Se asume que UserName es el correo
        }
    }
}

