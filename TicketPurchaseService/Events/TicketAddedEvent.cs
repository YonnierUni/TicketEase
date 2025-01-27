using TicketEase.Service.TicketPurchase.Entities;

namespace TicketEase.Service.TicketPurchase.Events
{
    public class TicketAddedEvent
    {
        public Ticket Ticket { get; }

        public TicketAddedEvent(Ticket ticket)
        {
            Ticket = ticket;
        }
    }

    public class TicketUpdatedEvent
    {
        public Ticket Ticket { get; }

        public TicketUpdatedEvent(Ticket ticket)
        {
            Ticket = ticket;
        }
    }

    public class TicketDeletedEvent
    {
        public Guid TicketId { get; }

        public TicketDeletedEvent(Guid ticketId)
        {
            TicketId = ticketId;
        }
    }
}
