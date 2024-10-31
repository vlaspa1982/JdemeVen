using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class EventInvitation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; } = null!;

        [Required]
        public int InvitedUserId { get; set; }

        [ForeignKey("InvitedUserId")]
        public User InvitedUser { get; set; } = null!;

        [Required]
        public DateTime InvitationDate { get; set; }

        public bool IsAccepted { get; set; }
    }

}
