using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class EventImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; } = null!;

        [Required]
        [Url]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty; // URL obrázku

        [Required]
        public DateTime UploadedDate { get; set; }
    }
}
