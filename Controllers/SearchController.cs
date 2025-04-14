using Microsoft.AspNetCore.Mvc;
using Talkify.Extensions;
using Talkify.Services.Interfaces;

namespace Talkify.Controllers
{
    public class SearchController : Controller
    {

        private readonly ISearchService searchService = null!;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public async Task<IActionResult> Index(string searchTerm = "")
        {
            var id = ClaimsPrincipalExtensions.GetUserId(User);
            var users = await searchService.SearchAsync(searchTerm, id);
            ViewBag.SearchTerm = searchTerm;
            return View(users);
        }

    }
}