using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JdemeVen.Server.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public ICollection<FriendRequest> FriendRequestsSent { get; set; } = new List<FriendRequest>(); // Odeslané požadavky
        public ICollection<FriendRequest> FriendRequestsReceived { get; set; } = new List<FriendRequest>(); // Přijaté požadavky

        // Navigation properties
        public ICollection<Event> SubmittedEvents { get; set; }
        public ICollection<FriendRequest> FriendRequests { get; set; }
        public ICollection<FriendRequest> Friends { get; set; }
    }
}
