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

        public DbSet<Bookmark_Has_Image> Bookmark_Has_Images { get; set; }

        public DbSet<Bookmark_Has_Category> Bookmark_Has_Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // definire primary key compus
            modelBuilder.Entity<Bookmark_Has_Category>()
                .HasKey(ab => new { ab.Id, ab.BookmarkId, ab.CategoryId });


            modelBuilder.Entity<Bookmark_Has_Category>()
                .HasOne(ab => ab.Bookmark)
                .WithMany(ab => ab.Bookmark_Has_Categories)
                .HasForeignKey(ab => ab.BookmarkId);

            modelBuilder.Entity<Bookmark_Has_Category>()
                .HasOne(ab => ab.Category)
                .WithMany(ab => ab.Bookmark_Has_Categories)
                .HasForeignKey(ab => ab.CategoryId);


            modelBuilder.Entity<Bookmark_Has_Image>()
           .HasKey(bhi => new { bhi.Id, bhi.BookmarkId, bhi.ImageId });

            modelBuilder.Entity<Bookmark_Has_Image>()
                .HasOne(bhi => bhi.Bookmark)
                .WithMany(b => b.Bookmark_Has_Images)
                .HasForeignKey(bhi => bhi.BookmarkId);

            modelBuilder.Entity<Bookmark_Has_Image>()
                .HasOne(bhi => bhi.Image)
                .WithMany(i => i.Bookmark_Has_Images)
                .HasForeignKey(bhi => bhi.ImageId);
        }

    }
}