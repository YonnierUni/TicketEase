using Microsoft.AspNetCore.Mvc;
using TicketEase.Service.Email.Interfaces;

namespace TicketEase.Service.Email.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;

        public NotificationsController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            await _emailSenderService.SendEmailAsync(to, subject, body);
            return Ok("Email sent successfully");
        }
    }
}
