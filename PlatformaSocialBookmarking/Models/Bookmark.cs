using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Bookmark
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public virtual Comment? Comment { get; set; }

        public DateTime? Date {  get; set; }

        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }


        [NotMapped]
        public IEnumerable<SelectListItem>? Categ { get; set; }

        public virtual ICollection<Image>? Images { get; set; }

        public virtual ICollection<Bookmark_has_Category>? Bookmark_Has_Category { get; set; }
    }
}
