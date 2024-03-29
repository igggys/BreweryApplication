namespace WebAppSendMessageService.DataAccess.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string recipient, string message, string body);
    }
}
