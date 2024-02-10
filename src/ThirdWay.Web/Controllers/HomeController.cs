using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThirdWay.Data;
using ThirdWay.Web.Models;

namespace ThirdWay.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReaderContext _context;

        public HomeController(ILogger<HomeController> logger, ReaderContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
