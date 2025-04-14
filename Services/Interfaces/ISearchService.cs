using Talkify.DTOs.SearchDTOs;

namespace Talkify.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchDTO>> SearchAsync(string searchTerm, string currentUserId);
    }
}