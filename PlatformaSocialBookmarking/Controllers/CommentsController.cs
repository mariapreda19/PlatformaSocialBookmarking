using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;
using Microsoft.EntityFrameworkCore;


namespace PlatformaSocialBookmarking.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public CommentsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }


        [HttpPost]
        public IActionResult New(Comment comm)
        {
            comm.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comm);
                db.SaveChanges();
                return Redirect("/Bookmarks/Show" + comm.BookmarkId);
            }
            else
            {
                return Redirect("/Bookmarks/Show" + comm.BookmarkId);
            }
        }

        [Authorize(Roles = "UserInregistrat,Admin")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Comment comm = db.Comments.Find(id);

            if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Comments.Remove(comm);
                db.SaveChanges();
                return Redirect("/Bookmarks/Show/" + comm.BookmarkId);
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti comentariul";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Bookmarks");
            }
        }

        [Authorize(Roles = "UserInregistrat,Admin")]
        public IActionResult Edit(int id)
        {
            Comment comm = db.Comments.Find(id);

            if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    // If it's an AJAX request, return a partial view for inline editing
                    return PartialView("_EditCommentPartial", comm);
                }

                return View(comm);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa editati comentariul";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Bookmarks");
            }
        }


        [HttpPost]
        [Authorize(Roles = "UserInregistrat, Admin")]
        public IActionResult Edit(int id, Comment requestComment)
        {
            Comment comm = db.Comments.Find(id);

            if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {

                if (ModelState.IsValid)
                {
                    comm.Content = requestComment.Content;
                    db.SaveChanges();
                    return Redirect("/Bookmarks/Show/" + comm.BookmarkId);
                }
                else
                {
                    return View(requestComment);
                }
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Bookmarks");
            }
        }

        [HttpPost]
        public IActionResult UpdateComment(int commentId, string editedCommentContent)
        {
            // Update the comment in your data store
            Comment comm = db.Comments.Find(commentId);
            comm.Content = editedCommentContent;
            db.SaveChanges();

            // Return the updated comment as a partial view
            return RedirectToAction("Index", "Bookmarks");
        }

        [HttpGet]
        public IActionResult ReloadComment(int commentId)
        {
            // Reload the original comment content
            Comment comm = db.Comments.Find(commentId);

            // Return to the bookmark show page
            return RedirectToAction("Index", "Bookmarks");
        }

    }
}