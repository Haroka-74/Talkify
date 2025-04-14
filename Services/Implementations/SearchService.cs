using Talkify.DTOs.SearchDTOs;
using Talkify.Repositories.Interfaces;
using Talkify.Services.Interfaces;

namespace Talkify.Services.Implementations
{
    public class SearchService : ISearchService
    {

        private readonly IUserRepository userRepository = null!;

        public SearchService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<SearchDTO>> SearchAsync(string searchTerm, string currentUserId)
        {
            var users = await userRepository.GetUsersAsync();
            return users.Where(u => u.UserName.Contains(searchTerm) && u.Id != currentUserId).Select(u => new SearchDTO
            {
                UserId = u.Id,
                Username = u.UserName
            }).ToList();
        }

    }
}