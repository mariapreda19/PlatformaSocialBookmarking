using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;

namespace PlatformaSocialBookmarking.Controllers
{
    public class ImagesController : Controller
    {
        
        private readonly ApplicationDbContext db;

        public ImagesController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {

            var images = db.Images.Include("Category");

            ViewBag.Images = images;

            if(TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            return View();
        }

        public IActionResult Show(int id)
        {
            Image image = db.Images.Include("Category")
                            .Where(img => img.Id == id)
                            .First();

            return View(image);
        }

        public IActionResult New()
        {
            Image image = new Image();

            image.Categ = GetAllCategories();

            return View(image);
        }

        //adaugarea postarii in baza de date
        [HttpPost]

        public IActionResult New(Image image)
        {
            image.Date = DateTime.Now;
            image.Categ = GetAllCategories();

            if(ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                TempData["message"] = "Poza a fost adaugata cu succes";
                return RedirectToAction("Index");
            }
            else
            {
                return View(image);
            }
        }

        //editarea unei postari

        public IActionResult Edit(int id)
        {
            Image image = db.Images.Include("Category")
                                   .Where (img => img.Id == id)
                                   .First();
            image.Categ = GetAllCategories();

            return View(image);
        }

        //readaugare in baza de date dupa modificare

        [HttpPost]

        public IActionResult Edit(int id, Image requestImage)
        {
            Image image = db.Images.Find(id);
            requestImage.Categ = GetAllCategories();

            if(ModelState.IsValid)
            {
                image.Description = requestImage.Description;
                image.Url = requestImage.Url;
                image.CategoryId = requestImage.CategoryId;
                TempData["message"] = "Postarea a fost modificata";

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(requestImage);
            }
        }



        public IActionResult Delete(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            TempData["message"] = "Postarea a fost stearsa";
            return RedirectToAction("Index");
        }

        [NonAction]

        public IEnumerable<SelectListItem> GetAllCategories()
        {
            var selectList = new List<SelectListItem>();

            var categories = from cat in db.Categories
                             select cat;

            foreach (var category in categories)
            {
                selectList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.CategoryName.ToString()
                });
            }

            return selectList;
        }

        public IActionResult IndexPartialView()
        {
            return View();
        }
        
    }
}
