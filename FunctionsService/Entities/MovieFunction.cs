namespace TicketEase.Service.Functions.Entities
{
    public class MovieFunction
    {
        public Guid MovieFunctionId { get; set; }
        public Guid MovieId { get; set; }
        public string Room { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
    }
}
