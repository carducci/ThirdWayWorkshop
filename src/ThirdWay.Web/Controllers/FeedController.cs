using Microsoft.AspNetCore.Mvc;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class FeedController(IFeedService feedService) : Controller
    {
        private readonly IFeedService _feedService = feedService;

        [HttpGet("/Feed")]
        public IActionResult Index()
        {
            var feeds = _feedService.GetAll();
            ViewData["title"] = "Feeds";
            return View(feeds);
        }
    }
}
