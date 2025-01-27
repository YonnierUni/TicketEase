namespace TicketEase.Service.Email.Models
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public Boolean EnableSsl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
