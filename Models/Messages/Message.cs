using Talkify.Models.Auth;
using Talkify.Models.Chats;

namespace Talkify.Models.Messages
{
    public class Message
    {
        public string Id { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SenderId { get; set; } = null!;
        public TalkifyUser Sender { get; set; } = null!;
        public string ChatId { get; set; } = null!;
        public Chat Chat { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}