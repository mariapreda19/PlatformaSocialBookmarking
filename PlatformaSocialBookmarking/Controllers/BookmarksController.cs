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
            var bookmarks = db.Bookmarks.Include(b => b.Bookmark_Has_Categories)
                                        .Include(b => b.User)
                                        .Include(b => b.Bookmark_Has_Images)
                                            .ThenInclude(bhi => bhi.Image)
                                        .OrderBy(b => b.Date)
                                        .ToList();

            ViewBag.Bookmarks = bookmarks;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View();
        }


        [Authorize(Roles = "User, Editor, Admin")]
        public IActionResult Show(int id)
        {
            Bookmark bookmark = db.Bookmarks.Include("Category")
                                            .Include("User")
                                            .Include("Image")
                                            .Include("Comments.User")
                                            .Where(b => b.Id == id)
                                            .First();
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

        public IActionResult Show([FromForm] Comment comment)
        {
            comment.Date = DateTime.Now;
            comment.UserId = _userManager.GetUserId(User);

            if(ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Redirect("/Bookmarks/Show/" + comment.BookmarkId);
            }

            else
            {
                Bookmark bkm = db.Bookmarks.Include("Category")
                                           .Include("User")
                                           .Include("Image")
                                           .Include("Comments")
                                           .Include("Comments.User")
                                           .Where(bkm => bkm.Id == comment.BookmarkId)
                                           .First();
                ViewBag.UserBookmarks = db.Categories
                                         .Where(b => b.UserId == _userManager.GetUserId(User))
                                         .ToList();


                SetAccessRights();

                return View(bkm);
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



    }
}
