using System.ComponentModel.DataAnnotations;

namespace JdemeVen.Server.Models
{
    public class EventImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public int EventId { get; set; } // Foreign key
        public Event Event { get; set; } // Navigation property
    }
}
