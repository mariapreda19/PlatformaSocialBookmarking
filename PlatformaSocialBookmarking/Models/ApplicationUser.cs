using Microsoft.AspNetCore.Identity;
using PlatformaSocialBookmarking.Models;


namespace PlatformaSocialBookmarking.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Comment>? Comments { get; set; }

        public virtual ICollection<Image>? Images { get; set; }

        public virtual ICollection<Bookmark>? Bookmarks { get; set; }

    }
}
