using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatformaSocialBookmarking.Models;

namespace PlatformaSocialBookmarking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Bookmark> Bookmarks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // definire primary key compus
            modelBuilder.Entity<Bookmark_has_Category>()
                .HasKey(ab => new { ab.Id, ab.BookmarkId, ab.CategoryId });


            modelBuilder.Entity<Bookmark_has_Category>()
                .HasOne(ab => ab.Bookmark)
                .WithMany(ab => ab.Bookmark_Has_Category)
                .HasForeignKey(ab => ab.BookmarkId);

            modelBuilder.Entity<Bookmark_has_Category>()
                .HasOne(ab => ab.Category)
                .WithMany(ab => ab.Bookmark_Has_Category)
                .HasForeignKey(ab => ab.CategoryId);
        }

    }
}