namespace TicketEase.Service.Email.Messages
{
    public class TicketAddedEvent
    {
        public Guid TicketId { get; }
        public Guid FunctionId { get; }
        public decimal AdditionalPrice { get; }
        public string UserName { get; }
        public FunctionTicketAddedEvent Function { get; set; }

    }
}
