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
            var userBookmarks = db.Bookmarks
                .Include(b => b.Bookmark_Has_Categories)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .Include(b => b.Bookmark_Has_Images)
                .ThenInclude(bhi => bhi.Image)
                .OrderBy(b => b.Date)
                .ToList();

            
            return View(userBookmarks);
        }
    }
}
