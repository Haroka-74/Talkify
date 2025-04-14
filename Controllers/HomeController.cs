using Microsoft.AspNetCore.Mvc;

namespace Talkify.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}