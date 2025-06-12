using Microsoft.EntityFrameworkCore;
using Talkify.Data;
using Talkify.Models.Chats;
using Talkify.Repositories.Interfaces;

namespace Talkify.Repositories.Implementations
{
    public class ChatRepository : IChatRepository
    {

        private readonly TalkifyDbContext context = null!;

        public ChatRepository(TalkifyDbContext context)
        {
            this.context = context;
        }
        
        public async Task<List<Chat>> GetChatsAsync()
        {
            return await context.Chats.Include(c => c.User1).Include(c => c.User2).Include(c => c.Messages).ToListAsync();
        }

        public async Task<Chat> GetChatAsync(string chatId)
        {
            return await context.Chats.Include(c => c.User1).Include(c => c.User2).Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task CreateChatAsync(Chat chat)
        {
            await context.Chats.AddAsync(chat);
            await context.SaveChangesAsync();
        }

        public async Task IncrementUnreadCountAsync(string chatId, string userId)
        {
            var chat = await context.Chats.FindAsync(chatId);
            if (chat != null)
            {
                if (chat.UserId1 == userId)
                {
                    chat.UnreadCountUser1++;
                }
                else if (chat.UserId2 == userId)
                {
                    chat.UnreadCountUser2++;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task MarkMessagesAsReadAsync(string chatId, string userId)
        {
            var chat = await context.Chats.FindAsync(chatId);
            if (chat != null)
            {
                if (chat.UserId1 == userId)
                {
                    chat.UnreadCountUser1 = 0;
                }
                else if (chat.UserId2 == userId)
                {
                    chat.UnreadCountUser2 = 0;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> GetUnreadCountAsync(string chatId, string userId)
        {
            var chat = await context.Chats.FindAsync(chatId);
            if (chat != null)
            {
                if (chat.UserId1 == userId)
                {
                    return chat.UnreadCountUser1;
                }
                else if (chat.UserId2 == userId)
                {
                    return chat.UnreadCountUser2;
                }
            }

            return 0;
        }

    }
}