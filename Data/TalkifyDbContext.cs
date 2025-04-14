using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talkify.Models.Auth;

namespace Talkify.Data
{
    public class TalkifyDbContext : IdentityDbContext<TalkifyUser>
    {

        // Props:

        public TalkifyDbContext(DbContextOptions<TalkifyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(TalkifyDbContext).Assembly);
        }

    }
}