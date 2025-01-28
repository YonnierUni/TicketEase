namespace TicketEase.Service.TicketPurchase.Models
{
    public class TicketDto
    {
        public Guid TicketId { get; set; }
        public Guid FunctionId { get; set; }
        public decimal AdditionalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserName { get; set; }
    }
    public class TicketForCreationDto {
        public Guid FunctionId { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string UserName { get; set; }
    }
    public class TicketForUpdateDto {
        public string UserName { get; set; }
    }
    public class CancelTicketsDto
    {
        public List<Guid> TicketIds { get; set; } = new List<Guid>();
    }

}
