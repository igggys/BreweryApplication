using Microsoft.AspNetCore.Mvc;
using WebAppSendMessageService.Dto;
using WebAppSendMessageService.Interfaces;
using WebAppSendMessageService.Models;
using WLog;

namespace WebAppSendMessageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly WLogger _logger;
        private readonly IMessageProvider _emailMessageProvider;
        private readonly ISmsService _turboSmsService;

        public MessageController(
            WLogger wLogger, 
            IMessageProvider emailMessageProvider,
            ISmsService turboSmsService)
        {
            _logger = wLogger;
            _emailMessageProvider = emailMessageProvider;
            _turboSmsService = turboSmsService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromForm] string provider, [FromForm] string recipient, [FromForm] string message, [FromForm] string subject = null)
        {
            try
            {
                if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(recipient) || string.IsNullOrEmpty(provider))
                {
                    return BadRequest("Recipient, message, and provider are required");
                }

                // Providers: email, sms, viber...

                switch (provider)
                {
                    case "sms":
                        SmsResponceStatus responceStatus = _turboSmsService.SendMessage(new SendNotificationDto
                        {
                            Phone = recipient,
                            Body = message
                        });
                        return Ok(responceStatus);
                    case "email":
                        bool success = await _emailMessageProvider.SendMessageAsync(recipient, subject, message);
                        if (success)
                        {
                            return Ok("Email sent successfully");
                        }
                        else
                        {
                            return BadRequest("Failed to send email");
                        }
                    case "viber":
                        // Implement Viber message sending logic here
                        return Ok("Viber message sent successfully");
                    case "telegram":
                        // Implement Telegram message sending logic here
                        return Ok("Telegram message sent successfully");
                    case "whatsapp":
                        // Implement WhatsApp message sending logic here
                        return Ok("WhatsApp message sent successfully");
                    default:
                        return BadRequest("Invalid provider");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }            
        }
    }
}
