using Microsoft.EntityFrameworkCore;
using WebAppSendMessageService.Domain.Entities;

namespace WebAppSendMessageService.DataAccess
{
    public class SendMessageServiceSQLContext : DbContext
    {
        public SendMessageServiceSQLContext(DbContextOptions<SendMessageServiceSQLContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
