using Microsoft.AspNetCore.Mvc;
using ThirdWay.Data;

namespace ThirdWay.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReaderContext _context;

        public PostController(ILogger<HomeController> logger, ReaderContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }
    }
}
