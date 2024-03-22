namespace WebAppSendMessageService.Models
{
    public class SendMessage
    {
        public string Provider { get; set; }
        public string Recipient { get; set; }
        public string MessageText { get; set; }
        public string Subject { get; set; }
    }
}
