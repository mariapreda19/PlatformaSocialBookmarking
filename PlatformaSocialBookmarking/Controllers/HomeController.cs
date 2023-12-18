using Microsoft.AspNetCore.Mvc;
using PlatformaSocialBookmarking.Models;
using PlatformaSocialBookmarking.Data;
using System.Diagnostics;

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
            var categories = GetCategories();
            return View(categories);
        }

        private List<Category> GetCategories()
        {
            var categories = _db.Categories.OrderBy(c => c.CategoryName).Take(3).ToList();
            return categories;
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
