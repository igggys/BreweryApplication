namespace WebAppSendMessageService.Models
{
    public class PhoneConfirmationStatus : SmsResponceStatus
    {
        public int Code { get; set; }
        public string Phone { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
