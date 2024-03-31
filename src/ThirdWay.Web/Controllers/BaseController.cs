using Microsoft.AspNetCore.Mvc;

namespace ThirdWay.Web.Controllers
{
    public class BaseController : Controller
    {
        protected async Task Bottleneck(int delay = 1500)
        {
            await Task.Delay(delay);
        }
    }
}
