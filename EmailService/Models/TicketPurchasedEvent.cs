namespace TicketEase.Service.Email.Models
{
    public class TicketPurchasedEvent
    {
        public int TicketId { get; set; }
        public string CustomerEmail { get; set; }
        public string MovieName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
