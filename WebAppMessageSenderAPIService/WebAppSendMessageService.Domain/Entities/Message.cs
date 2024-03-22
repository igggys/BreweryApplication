using System.Net;

namespace WebAppSendMessageService.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Provider { get; set; }
        public string Recipient { get; set; }
        public string MessageText { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset SendDate { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}
