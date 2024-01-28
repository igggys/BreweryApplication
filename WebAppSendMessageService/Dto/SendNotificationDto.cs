using System.ComponentModel.DataAnnotations;

namespace WebAppSendMessageService.Dto
{
    public class SendNotificationDto
    {
        [MaxLength(12)]
        public string Phone { get; set; }
        public string Body { get; set; }
    }
}
