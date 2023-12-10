using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Bookmark
    {
        [Key]
        public int Id { get; set; }

        public virtual Comment? Comment { get; set; }

        public int? Votes { get; set; }


        public int? CategoryId { get; set; }

        //public int? UserId { get; set; }

        public virtual Category? Category { get; set; }

        [NotMapped]

        public IEnumerable<SelectListItem>? Categ { get; set; }
    }
}
