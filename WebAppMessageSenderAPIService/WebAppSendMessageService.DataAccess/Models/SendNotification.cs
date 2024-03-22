using System.ComponentModel.DataAnnotations;

namespace WebAppSendMessageService.DataAccess.Models
{
    public class SendNotification
    {
        [MaxLength(12)]
        public string Phone { get; set; }
        public string Body { get; set; }
    }
}
