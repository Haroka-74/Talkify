﻿using Microsoft.AspNetCore.SignalR;
using Talkify.Models.Messages;
using Talkify.Repositories.Interfaces;

namespace Talkify.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IMessageRepository messageRepository = null!;
        private readonly IUserRepository userRepository = null!;
        private readonly IChatRepository chatRepository = null!;

        public ChatHub(IMessageRepository messageRepository, IUserRepository userRepository, IChatRepository chatRepository)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
            this.chatRepository = chatRepository;
        }

        public async Task JoinGroup(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        public async Task SendMessage(string chatId, string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Content = content,
                SenderId = senderId,
                ChatId = chatId
            };

            await messageRepository.AddMessageAsync(message);

            await chatRepository.IncrementUnreadCountAsync(chatId, receiverId);

            await Clients.Group(chatId).SendAsync("ReceiveMessage", senderId, content);

            var senderUsername = await userRepository.GetUsernameById(senderId);
            var lastMessage = content;
            var lastMessageTime = message.SentAt;

            var unreadCount = await chatRepository.GetUnreadCountAsync(chatId, receiverId);

            await Clients.User(receiverId).SendAsync("UpdateChatList", senderId, senderUsername, lastMessage, lastMessageTime, unreadCount);

            var totalUnreadCount = await chatRepository.GetTotalUnreadCountAsync(receiverId);
            await Clients.User(receiverId).SendAsync("UpdateGlobalUnreadCount", totalUnreadCount);
        }

        public async Task MarkMessagesAsRead(string chatId, string userId)
        {
            await chatRepository.MarkMessagesAsReadAsync(chatId, userId);

            await Clients.User(userId).SendAsync("MessagesMarkedAsRead", chatId);

            var totalUnreadCount = await chatRepository.GetTotalUnreadCountAsync(userId);
            await Clients.User(userId).SendAsync("UpdateGlobalUnreadCount", totalUnreadCount);
        }

    }
}