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
            string subject = "🎟️ Ticket Purchase Confirmation";

            // Construir el cuerpo del correo con formato mejorado
            var emailBodyBuilder = new StringBuilder();
            emailBodyBuilder.AppendLine("<html><body>");
            emailBodyBuilder.AppendLine("<h2 style='color: #2D89EF;'>Thank You for Your Purchase! 🎬</h2>");
            emailBodyBuilder.AppendLine("<p>Dear customer,</p>");
            emailBodyBuilder.AppendLine("<p>You have successfully purchased the following tickets:</p>");

            emailBodyBuilder.AppendLine("<ul>");
            foreach (var ticket in @event.Tickets)
            {
                emailBodyBuilder.AppendLine("<li>");
                emailBodyBuilder.AppendLine($"<strong>🎫 Ticket ID:</strong> {ticket.TicketId}<br>");
                emailBodyBuilder.AppendLine($"<strong>🎥 Movie:</strong> {ticket.FunctionId}<br>");
                emailBodyBuilder.AppendLine($"<strong>👤 User Name:</strong> {ticket.UserName}<br>");
                emailBodyBuilder.AppendLine($"<strong>💲 Additional Price:</strong> {ticket.AdditionalPrice:C}<br>");
                emailBodyBuilder.AppendLine("</li><br>");
            }
            emailBodyBuilder.AppendLine("</ul>");

            emailBodyBuilder.AppendLine("<p>We hope you enjoy the show!</p>");
            emailBodyBuilder.AppendLine("<p>Best regards,<br><strong>The TicketEase Team</strong></p>");
            emailBodyBuilder.AppendLine("</body></html>");

            // Convertir el contenido a string
            string body = emailBodyBuilder.ToString();

            // Enviar el correo electrónico
            await _emailSenderService.SendEmailAsync(@event.Tickets.FirstOrDefault().UserName, subject, body); // Se asume que UserName es el correo
        }
    }
}

