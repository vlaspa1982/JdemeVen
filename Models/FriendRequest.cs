using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class FriendRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FromUserId { get; set; }

        [ForeignKey("FromUserId")]
        public User FromUser { get; set; } = null!;

        [Required]
        public int ToUserId { get; set; }

        [ForeignKey("ToUserId")]
        public User ToUser { get; set; } = null!;

        [Required]
        public DateTime RequestDate { get; set; }

        public bool IsAccepted { get; set; }
    }

}
