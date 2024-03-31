using Microsoft.AspNetCore.Mvc;
using ThirdWay.Web.Models;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class FeedController(IFeedService feedService) : BaseController
    {
        private readonly IFeedService _feedService = feedService;

        [HttpGet("/Feed")]
        public async Task<IActionResult> Index()
        {
            var feeds = await _feedService.GetAllAsync();
            ViewData["title"] = "Feeds";
            ViewData["CurrentUrl"] = $"/Feed";

            await Bottleneck();
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

                    await Bottleneck();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("NewUrl", ex.Message);
            }

            model.Feeds = await _feedService.GetAllAsync();

            await Bottleneck();
            return View("Index", model);
        }

        [HttpPost("/Feed/RefreshAll")]

        public async Task<IActionResult> RefreshAll(string redirectUrl)
        {
            await _feedService.RefreshAllAsync();

            await Bottleneck();
            return LocalRedirect(redirectUrl);
        }

        [HttpPost("/Feed/Id/{id}/DeleteFeed")]
        [Obsolete("This endpoint exists for compatibility/graceful degration only. A proper DELETE request on the resources is preferred")]
        public async Task<IActionResult> Delete(int id)
        {
            await _feedService.DeleteFeed(id);

            await Bottleneck();
            return RedirectToAction("Index");
        }

        [HttpDelete("/Feed/Id/{id}")]
        public async Task<IActionResult> DeleteFeed(int id)
        {
            try
            {
                await _feedService.DeleteFeed(id);
                await Bottleneck();
            }
            catch(Exception){
                //Something went wrong here. Most likely a 404
                //A delete is supposed to be idempotent so we will ignore this
            }
            // Return a 204 No Content response
            return NoContent();
        }
    }
}
