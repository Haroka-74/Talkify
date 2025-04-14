using Talkify.Models.Auth;

namespace Talkify.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<TalkifyUser>> GetUsersAsync();
    }
}