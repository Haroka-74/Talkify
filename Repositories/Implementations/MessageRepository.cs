using Microsoft.EntityFrameworkCore;
using Talkify.Data;
using Talkify.Models.Messages;
using Talkify.Repositories.Interfaces;

namespace Talkify.Repositories.Implementations
{
    public class MessageRepository : IMessageRepository
    {

        private readonly TalkifyDbContext context = null!;

        public MessageRepository(TalkifyDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await context.Messages.Include(m => m.Sender).Include(m => m.Chat).ToListAsync();
        }

        public async Task<Message> GetMessageAsync(string messageId)
        {
            return await context.Messages.Include(m => m.Sender).Include(m => m.Chat).FirstOrDefaultAsync(m => m.Id == messageId);
        }

        public async Task AddMessageAsync(Message message)
        {
            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();
        }

    }
}