using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Common.Models
{
    public class TicketDTO
    {
        public Guid TicketId { get; set; }
        public Guid FunctionId { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string UserName { get; set; }

        public TicketDTO(Guid ticketId, Guid functionId, decimal additionalPrice, string userName)
        {
            TicketId = ticketId;
            FunctionId = functionId;
            AdditionalPrice = additionalPrice;
            UserName = userName;
        }
    }
}
