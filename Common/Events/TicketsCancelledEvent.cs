using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketEase.Common.Models;

namespace TicketEase.Common.Events
{
    public class TicketsCancelledEvent
    {
        public IEnumerable<TicketItem> TicketItems { get; }

        public TicketsCancelledEvent(IEnumerable<TicketItem> ticketItems)
        {
            TicketItems = ticketItems ?? throw new ArgumentNullException(nameof(ticketItems));
        }
    }
}
