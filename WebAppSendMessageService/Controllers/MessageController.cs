using Microsoft.AspNetCore.Mvc;
using WebAppSendMessageService.BLL;
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
        private readonly SmsMessageProvider _smsMessageProvider;
        private readonly IMessageProvider _emailMessageProvider;
        private readonly ISmsService _turboSmsService;

        public MessageController(
            WLogger wLogger, 
            SmsMessageProvider smsMessageProvider,
            IMessageProvider emailMessageProvider,
            ISmsService turboSmsService)
        {
            _logger = wLogger;
            _smsMessageProvider = smsMessageProvider;
            _emailMessageProvider = emailMessageProvider;
            _turboSmsService = turboSmsService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromForm] string provider, [FromForm] string recipient, [FromForm] string message, [FromForm] string subject = null)
        {
            //_logger.AddToLod(provider);
            //_logger.AddToLod(recipient);
            //_logger.AddToLod(message);
            //_logger.AddToLod(subject);

            // Providers: email, sms, viber

            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(recipient))
            {
                bool success = false;

                switch (provider)
                {
                    case "sms":
                        // If subject is not specified, use SmsMessageProvider to send SMS
                        SmsResponceStatus responceStatus = _turboSmsService.SendMessage(new SendNotificationDto
                        {
                            //Phone = user.PhoneNumber.GetFormattedPhone(),
                            Phone = recipient,
                            Body = message
                        });
                        return Ok(responceStatus);
                    case "email":
                        // If a subject is specified, use the EmailMessageProviderOld to send the EMAIL
                        success = await _emailMessageProvider.SendMessageAsync(recipient, subject, message);
                        break;
                    case "viber":
                        break;
                    case "telegram":
                        break;
                    case "whatsapp":
                        break;
                    default:
                        return BadRequest("Invalid message type");
                }

                if (success)
                {
                    return Ok("Message sent successfully");
                }
                else
                {
                    return BadRequest("Failed to send message");
                }                
            }
            else
            {
                return BadRequest("Recipient and message are required");
            }
        }
    }
}
