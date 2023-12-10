using Microsoft.AspNetCore.Mvc;

namespace PlatformaSocialBookmarking.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
