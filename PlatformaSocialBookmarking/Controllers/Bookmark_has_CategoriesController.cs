using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;

public class Bookmark_Has_CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public Bookmark_Has_CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult SelectCategory(int id)
    {
        // Retrieve the bookmark by id
        var bookmark = _context.Bookmarks.FirstOrDefault(b => b.Id == id);

        if (bookmark == null)
        {
            // Handle the case where the bookmark is not found
            return NotFound();
        }

        // Retrieve the list of categories
        var categories = _context.Categories.ToList();

        // Set up ViewBag for the view
        ViewBag.BookmarkId = bookmark.Id;
        ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

        return View();
    }

    [HttpPost]
    public IActionResult Save(int bookmarkId, int selectedCategory)
    {
        // Check if the association already exists
        var existingAssociation = _context.Bookmark_Has_Categories
            .FirstOrDefault(bhc => bhc.BookmarkId == bookmarkId && bhc.CategoryId == selectedCategory);

       
            // Create a new association
            var newAssociation = new Bookmark_Has_Category
            {
                BookmarkId = bookmarkId,
                CategoryId = selectedCategory
            };

            _context.Bookmark_Has_Categories.Add(newAssociation);
            _context.SaveChanges();
        

        return RedirectToAction("Index", "Bookmarks");
    }

}
