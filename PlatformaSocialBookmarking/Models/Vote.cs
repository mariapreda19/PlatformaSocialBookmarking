using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Vote
    {

        public int Id { get; set; }

        public string? UserId { get; set; }

        public int? BookmarkId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual Bookmark? Bookmark { get; set; }

    }
}
