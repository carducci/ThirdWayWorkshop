using Microsoft.AspNetCore.Mvc;
using ThirdWay.Web.Models;
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
            return View(new FeedViewModel(){Feeds = feeds});
        }


        [HttpPost("/Feed")]
        public async Task<IActionResult> Add(FeedViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _feedService.UpsertFeedAsync(model.NewUrl!);
                    ViewData["title"] = "Feeds";
                    ViewData["CurrentUrl"] = $"/Feed";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("NewUrl", ex.Message);
            }

            model.Feeds = await _feedService.GetAllAsync();
            return View("Index", model);
        }

        [HttpPost("/Feed/RefreshAll")]

        public async Task<IActionResult> RefreshAll(string redirectUrl)
        {
            await _feedService.RefreshAllAsync();
            return LocalRedirect(redirectUrl);
        }

        [HttpPost("/Feed/Id/{id}/DeleteFeed")]
        public async Task<IActionResult> Delete(int id)
        {
            await _feedService.DeleteFeed(id);
            return RedirectToAction("Index");
        }

    }
}
