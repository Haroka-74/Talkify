using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talkify.Models.Chats;

namespace Talkify.Data.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {

        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasOne(c => c.Sender).WithMany(u => u.SentChats).HasForeignKey(c => c.SenderId).IsRequired();
            builder.HasOne(c => c.Receiver).WithMany(u => u.ReceivedChats).HasForeignKey(c => c.ReceiverId).IsRequired();
            builder.HasMany(c => c.Messages).WithOne(m => m.Chat).HasForeignKey(m => m.ChatId).IsRequired();
        }

    }
}