using System.ComponentModel.DataAnnotations;

namespace WebAppSendMessageService.Dto
{
    public class SendSmsDto
    {
        [MaxLength(12)]
        public string Phone { get; set; }

        [Range(0, 9999)]
        public int Code { get; set; }
    }
}
