using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;

namespace PlatformaSocialBookmarking.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "UserNeinregistrat, UserInregistrat, Admin")]
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }

            var categories = _db.Categories.OrderBy(category => category.CategoryName);
            ViewBag.Categories = categories;
            return View();
        }

        public ActionResult Show(int id, string userId)
        {
            Category category = _db.Categories.Find(id);
            return View(category);
        }

        [Authorize(Roles = "UserInregistrat, Admin")]
        [HttpPost]
        public ActionResult New(Category cat)
        {
            if (ModelState.IsValid)
            {
                cat.UserId = _userManager.GetUserId(User);
                _db.Categories.Add(cat);
                _db.SaveChanges();
                TempData["message"] = "Categoria a fost adaugata";
                return RedirectToAction("Index");
            }
            else
            {
                return View(cat);
            }
        }

        [Authorize(Roles = "UserInregistrat, Admin")]
        public ActionResult Edit(int id)
        {
            Category category = _db.Categories.Find(id);

            if (category.UserId == _userManager.GetUserId(User))
            {
                return View(category);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra acestei categorii";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "UserInregistrat, Admin")]
        [HttpPost]
        public ActionResult Edit(int id, Category requestCategory)
        {
            Category category = _db.Categories.Find(id);

            if (category.UserId == _userManager.GetUserId(User))
            {
                if (ModelState.IsValid)
                {
                    category.CategoryName = requestCategory.CategoryName;
                    category.CoverUrl = requestCategory.CoverUrl;
                    _db.SaveChanges();
                    TempData["message"] = "Categoria a fost modificata!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestCategory);
                }
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra acestei categorii";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "UserInregistrat, Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Category category = _db.Categories.Find(id);

            if (category.UserId == _userManager.GetUserId(User))
            {
                _db.Categories.Remove(category);
                TempData["message"] = "Categoria a fost stearsa";

                try
                {
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    TempData["message"] = $"Eroare la stergerea categoriei: {ex.Message}";
                }

                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti aceasta categorie";
                return RedirectToAction("Index");
            }
        }
    }

}
