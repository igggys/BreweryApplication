using Microsoft.EntityFrameworkCore;
using WebAppSendMessageService.DataAccess.Interfaces;
using WebAppSendMessageService.Domain.Entities;

namespace WebAppSendMessageService.DataAccess.Services
{
    public class MessageMSSQLService : IMessageSqlService
    {
        private readonly SendMessageServiceSQLContext _context;

        public MessageMSSQLService(SendMessageServiceSQLContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            try
            {
                return await _context.Messages.ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it) $"An error occurred while getting all messages: {ex.Message}"
                return null;
            }
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Messages.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it) $"An error occurred while getting message by id {id}: {ex.Message}"
                return null;
            }
        }

        public async Task<bool> AddAsync(Message message)
        {
            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                // Handle specific exceptions related to database update
                // Log the exception if needed
                return false;
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it) $"An error occurred while adding message: {ex.Message}"
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Message message)
        {
            try
            {
                Message existingMessage = await _context.Messages.FindAsync(message.Id);
                if (existingMessage == null)
                {
                    // Message with the specified id was not found
                    return false;
                }

                _context.Entry(existingMessage).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency issues (e.g., another user updated the message)
                // Log the exception if needed
                return false;
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it) $"An error occurred while update message: {ex.Message}"
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Message message = await _context.Messages.FindAsync(id);

                if (message == null)
                {
                    // Message with the specified id was not found
                    return false;
                }

                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency issues (e.g., another user updated the message)
                // Log the exception if needed
                return false;
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it) $"An error occurred while update message: {ex.Message}"
                return false;
            }
        }
    }
}
