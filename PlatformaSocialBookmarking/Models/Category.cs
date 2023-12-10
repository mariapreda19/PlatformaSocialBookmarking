using System.ComponentModel.DataAnnotations;

namespace PlatformaSocialBookmarking.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your application had a stroke: cjkdgskcjs")]

        public string CategoryName { get; set; }

    }
}
