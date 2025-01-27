using MassTransit;
using TicketEase.Service.Email.Interfaces;
using TicketEase.Service.Email.Models;

namespace TicketEase.Service.Email.Consumers
{
    public class TicketPurchasedEventConsumer : IConsumer<TicketPurchasedEvent>
    {
        private readonly IEmailSenderService _emailSenderService;

        public TicketPurchasedEventConsumer(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public async Task Consume(ConsumeContext<TicketPurchasedEvent> context)
        {
            var @event = context.Message;

            string subject = "Ticket Purchase Confirmation";
            string body = $"Dear customer, you have purchased a ticket for the movie: {@event.MovieName}. " +
                $"Amount paid: {@event.Amount}. " +
                $"Date of purchase: {@event.PurchaseDate}.";

            await _emailSenderService.SendEmailAsync(@event.CustomerEmail, subject, body);
        }
    }
}
