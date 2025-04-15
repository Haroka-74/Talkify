using Microsoft.EntityFrameworkCore;
using Talkify.DTOs.ChatDTOs;
using Talkify.Repositories.Interfaces;
using Talkify.Services.Interfaces;

namespace Talkify.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly IChatRepository chatRepository = null!;

        public UserService(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public async Task<List<ChatDTO>> GetUserChatsAsync(string userId)
        {
            var chats = await chatRepository.GetChatsAsync();

            return chats
                .Where(c => (c.UserId1 == userId || c.UserId2 == userId) && c.Messages.Any())
                .Select(c => new ChatDTO
                {
                    ChatId = c.Id,
                    ReceiverId = c.UserId1 == userId ? c.UserId2 : c.UserId1,
                    ReceiverUsername = c.UserId1 == userId ? c.User2.UserName : c.User1.UserName,
                    LastMessage = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().Content,
                    LastMessageTime = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().SentAt
                })
                .OrderByDescending(c => c.LastMessageTime)
                .ToList();
        }

    }
}