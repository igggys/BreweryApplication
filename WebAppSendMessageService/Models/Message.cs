using System.Net;

namespace WebAppSendMessageService.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Provider { get; set; }
        public string Recipient { get; set; }
        public string MessageText { get; set; }
        public string Subject { get; set; }
        public DateTime SendDate { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}
