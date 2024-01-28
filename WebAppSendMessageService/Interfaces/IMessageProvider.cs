namespace WebAppSendMessageService.Interfaces
{
    public interface IMessageProvider
    {
        Task<bool> SendMessageAsync(string recipient, string message, string body);
    }
}
