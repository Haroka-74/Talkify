using Microsoft.AspNetCore.SignalR;
using Talkify.DTOs.MessageDTOs;
using Talkify.Models.Messages;
using Talkify.Repositories.Interfaces;

namespace Talkify.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IMessageRepository messageRepository = null!;
        
        public ChatHub(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task SendMessage(string chatId, string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Content = content,
                SenderId = senderId,
                ChatId = chatId,
                SentAt = DateTime.Now,
            };

            await messageRepository.AddMessageAsync(message);

            await Clients.Users(senderId, receiverId).SendAsync("ReceiveMessage", chatId, senderId, receiverId, content);
        }

    }
}