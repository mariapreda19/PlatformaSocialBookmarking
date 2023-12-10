using System.ComponentModel.DataAnnotations;

namespace PlatformaSocialBookmarking.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "pune ceva")]

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int? ImageId { get; set; }

        public virtual Image? Image { get; set; }

    }
}
