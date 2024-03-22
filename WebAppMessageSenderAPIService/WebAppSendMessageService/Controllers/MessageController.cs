using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppSendMessageService.DataAccess.Interfaces;
using WebAppSendMessageService.DataAccess.Models;
using WebAppSendMessageService.Models;
using WLog;

namespace WebAppSendMessageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly WLogger _logger;
        private readonly IEmailService _emailService;
        private readonly ISmsService _turboSmsService;
        private readonly IMessageSqlService _messageSqlService;

        public MessageController(
            WLogger wLogger,
            IEmailService emailService,
            ISmsService turboSmsService,
            IMessageSqlService messageSqlService)
        {
            _logger = wLogger;
            _emailService = emailService;
            _turboSmsService = turboSmsService;
            _messageSqlService = messageSqlService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(SendMessage message)
        {
            try
            {
                if (string.IsNullOrEmpty(message.MessageText) || string.IsNullOrEmpty(message.Recipient) || string.IsNullOrEmpty(message.Provider))
                {
                    return BadRequest("Recipient, message, and provider are required");
                }

                switch (message.Provider)
                {
                    case "sms":
                        SmsResponceStatus responceStatus = _turboSmsService.SendMessage(new SendNotification
                        {
                            Phone = message.Recipient,
                            Body = message.MessageText
                        });

                        await _messageSqlService.AddAsync(new Domain.Entities.Message {
                            Provider = message.Provider,
                            Recipient = message.Recipient,
                            MessageText = message.MessageText,
                            Subject = message.Subject,
                            SendDate = responceStatus.RequestDateTime,
                            StatusCode = responceStatus.ResponceCode,
                            StatusDescription = responceStatus.ResponceStatus
                        });

                        return Ok(responceStatus);
                    case "email":
                        bool success = await _emailService.SendEmailAsync(message.Recipient, message.Subject, message.MessageText);

                        await _messageSqlService.AddAsync(new Domain.Entities.Message
                        {
                            Provider = message.Provider,
                            Recipient = message.Recipient,
                            MessageText = message.MessageText,
                            Subject = message.Subject,
                            SendDate = DateTimeOffset.Now,
                            StatusCode = success ? HttpStatusCode.OK : HttpStatusCode.Conflict,
                            StatusDescription = ""
                        });

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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
