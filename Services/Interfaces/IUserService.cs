using Talkify.DTOs.ChatDTOs;

namespace Talkify.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<ChatDTO>> GetUserChatsAsync(string userId);
    }
}