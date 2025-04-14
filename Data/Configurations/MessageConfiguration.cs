using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talkify.Models.Messages;

namespace Talkify.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {

        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.Sender).WithMany(u => u.SentMessages).HasForeignKey(m => m.SenderId).IsRequired();
        }

    }
}