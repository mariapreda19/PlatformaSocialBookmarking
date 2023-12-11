using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformaSocialBookmarking.Data;

namespace PlatformaSocialBookmarking.Controllers
{
    public class BookmarksController : Controller
    {
        private readonly ApplicationDbContext db;
        public BookmarksController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var bookmark = db.Bookmarks.Include("Category");
            ViewBag.Bookmarks = bookmark;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }
    }
}
