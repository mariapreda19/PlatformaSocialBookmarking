using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Bookmark_Has_Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int BookmarkId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual Bookmark? Bookmark { get; set; }
       
    }
}
