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

    }
}