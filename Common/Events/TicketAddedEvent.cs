using TicketEase.Common.Models;

namespace TicketEase.Common.Events
{
    public class TicketAddedEvent
    {
        public Guid TicketId { get; set; }
        public Guid FunctionId { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string UserName { get; set; }

        public TicketAddedEvent(Guid ticketId, Guid functionId, decimal additionalPrice, string userName)
        {
            TicketId = ticketId;
            FunctionId = functionId;
            AdditionalPrice = additionalPrice;
            UserName = userName;
        }
    }
    public class TicketUpdatedEvent
    {
        public Guid TicketId { get; }
        public Guid FunctionId { get; }
        public decimal AdditionalPrice { get; }
        public string UserName { get; }

        public TicketUpdatedEvent(Guid ticketId, Guid functionId, decimal additionalPrice, string userName)
        {
            TicketId = ticketId;
            FunctionId = functionId;
            AdditionalPrice = additionalPrice;
            UserName = userName;
        }
    }


    public class TicketDeletedEvent
    {
        public Guid TicketId { get; }

        // Constructor que recibe el TicketId
        public TicketDeletedEvent(Guid ticketId)
        {
            TicketId = ticketId;
        }
    }
}

