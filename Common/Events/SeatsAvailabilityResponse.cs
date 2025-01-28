using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Common.Events
{
    public class SeatsAvailabilityResponse
    {
        public Guid FunctionId { get; set; }
        public bool AreSeatsAvailable { get; set; }
    }
}
