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

        [HttpGet("/Post")]
        public IActionResult All()
        {
            var posts = _context.Posts.ToList();
            ViewData["title"] = "All Posts";

            return View("list",posts);
        }

        [HttpGet("/Post/Status/Unread")]
        public IActionResult Unread()
        {
            var posts = _context.Posts.Where(p=>p.IsRead==false).ToList();
            ViewData["title"] = "Unread Posts";

            return View("list", posts);
        }

        [HttpGet("/Post/Status/Favorite")]
        public IActionResult Favorite()
        {
            ViewData["title"] = "Favorite Posts";
            var posts = _context.Posts.Where(p => p.IsRead == false).ToList();

            return View("list", posts);
        }

        [HttpGet("/Post/Id/{id}")]
        public IActionResult Post(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            
            if(post == null)
                return NotFound();

            ViewData["title"] = post.Title;
            
            return View(post);
        }
    }
}
