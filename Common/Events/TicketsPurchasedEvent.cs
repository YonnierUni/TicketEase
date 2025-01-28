using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Common.Events
{
    public class TicketsPurchasedEvent
    {
        public List<TicketItem> Tickets { get; set; }

        public TicketsPurchasedEvent(List<TicketItem> tickets)
        {
            Tickets = tickets;
        }
    }

    public class TicketItem
    {
        public Guid TicketId { get; }
        public Guid FunctionId { get; }
        public decimal AdditionalPrice { get; }
        public string UserName { get; }

        public TicketItem(Guid ticketId, Guid functionId, decimal additionalPrice, string userName)
        {
            TicketId = ticketId;
            FunctionId = functionId;
            AdditionalPrice = additionalPrice;
            UserName = userName;
        }
    }

}
