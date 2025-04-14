using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talkify.Models.Auth;
using Talkify.Models.Chats;
using Talkify.Models.Messages;

namespace Talkify.Data
{
    public class TalkifyDbContext : IdentityDbContext<TalkifyUser>
    {

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public TalkifyDbContext(DbContextOptions<TalkifyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(TalkifyDbContext).Assembly);
        }

    }
}