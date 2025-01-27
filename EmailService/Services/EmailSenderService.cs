using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using TicketEase.Service.Email.Interfaces;
using TicketEase.Service.Email.Models;

namespace TicketEase.Service.Email.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IOptions<SmtpSettings> smtpSettings, ILogger<EmailSenderService> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using var smtpClient = new SmtpClient(_smtpSettings.Host)
                {
                    Port = _smtpSettings.Port,
                    Credentials = new NetworkCredential(_smtpSettings.User, _smtpSettings.Password),
                    EnableSsl = _smtpSettings.EnableSsl,
                    Timeout = 15000
                };
                _logger.LogInformation($"Connecting to SMTP server at {_smtpSettings.Host}:{_smtpSettings.Port}");
                Console.WriteLine($"Connecting to SMTP server at {_smtpSettings.Host}:{_smtpSettings.Port}");
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.User),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await smtpClient.SendMailAsync(mailMessage);

                _logger.LogInformation($"Email sent to {to} with subject '{subject}'");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email to {to}: {ex.Message}");
                throw;
            }
        }
    }
}