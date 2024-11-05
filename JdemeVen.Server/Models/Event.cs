using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JdemeVen.Server.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        public bool IsPromoted { get; set; }

        public int OrganizerId { get; set; }

        public User Organizer { get; set; }

        public int EventTypeId { get; set; } // Nová vlastnost pro typ události
        public EventType EventType { get; set; } // Navigační vlastnost

        public ICollection<EventImage> Images { get; set; }
        public ICollection<EventRating> Ratings { get; set; }
        public ICollection<EventInvitation> InvitedGuests { get; set; }
    }
}
