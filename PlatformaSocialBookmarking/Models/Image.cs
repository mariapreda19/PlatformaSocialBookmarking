using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformaSocialBookmarking.Models
{
    public class Image
    { 

            [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title required")]
        [StringLength(100, ErrorMessage = "max 100")]
        [MinLength(3, ErrorMessage = "Min 3")]

        public string Description { get; set; }

        [Required(ErrorMessage = "Pune poza")]

        public string Url { get; set; }

        [Required(ErrorMessage = "Vrem sa stim cand faci ce faci")]
        public DateTime Date { get; set; }

        public int? CategoryId { get; set; }

        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public virtual Category? Category { get; set; }

        [NotMapped]

        public IEnumerable<SelectListItem>? Categ { get; set; }

    }
}
