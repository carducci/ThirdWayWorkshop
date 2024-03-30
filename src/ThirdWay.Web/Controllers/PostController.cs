using Microsoft.AspNetCore.Mvc;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class PostController(IPostService postService) : Controller
    {
        private readonly IPostService _postService = postService;

        [HttpGet("/Post")]
        public IActionResult All(int page = 1)
        {
            var posts = _postService.GetAll(6, (page-1)*5);
            
            ViewData["title"] = $"All Posts {(page>1 ? "- Page " + page : "")}";
            ViewData["current-page"] = page;
            ViewData["next-page"] = page;
            ViewData["more-link"] = $"/Post?page={page + 1}";

            if (posts.Count == 6)
            {
                ViewData["next-page"] = page + 1;
            }
            
            return View("list",posts.OrderByDescending( p => p.PublishDateTime).Take(5).ToList());
        }

        [HttpGet("/Post/Status/Unread")]
        public IActionResult Unread(int page = 1)
        {
            var posts = _postService.GetUnread(6, (page - 1) * 5);
            ViewData["title"] = $"Unread Posts  {(page>1 ? "- Page " + page : "")}";
            ViewData["current-page"] = page;
            ViewData["next-page"] = page;
            ViewData["more-link"] = $"/Post/Status/Unread?page={page + 1}";

            if (posts.Count == 6)
            {
                ViewData["next-page"] = page + 1;
            }

            return View("list", posts.OrderByDescending(p => p.PublishDateTime).Take(5).ToList());

        }

        [HttpGet("/Post/Status/Favorite")]
        public IActionResult Favorite(int page = 1)
        {
            var posts = _postService.GetFavorite(6, (page - 1) * 5);
            ViewData["title"] = $"Favorite Posts  {(page > 1 ? "- Page " + page : "")}";
            ViewData["current-page"] = page;
            ViewData["next-page"] = page;
            ViewData["more-link"] = $"/Post/Status/Favorite?page={page + 1}";

            if (posts.Count == 6)
            {
                ViewData["next-page"] = page + 1;
            }

            return View("list", posts.OrderByDescending(p => p.PublishDateTime).Take(5).ToList());
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
