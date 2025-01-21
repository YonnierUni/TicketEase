namespace TicketEase.Service.TicketPurchase.Models
{
    public class FunctionDto
    {
        public Guid FunctionId { get; set; }
        public Guid MovieId { get; set; }
        public decimal Price { get; set; }
        public DateTime FunctionDate { get; set; }
    }
    public class FunctionForCreationDto {
        public Guid MovieId { get; set; }
        public decimal Price { get; set; }
        public DateTime FunctionDate { get; set; }
    }
    public class FunctionForUpdateDto
    {
        public decimal Price { get; set; }
        public DateTime FunctionDate { get; set; }
    }
}
