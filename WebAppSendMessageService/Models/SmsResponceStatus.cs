using System.Net;

namespace WebAppSendMessageService.Models
{
    public class SmsResponceStatus
    {
        public Guid Id { get; set; }
        public string ResponceStatus { get; set; }
        public HttpStatusCode ResponceCode { get; set; }
        public DateTimeOffset RequestDateTime { get; set; }
    }
}
