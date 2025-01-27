namespace TicketEase.Service.Email.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
