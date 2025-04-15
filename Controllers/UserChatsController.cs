using Microsoft.AspNetCore.Mvc;
using Talkify.Extensions;
using Talkify.Services.Interfaces;

namespace Talkify.Controllers
{
    public class UserChatsController : Controller
    {

        private readonly IUserService userService = null!;

        public UserChatsController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var id = ClaimsPrincipalExtensions.GetUserId(User);

            var userChats = await userService.GetUserChatsAsync(id);

            return View("UserChats", userChats);
        }

    }
}