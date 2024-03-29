using WebAppSendMessageService.Dto;
using WebAppSendMessageService.Models;

namespace WebAppSendMessageService.Interfaces
{
    public interface ISmsService
    {
        PhoneConfirmationStatus SendMessage(SendSmsDto message);
        SmsResponceStatus SendMessage(SendNotificationDto message);
    }
}
