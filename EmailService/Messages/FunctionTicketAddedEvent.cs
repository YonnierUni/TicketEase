namespace TicketEase.Service.Email.Messages
{
    public class FunctionTicketAddedEvent
    {
        public Guid FunctionId { get; set; }
        public Guid MovieId { get; set; }
        public decimal Price { get; set; }
        public DateTime FunctionDate { get; set; }
    }
}
