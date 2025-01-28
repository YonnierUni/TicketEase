using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Common.Commands
{
    public class CheckSeatsAvailabilityCommand
    {
        public Guid FunctionId { get; set; }
        public int NumberOfSeats { get; set; }

        public CheckSeatsAvailabilityCommand(Guid functionId, int numberOfSeats)
        {
            FunctionId = functionId;
            NumberOfSeats = numberOfSeats;
        }
    }
}
