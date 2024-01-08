using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Text.RegularExpressions;

using PlatformaSocialBookmarking.Controllers;
using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using SQLitePCL;

namespace PlatformaSocialBookmarking.Controllers
{
    [Authorize]
    public class BookmarksController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public BookmarksController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        [Authorize(Roles = "User, Editor, Admin")]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            var bookmarks = db.Bookmarks.Include(b => b.Bookmark_Has_Categories)
                                        .Include(b => b.User)
                                        .Where(b => b.UserId == userId)
                                        .Include(b => b.Bookmark_Has_Images)
                                            .ThenInclude(bhi => bhi.Image)
                                        .OrderBy(b => b.Date)
                                        .ToList();

            var search = "";

            if (Convert.ToString(HttpContext.Request.Query["search"]) != null)
            {
                search = Convert.ToString(HttpContext.Request.Query["search"]).Trim();

                List<int> bookmarkIds = db.Bookmarks.Where(
                                                     bk => bk.Title.Contains(search) ||
                                                     bk.Description.Contains(search)).Select(b => b.Id).ToList();
                bookmarks = db.Bookmarks.Where(bookmark => bookmarkIds.Contains(bookmark.Id))
                                        .Include(b => b.Bookmark_Has_Categories)
                                        .Include(b => b.User)
                                        .Where(b => b.UserId == userId)
                                        .Include(b => b.Bookmark_Has_Images)
                                            .ThenInclude(bhi => bhi.Image)
                                        .OrderBy(b => b.Date)
                                        .ToList();
            }


            ViewBag.SearchString = search;
            ViewBag.Bookmarks = bookmarks;


            if (search != "")
            {
                ViewBag.BaseUrl = "/Bookmarks/Index/?search=" + search;
            }
            else
            {
                ViewBag.BaseUrl = "/Bookmarks/Index";
            }

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View(bookmarks);
        }



        [Authorize(Roles = "User, Editor, Admin")]
        public IActionResult Show(int id)
        {
            Bookmark bookmark = db.Bookmarks
                .Include(b => b.User)
                .Include("Comments")
                .Include("Comments.User")
                .Include(b => b.Bookmark_Has_Categories)
                    .ThenInclude(bhc => bhc.Category)
                .Include(b => b.Bookmark_Has_Images)
                    .ThenInclude(bhi => bhi.Image)
                .FirstOrDefault(b => b.Id == id);

            if (bookmark == null)
            {
                return NotFound();
            }

            SetAccessRights();

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View(bookmark);
        }


        [HttpPost]
        [Authorize(Roles = "User, Editor, Admin")]
        public async Task<IActionResult> Show([FromForm] Comment comment, int id)
        {
            comment.Date = DateTime.Now;
            comment.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Show), new { id = comment.BookmarkId });
            }
            else
            {
                Bookmark bookmark = db.Bookmarks.Include(b => b.User)
                                                .Include("Comments")
                                                .Include("Comments.User")
                                                .Include(b => b.Bookmark_Has_Categories)
                                                    .ThenInclude(bhc => bhc.Category)
                                                .Include(b => b.Bookmark_Has_Images)
                                                    .ThenInclude(bhi => bhi.Image)
                                                .First();


                ViewBag.UserBookmarks = db.Categories
                    .Where(b => b.UserId == _userManager.GetUserId(User))
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.CategoryName })
                    .ToList();

                SetAccessRights();

                return View(bookmark);
            }
        }


        [Authorize(Roles = "Editor, Admin")]
        public IActionResult New()
        {
            Bookmark bookmark = new Bookmark();

            var userId = _userManager.GetUserId(User);
            var userImages = db.Images.Where(img => img.UserId == userId).ToList();

            var userCategories = db.Categories.Where(cat => cat.UserId == userId).ToList();

            ViewBag.UserImages = userImages;
            ViewBag.UserCategories = new SelectList(userCategories, "Id", "CategoryName");

            return View(bookmark);
        }




        [Authorize(Roles = "Editor, Admin")]
        [HttpPost]
        public IActionResult New(Bookmark bookmark, string[] selectedImages, int selectedCategory)
        {
            bookmark.Date = DateTime.Now;
            bookmark.UserId = _userManager.GetUserId(User);
            bookmark.Votes = 0;

            if (ModelState.IsValid)
            {
                db.Bookmarks.Add(bookmark);
                db.SaveChanges();

                if (selectedImages != null)
                {
                    foreach (var imageId in selectedImages)
                    {
                        var bookmarkHasImage = new Bookmark_Has_Image
                        {
                            BookmarkId = bookmark.Id,
                            ImageId = int.Parse(imageId)
                        };

                        db.Bookmark_Has_Images.Add(bookmarkHasImage);
                    }

                    db.SaveChanges();
                }

                var bookmarkHasCategory = new Bookmark_Has_Category
                {
                    BookmarkId = bookmark.Id,
                    CategoryId = selectedCategory
                };

                db.Bookmark_Has_Categories.Add(bookmarkHasCategory);
                db.SaveChanges();

                TempData["message"] = "Bookmarkul a fost adaugat";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                var userId = _userManager.GetUserId(User);
                var userImages = db.Images.Where(img => img.UserId == userId).ToList();
                var userCategories = db.Categories.Where(cat => cat.UserId == userId).ToList();
                ViewBag.UserImages = userImages;
                ViewBag.UserCategories = new SelectList(userCategories, "Id", "CategoryName");

                return View(bookmark);
            }
        }











        [Authorize(Roles = "Editor,Admin")]
        public IActionResult Edit(int id)
        {

            Bookmark bookmark = db.Bookmarks.Include("Category")
                                            .Include("Images")
                                            .Where(bkm => bkm.Id == id)
                                            .First();

            bookmark.Categ = GetAllCategories();

            if (bookmark.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(bookmark);
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui bookmark care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [Authorize(Roles = "Editor,Admin")]
        public IActionResult Edit(int id, Bookmark requestBookmark)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);


            if (ModelState.IsValid)
            {
                if (bookmark.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    bookmark.Title = requestBookmark.Title;
                    bookmark.Description = requestBookmark.Description;
                    TempData["message"] = "Bookmarkul a fost modificat";
                    TempData["messageType"] = "alert-success";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui bookmark care nu va apartine";
                    TempData["messageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                requestBookmark.Categ = GetAllCategories();
                return View(requestBookmark);
            }
        }




        [HttpPost]
        [Authorize(Roles = "Editor,Admin")]
        public ActionResult Delete(int id)
        {
            Bookmark Bookmark = db.Bookmarks.Include("Comments")
                                         .Where(art => art.Id == id)
                                         .First();

            if (Bookmark.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Bookmarks.Remove(Bookmark);
                db.SaveChanges();
                TempData["message"] = "Bookmarkul a fost sters";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un bookmark care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }
        }


        private void SetAccessRights()
        {
            ViewBag.AfisareButoane = false;

            if (User.IsInRole("Editor"))
            {
                ViewBag.AfisareButoane = true;
            }

            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            var selectList = new List<SelectListItem>();

            var userId = _userManager.GetUserId(User);

            var categories = db.Categories
                .Where(cat => cat.UserId == userId)
                .ToList();

            foreach (var category in categories)
            {
                selectList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.CategoryName
                });
            }

            return selectList;
        }

        [HttpPost]
        public IActionResult Upvote(int bookmarkId)
        {
            ApplicationDbContext _context;
            _context = db;
            

            // Retrieve the bookmark from the database
            var bookmark = _context.Bookmarks.Find(bookmarkId);

            if (bookmark != null)
            {
                // Check if the user has already upvoted
                var userId = _userManager.GetUserId(User);
                var hasUpvoted = _context.Votes.Any(u => u.BookmarkId == bookmarkId && u.UserId == userId);

                if (hasUpvoted)
                {
                    // User has already upvoted, remove the upvote
                    var upvote = _context.Votes.FirstOrDefault(u => u.BookmarkId == bookmarkId && u.UserId == userId);
                    _context.Votes.Remove(upvote);
                    bookmark.Votes--; // Decrement the upvotes
                }
                else
                {
                    // User has not upvoted, add the upvote
                    var upvote = new Vote
                    {
                        BookmarkId = bookmarkId,
                        UserId = userId
                    };
                    _context.Votes.Add(upvote);
                    bookmark.Votes++; // Increment the upvotes
                }

                // Save changes to the database
                _context.SaveChanges();
            }

            //refresh the page
            return RedirectToAction("Show", new { id = bookmarkId });
        }

    }
}
