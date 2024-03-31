using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class PostController(IPostService postService) : Controller
    {
        private readonly IPostService _postService = postService;

        [HttpGet("/Post")]
        public async Task<IActionResult> All(int page = 1)
        {
            var posts = await _postService.GetAllAsync(6, (page-1)*5);
            
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
        public async Task<IActionResult> Unread(int page = 1)
        {
            var posts = await _postService.GetUnreadAsync(6, (page - 1) * 5);

            ViewData["title"] = $"Unread Posts  {(page>1 ? "- Page " + page : "")}";
            ViewData["CurrentUrl"] = $"/Post/Status/Unread{(page > 1 ? "?page=" + page : "")}";
            ViewData["current-page"] = page;
            ViewData["next-page"] = page;
            ViewData["more-link"] = $"/Post/Status/Unread?page={page + 1}";

            if (posts.Count == 6)
            {
                ViewData["next-page"] = page + 1;
            }

            return View("list", posts.OrderByDescending(p => p.PublishDateTime).Take(5).ToList());

        }
        [HttpGet("/Post/Status/Unread/Count")]
        public PartialViewResult UnreadCount()
        {
            int postCount = _postService.GetUnreadCountAsync();

            return PartialView("_countBadge", postCount);

        }

        [HttpGet("/Post/Status/Favorite")]
        public async Task<IActionResult> Favorite(int page = 1)
        {
            var posts = await _postService.GetFavoriteAsync(6, (page - 1) * 5);

            ViewData["title"] = $"Favorite Posts  {(page > 1 ? "- Page " + page : "")}";
            ViewData["CurrentUrl"] = $"/Post/Status/Favorite{(page > 1 ? "?page=" + page : "")}";
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
        public async Task<IActionResult> Post(int id)
        {
            var post = await _postService.GetPostAsync(id);
            if(post == null)
                return NotFound();

            if (post is { IsRead: false })
            {
                await _postService.MarkReadAsync(post.Id);
            }

            ViewData["title"] = post.Title;
            ViewData["CurrentUrl"] = $"/Post/Id/{id}";

            return View(post);
        }

        [HttpGet("/Post/Hash/{hash}")]
        public async Task<IActionResult> Post(string hash)
        {
            var post = await _postService.GetPostAsync(hash);

            if (post == null)
                return NotFound();

            if (post is { IsRead: false })
            {
                await _postService.MarkReadAsync(post.Id);
            }

            ViewData["title"] = post.Title;
            ViewData["CurrentUrl"] = $"/Post/Hash/{hash}";

            return View(post);
        }

        [HttpPost("/Post/Id/{id}/ToggleFavorite")]
        public async Task<IActionResult> ToggleFavorite(int id, string redirectUrl)
        {
            await _postService.ToggleFavoriteAsync(id);
            return LocalRedirect(redirectUrl);
        }

        [HttpPost("/Post/Id/{id}/MarkRead")]
        public async Task<IActionResult> MarkRead(int id, string redirectUrl)
        {
            await _postService.MarkReadAsync(id);
            return LocalRedirect(redirectUrl);
        }

        [HttpPost("/Post/Id/{id}/MarkUnread")]
        public async Task<IActionResult> MarkUnread(int id, string redirectUrl)
        {
            await _postService.MarkUnreadAsync(id);
            return LocalRedirect(redirectUrl);
        }

        [HttpGet("/Post/Search")]
        public async Task<IActionResult> Search(string query)
        {
            ViewData["title"] = $"Search Results";
            ViewData["CurrentUrl"] = $"/Post/Search?query={query}";
            var posts = await _postService.SearchPosts(query);
            return View("List", posts);
        }
    }
}
