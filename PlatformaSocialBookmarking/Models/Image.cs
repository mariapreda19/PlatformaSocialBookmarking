using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Image
    { 

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your application had a stroke: cjkdgskcjs")]

        public string Url { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]

        public virtual ApplicationUser? User { get; set; }

        [NotMapped]
        public virtual ICollection<Bookmark_Has_Image>? Bookmark_Has_Images { get; set; }
    }
}
