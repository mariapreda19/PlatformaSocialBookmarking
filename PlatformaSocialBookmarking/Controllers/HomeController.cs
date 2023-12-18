using Microsoft.AspNetCore.Mvc;
using PlatformaSocialBookmarking.Models;
using PlatformaSocialBookmarking.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace PlatformaSocialBookmarking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

      

        [HttpPost]
        public IActionResult AddCategory(string categoryName, string coverUrl)
        {
            return Redirect("/Category/New");

        }

        [HttpPost]
        public IActionResult AddBookmark(string title, string description, int categoryId)
        {
            return RedirectToAction("Index");
        }



        public IActionResult Index()
        {
            var bookmarks = GetBookmarks();
            return View(bookmarks);
        }


        private List<Bookmark> GetBookmarks()
        {
            var bookmarks = _db.Bookmarks.Include(b => b.Bookmark_Has_Categories)
                                         .Include(b => b.Bookmark_Has_Images)
                                            .ThenInclude(bhi => bhi.Image)
                                         .OrderBy(b => b.Date)
                                         .ToList();

            return bookmarks;
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
