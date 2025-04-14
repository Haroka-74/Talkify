using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

            return RedirectToAction("ChatPage", new { chatId, receiverUsername, receiverId });
        }

        public async Task<IActionResult> ChatPage(string chatId, string receiverUsername, string receiverId)
        {
            ViewData["receiverUsername"] = receiverUsername;
            ViewData["LoggedUserId"] = ClaimsPrincipalExtensions.GetUserId(User);
            ViewData["ChatId"] = chatId;
            ViewData["ReceiverId"] = receiverId;
            var chatMessages = await chatService.GetChatMessages(chatId);
            return View(chatMessages);
        }

    }
}