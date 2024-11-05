using System.ComponentModel.DataAnnotations;

namespace JdemeVen.Server.Models
{
    public class EventInvitation
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; } // Foreign key
        public Event Event { get; set; } // Navigation property

        public int UserId { get; set; } // Foreign key for User
        public User User { get; set; } // Navigation property
    }
}
