using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThirdWay.Data;
using ThirdWay.Data.Model;
using ThirdWay.Web.Models;
using ThirdWay.Web.Service;

namespace ThirdWay.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(){ }

        public IActionResult Index()
        {
            return RedirectToAction("All", "Post");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
