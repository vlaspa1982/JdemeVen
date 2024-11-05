using System.ComponentModel.DataAnnotations;
namespace JdemeVen.Server.Models
{

    public class FriendRequest
    {
        [Key]
        public int Id { get; set; }

        public int FromUserId { get; set; }
        public User FromUser { get; set; }

        public int ToUserId { get; set; }
        public User ToUser { get; set; }

        public DateTime RequestDate { get; set; }
        public bool IsAccepted { get; set; }
    }

}