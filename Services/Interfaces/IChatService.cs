using Talkify.DTOs.MessageDTOs;

namespace Talkify.Services.Interfaces
{
    public interface IChatService
    {
        Task<string> OpenChatBetweenUsersAsync(string userId1, string userId2);
        Task<List<MessageDTO>> GetChatMessages(string chatId);
    }
}