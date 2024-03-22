using WebAppSendMessageService.Domain.Entities;

namespace WebAppSendMessageService.DataAccess.Interfaces
{
    public interface IMessageSqlService
    {
        Task<IEnumerable<Message>> GetAllAsync();
        Task<Message> GetByIdAsync(int id);
        Task<bool> AddAsync(Message message);
        Task<bool> UpdateAsync(Message message);
        Task<bool> DeleteAsync(int id);
    }
}
