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



        public IActionResult Index(int page = 1)
        {
            const int pageSize = 12;

            var bookmarks = GetBookmarks();

            var search = HttpContext.Request.Query["search"].ToString().Trim();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower(); // Convert search string to lowercase for case-insensitive matching

                bookmarks = _db.Bookmarks
                    .Where(bk =>
                        bk.Title.ToLower().Contains(search) ||
                        bk.Description.ToLower().Contains(search) ||
                        bk.Bookmark_Has_Categories.Any(bhc =>
                            bhc.Category.CategoryName.ToLower().Contains(search)
                        )
                    )
                    .Include(b => b.Bookmark_Has_Categories)
                    .ThenInclude(bhc => bhc.Category)
                    .Include(b => b.User)
                    .Include(b => b.Bookmark_Has_Images)
                    .ThenInclude(bhi => bhi.Image)
                    .OrderBy(b => b.Date)
                    .ToList();
            }

            ViewBag.SearchString = search;

            int totalItems = bookmarks.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            page = Math.Max(1, Math.Min(page, totalPages));

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);

            var paginatedBookmarks = bookmarks.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.currentPage = page;
            ViewBag.lastPage = totalPages;

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.BaseUrl = $"/Home/Index/?search={search}";
            }
            else
            {
                ViewBag.BaseUrl = "/Home/Index";
            }

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View(paginatedBookmarks);
        }




        private List<Bookmark> GetBookmarks()
        {
            var bookmarks = _db.Bookmarks.Include(b => b.Bookmark_Has_Categories)
                                         .Include(b => b.Bookmark_Has_Images)
                                            .ThenInclude(bhi => bhi.Image)
                                         .OrderByDescending(b => b.Votes)
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