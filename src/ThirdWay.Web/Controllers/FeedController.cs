using Microsoft.AspNetCore.Mvc;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class FeedController : Controller
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpGet("/Feed")]
        public IActionResult Index()
        {
            var feeds = _feedService.GetAll();
            ViewData["title"] = "Feeds";
            return View(feeds);
        }
    }
}
