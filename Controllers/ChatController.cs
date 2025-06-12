using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Talkify.Extensions;
using Talkify.Hubs;
using Talkify.Repositories.Interfaces;
using Talkify.Services.Interfaces;

namespace Talkify.Controllers
{
    public class ChatController : Controller
    {

        private readonly IChatService chatService = null!;
        private readonly IChatRepository chatRepository = null!;
        private readonly IHubContext<ChatHub> hubContext = null!;

        public ChatController(IChatService chatService, IChatRepository chatRepository, IHubContext<ChatHub> hubContext)
        {
            this.chatService = chatService;
            this.chatRepository = chatRepository;
            this.hubContext = hubContext;
        }

        public async Task<IActionResult> OpenChat(string receiverId, string receiverUsername)
        {
            var id = ClaimsPrincipalExtensions.GetUserId(User);

            var chatId = await chatService.OpenChatBetweenUsersAsync(id, receiverId);

            return RedirectToAction("Chat", new { chatId, receiverUsername, receiverId });
        }

        public async Task<IActionResult> Chat(string chatId, string receiverUsername, string receiverId)
        {
            ViewData["ChatId"] = chatId;
            ViewData["receiverUsername"] = receiverUsername;
            ViewData["receiverId"] = receiverId;
            var currentUserId = ClaimsPrincipalExtensions.GetUserId(User);
            ViewData["currentUserId"] = currentUserId;

            await chatRepository.MarkMessagesAsReadAsync(chatId, currentUserId);

            // Notify the hub to update unread count
            await hubContext.Clients.User(currentUserId).SendAsync("MessagesMarkedAsRead", chatId);

            var chatMessages = await chatService.GetChatMessages(chatId);

            return View(chatMessages);
        }


        [HttpPost]
        public async Task<IActionResult> MarkAsRead(string chatId)
        {
            var currentUserId = ClaimsPrincipalExtensions.GetUserId(User);
            await chatRepository.MarkMessagesAsReadAsync(chatId, currentUserId);

            // Notify the hub to update unread count
            await hubContext.Clients.User(currentUserId).SendAsync("MessagesMarkedAsRead", chatId);

            return Ok();
        }

    }
}