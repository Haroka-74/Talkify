using Microsoft.AspNetCore.Mvc;
using Talkify.Extensions;
using Talkify.Services.Interfaces;

namespace Talkify.Controllers
{
    public class ChatController : Controller
    {

        private readonly IChatService chatService = null!;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
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
            ViewData["currentUserId"] = ClaimsPrincipalExtensions.GetUserId(User);

            var chatMessages = await chatService.GetChatMessages(chatId);

            return View(chatMessages);
        }

    }
}