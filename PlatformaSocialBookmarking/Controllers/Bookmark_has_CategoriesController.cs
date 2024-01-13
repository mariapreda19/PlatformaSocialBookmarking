using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;

public class Bookmark_Has_CategoriesController : Controller
{
    private readonly ApplicationDbContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    public Bookmark_Has_CategoriesController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
    {
        _db = context;

        _userManager = userManager;

        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult SelectCategory(int id)
    {
        var bookmark = _db.Bookmarks.FirstOrDefault(b => b.Id == id);

        if (bookmark == null)
        {
            return NotFound();
        }

        // toate categoriile adaugate de utilizatorul care incearca sa faca save-ul
        var userId = _userManager.GetUserId(User);
        var categories = _db.Categories.Where(c => c.User.Id == userId).ToList();

        //viewbagul
        ViewBag.BookmarkId = bookmark.Id;
        ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

        return View();
    }

    [HttpPost]
    public IActionResult Save(int bookmarkId, int selectedCategory)
    {

        
        var existingAssociation = _db.Bookmark_Has_Categories
            .FirstOrDefault(bhc => bhc.BookmarkId == bookmarkId && bhc.CategoryId == selectedCategory);

       
            // o noua inregistrare in tabelul asociativ
            var newAssociation = new Bookmark_Has_Category
            {
                BookmarkId = bookmarkId,
                CategoryId = selectedCategory
            };

            _db.Bookmark_Has_Categories.Add(newAssociation);
            _db.SaveChanges();
        

        return RedirectToAction("Index", "Bookmarks");
    }

}
