using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Image
    { 

        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public int? BookmarkId { get; set; }

        public virtual Bookmark? Bookmark { get; set; }

    }
}
