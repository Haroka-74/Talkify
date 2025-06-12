using Microsoft.EntityFrameworkCore;
using Talkify.Data;
using Talkify.Models.Auth;
using Talkify.Repositories.Interfaces;

namespace Talkify.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {

        private readonly TalkifyDbContext context;

        public UserRepository(TalkifyDbContext context)
        {
            this.context = context;
        }

        public async Task<List<TalkifyUser>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<string?> GetUsernameById(string id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user?.UserName;
        }
    }
}