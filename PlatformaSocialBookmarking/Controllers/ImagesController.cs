using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace PlatformaSocialBookmarking.Controllers
{

    [Authorize]
    public class ImagesController : Controller
    {  
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ImagesController(ApplicationDbContext context,
               UserManager<ApplicationUser> userManager,
               RoleManager<IdentityRole> roleManager)
        {
            db = context;

            _roleManager = roleManager;
            
            _userManager = userManager;

        }

        [Authorize(Roles = "UserInregistrat, Admin")]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var images = db.Images.Include("User").Where(i => i.UserId == userId).ToList();

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View(images);
        }





        [Authorize(Roles = "UserInregistrat,Admin")]

        public IActionResult Show(int id)
        {
            Image image = db.Images.Include("Category")
                                   .Include("User")
                                   .Where(img => img.Id == id)
                                   .First();

            SetAccessRights();

            return View(image);
        }

        [Authorize(Roles = "UserInregistrat,Admin")]
        public IActionResult New()
        {
            Image image = new Image();

            return View(image);
        }

        //adaugarea postarii in baza de date
        [Authorize(Roles = "UserInregistrat,Admin")]
        [HttpPost]

        public IActionResult New(Image image)
        {

            image.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                TempData["message"] = "Poza a fost adaugata cu succes";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                return View(image);
            }
        }

        //editarea unei postari
        [Authorize(Roles = "UserInregistrat,Admin")]
        public IActionResult Edit(int id)
        {
            Image image = db.Images
                            .Include(b => b.User)    
                            .Where(img => img.Id == id)
                            .First();

            if (image.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(image);
            }

            return View(image);
        }

        //readaugare in baza de date dupa modificare
        [Authorize(Roles = "UserInregistrat,Admin")]
        [HttpPost]

        public IActionResult Edit(int id, Image requestImage)
        {
            Image image = db.Images.Find(id);

            if(ModelState.IsValid)
            {
                if (image.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    image.Url = requestImage.Url;
                    TempData["message"] = "Postarea a fost modificata";
                    TempData["messageType"] = "alert-success";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei postari care nu va apartine";
                    TempData["messageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(requestImage);
            }
        }


        [HttpPost]
        [Authorize(Roles = "UserInregistrat,Admin")]
        public IActionResult Delete(int id)
        {
            Image image = db.Images.Find(id);

            if (image.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Images.Remove(image);
                db.SaveChanges();
                TempData["message"] = "Postarea a fost sters";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti o postare care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }
        }

        private void SetAccessRights()
        {
            ViewBag.AfisareButoane = false;

            if (User.IsInRole("UserInregistrat"))
            {
                ViewBag.AfisareButoane = true;
            }

            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);
        }


      
        
    }
}
