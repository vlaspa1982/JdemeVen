using System.ComponentModel.DataAnnotations;

namespace JdemeVen.Server.Models
{
    public class EventRating
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public int EventId { get; set; } // Foreign key
        public Event Event { get; set; } // Navigation property
    }
}
