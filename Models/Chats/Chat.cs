using Talkify.Models.Auth;
using Talkify.Models.Messages;

namespace Talkify.Models.Chats
{
    public class Chat
    {
        public string Id { get; set; } = null!;
        public string SenderId { get; set; } = null!;
        public TalkifyUser Sender { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public TalkifyUser Receiver { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public ICollection<Message> Messages { get; set; } = [];
    }
}