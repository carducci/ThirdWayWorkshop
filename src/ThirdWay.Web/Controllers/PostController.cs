using Microsoft.AspNetCore.Mvc;
using ThirdWay.Data;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [HttpGet("/Post")]
        public IActionResult All(int page = 1)
        {
            var posts = _postService.GetAll();
            ViewData["title"] = "All Posts";

            return View("list",posts);
        }

        [HttpGet("/Post/Status/Unread")]
        public IActionResult Unread(int page = 1)
        {
            var posts = _postService.GetUnread();
            ViewData["title"] = "Unread Posts";

            return View("list", posts);
        }

        [HttpGet("/Post/Status/Favorite")]
        public IActionResult Favorite(int page = 1)
        {
            ViewData["title"] = "Favorite Posts";
            var posts = _postService.GetFavorite();

            return View("list", posts);
        }

        [HttpGet("/Post/Id/{id}")]
        public IActionResult Post(int id)
        {
            var post = _postService.GetPost(id);
            if(post == null)
                return NotFound();

            ViewData["title"] = post.Title;
            
            return View(post);
        }

        [HttpGet("/Post/Hash/{hash}")]
        public IActionResult Post(string hash)
        {
            var post = _postService.GetPost(hash);
            if (post == null)
                return NotFound();

            ViewData["title"] = post.Title;

            return View(post);
        }
    }
}
