using Microsoft.AspNetCore.Identity;
using Talkify.Models.Chats;
using Talkify.Models.Messages;

namespace Talkify.Models.Auth
{
    public class TalkifyUser : IdentityUser
    {
        public ICollection<Chat> SentChats { get; set; } = [];
        public ICollection<Chat> ReceivedChats { get; set; } = [];
        public ICollection<Message> SentMessages { get; set; } = [];
    }
}