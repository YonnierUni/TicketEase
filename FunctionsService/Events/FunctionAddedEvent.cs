namespace TicketEase.Service.Functions.Events
{
    public class FunctionAddedEvent
    {
        public Guid MovieFunctionId { get; set; }
        public Guid MovieId { get; set; }
        public string Room { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
    }
    public class FunctionUpdatedEvent
    {
        public Guid MovieFunctionId { get; set; }
        public Guid MovieId { get; set; }
        public string Room { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
    }
    public class FunctionDeletedEvent
    {
        public Guid MovieFunctionId { get; set; }
    }
}
