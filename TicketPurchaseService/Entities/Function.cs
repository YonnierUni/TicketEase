namespace TicketEase.Service.TicketPurchase.Entities
{
    public class Function
    {
        public Guid FunctionId { get; set; }
        public Guid MovieId { get; set; }
        public decimal Price { get; set; }
        public DateTime FunctionDate{ get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
