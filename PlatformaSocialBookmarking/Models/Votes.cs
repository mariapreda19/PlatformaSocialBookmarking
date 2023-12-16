using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Votes
    {

        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? BookmarkId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual Bookmark? Bookmark { get; set; }

    }
}
