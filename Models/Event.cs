using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        // Cizí klíč pro EventType
        [Required]
        public int EventTypeId { get; set; }

        [ForeignKey("EventTypeId")]
        public EventType Type { get; set; } = null!;

        // Propagace
        public bool IsPromoted { get; set; }

        // Datum konce propagace, pokud je událost propagována
        public DateTime? PromotionEndDate { get; set; }

        [MaxLength(100)]
        public string? OrganizerContact { get; set; }

        // Organizátor události
        [Required]
        public int OrganizerId { get; set; }

        [ForeignKey("OrganizerId")]
        public User Organizer { get; set; } = null!;

        // Pozvaní uživatelé (sociální síťová část)
        public List<EventInvitation> InvitedGuests { get; set; } = new List<EventInvitation>();

        // Obrázky připojené k události
        public List<EventImage> Images { get; set; } = new List<EventImage>();

        // Hodnocení události
        public List<EventRating> Ratings { get; set; } = new List<EventRating>();
    }
}
