using Microsoft.AspNetCore.Mvc;

namespace PlatformaSocialBookmarking.Controllers
{
    public class BookmarksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
