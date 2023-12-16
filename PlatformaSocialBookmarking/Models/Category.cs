using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PlatformaSocialBookmarking.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your application had a stroke: cjkdgskcjs")]

        public string CategoryName { get; set; }

        //pentru fiecare user, se vor afisa in profil bookmarkurile din categoriile proprii (create de el sau salvate)
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public string CoverUrl { get; set; }

        [NotMapped]

        public virtual ICollection<Bookmark_has_Category>? Bookmark_Has_Category { get; set; }

    }
}
