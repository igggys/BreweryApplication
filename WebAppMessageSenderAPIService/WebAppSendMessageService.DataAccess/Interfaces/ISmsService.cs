using WebAppSendMessageService.DataAccess.Models;

namespace WebAppSendMessageService.DataAccess.Interfaces
{
    public interface ISmsService
    {
        SmsResponceStatus SendMessage(SendNotification message);
    }
}
