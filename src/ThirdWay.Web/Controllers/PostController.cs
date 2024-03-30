using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class PostController(IPostService postService) : Controller
    {
        private readonly IPostService _postService = postService;

        [HttpGet("/Post")]
        public IActionResult All(int page = 1)
        {
            var posts = _postService.GetAllAsync(6, (page-1)*5);
            
            ViewData["title"] = $"All Posts {(page>1 ? "- Page " + page : "")}";
            ViewData["CurrentUrl"] = $"/Post{(page > 1 ? "?page=" + page : "")}";
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
            var posts = _postService.GetUnreadAsync(6, (page - 1) * 5);

            ViewData["title"] = $"Unread Posts  {(page>1 ? "- Page " + page : "")}";
            ViewData["CurrentUrl"] = $"/Post{(page > 1 ? "?page=" + page : "")}";
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
            var posts = _postService.GetFavoriteAsync(6, (page - 1) * 5);

            ViewData["title"] = $"Favorite Posts  {(page > 1 ? "- Page " + page : "")}";
            ViewData["CurrentUrl"] = $"/Post{(page > 1 ? "?page=" + page : "")}";
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
            var post = _postService.GetPostAsync(id);
            if(post == null)
                return NotFound();
            ViewData["title"] = post.Title;
            ViewData["CurrentUrl"] = $"/Post/Id/{id}";

            return View(post);
        }

        [HttpGet("/Post/Hash/{hash}")]
        public IActionResult Post(string hash)
        {
            var post = _postService.GetPostAsync(hash);
            if (post == null)
                return NotFound();

            ViewData["title"] = post.Title;
            ViewData["CurrentUrl"] = $"/Post/Hash/{hash}";

            return View(post);
        }

        [HttpPost("/Post/Id/{id}/ToggleFavorite")]
        public IActionResult ToggleFavorite(int id)
        {
            await _postService.MarkFavoriteAsync(id);
        }
    }
}
