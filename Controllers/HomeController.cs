using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talkify.Extensions;
using Talkify.Repositories.Interfaces;

namespace Talkify.Controllers
{
    public class HomeController(IChatRepository chatRepository) : Controller
    {

        public async Task<IActionResult> Index()
        {

            var currentUserId = ClaimsPrincipalExtensions.GetUserId(User);

            if (!string.IsNullOrEmpty(currentUserId))
            {
                var totalUnreadCount = await chatRepository.GetTotalUnreadCountAsync(currentUserId);
                ViewData["TotalUnreadCount"] = totalUnreadCount;
            }
            else
            {
                ViewData["TotalUnreadCount"] = 0;
            }

            return View();
        }

    }
}