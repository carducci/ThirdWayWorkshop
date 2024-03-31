using Microsoft.AspNetCore.Mvc;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class FeedController(IFeedService feedService) : Controller
    {
        private readonly IFeedService _feedService = feedService;

        [HttpGet("/Feed")]
        public async Task<IActionResult> Index()
        {
            var feeds = await _feedService.GetAllAsync();
            ViewData["title"] = "Feeds";
            ViewData["CurrentUrl"] = $"/Feed";
            return View(feeds);
        }

        [HttpPost("/Feed/RefreshAll")]

        public async Task<IActionResult> RefreshAll(string redirectUrl)
        {
            await _feedService.RefreshAllAsync();
            return LocalRedirect(redirectUrl);
        }

    }
}
