﻿using System.Text.Json.Serialization;

namespace TicketEase.Service.TicketPurchase.Entities
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public Guid FunctionId { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public Function Function { get; set; }
    }
}