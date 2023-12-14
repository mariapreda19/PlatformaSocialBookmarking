using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlatformaSocialBookmarking.Models;
using PlatformaSocialBookmarking.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

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
