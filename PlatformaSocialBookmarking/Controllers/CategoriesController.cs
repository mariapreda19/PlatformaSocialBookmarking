using Microsoft.AspNetCore.Mvc;

namespace PlatformaSocialBookmarking.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
