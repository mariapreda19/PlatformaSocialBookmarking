using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Bookmark_Has_Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ImageId { get; set; }
        public int? BookmarkId { get; set; }

        public virtual Image? Image { get; set; }
        public virtual Bookmark? Bookmark { get; set; }

    }
}
