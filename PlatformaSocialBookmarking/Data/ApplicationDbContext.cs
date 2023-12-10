using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatformaSocialBookmarking.Models;

namespace PlatformaSocialBookmarking.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Bookmark> Bookmarks { get; set; }

        public DbSet<Comment> Comments { get; set; }

    }
}