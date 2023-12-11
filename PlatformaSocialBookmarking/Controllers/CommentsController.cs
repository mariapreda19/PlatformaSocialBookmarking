using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlatformaSocialBookmarking.Data;
using PlatformaSocialBookmarking.Models;

namespace PlatformaSocialBookmarking.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;

        public CommentsController(ApplicationDbContext context)
        {
            db = context;
        }


        [HttpPost]
        public IActionResult New(Comment comm)
        {
            comm.Date = DateTime.Now;
            if(ModelState.IsValid)
            {
                db.Comments.Add(comm);
                db.SaveChanges();
                return Redirect("/Bookmarks/Show" +  comm.BookmarkId);
            }
            else
            {
                return Redirect("/Bookmarks/Show" + comm.BookmarkId);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Comment comm = db.Comments.Find(id);
            db.Comments.Remove(comm);
            db.SaveChanges();
            return Redirect("/Bookmarks/Show" + comm.BookmarkId);
        }


        public IActionResult Edit(int id)
        {
            Comment comm = db.Comments.Find(id);

            return View(comm);
        }

        [HttpPost]
        public IActionResult Edit(int id, Comment requestComment)
        {
            Comment comm = db.Comments.Find(id);

            if (ModelState.IsValid)
            {
                comm.Content = requestComment.Content;
                db.SaveChanges();
                return Redirect("/Bookmarks/Show" + comm.BookmarkId);
            }
            else
            {
                return View(requestComment);
            }
        }
    }
}
