using Talkify.Models.Chats;

namespace Talkify.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task<List<Chat>> GetChatsAsync();
        Task<Chat> GetChatAsync(string chatId);
        Task CreateChatAsync(Chat chat);

        // ------------------------------------------------------------
        Task IncrementUnreadCountAsync(string chatId, string userId);
        Task MarkMessagesAsReadAsync(string chatId, string userId);
        Task<int> GetUnreadCountAsync(string chatId, string userId);
    }
}