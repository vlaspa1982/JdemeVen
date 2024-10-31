using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class EventRating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [Required]
        [Range(1, 5)] // Hodnocení by mělo být v rozsahu 1–5
        public int Rating { get; set; }

        [MaxLength(500)] // Maximální délka komentáře
        public string? Comment { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
    
