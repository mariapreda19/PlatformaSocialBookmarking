using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;
using System.Linq;

namespace PlatformaSocialBookmarking.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "UserInregistrat, Admin")]
        public IActionResult Index(string userId)
        {
            ApplicationUser userProfile = _userManager.FindByIdAsync(userId).Result;

            if (userProfile == null)
            {
                return NotFound();
            }

            var userBookmarks = db.Bookmarks
                .Include(b => b.Bookmark_Has_Categories)
                    .ThenInclude(bhc => bhc.Category)
                .Include(b => b.User)
                .Include(b => b.Bookmark_Has_Images)
                    .ThenInclude(bhi => bhi.Image)
                .Where(b => b.Bookmark_Has_Categories.Any(bhc => bhc.Category.UserId == userId)) // Filter by user whose profile is being viewed
                .OrderBy(b => b.Date)
                .ToList();

            ViewBag.User = userProfile;

            return View(userBookmarks);
        }

    }
}