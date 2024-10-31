using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        // Pro přihlášení přes sociální sítě
        public string? FacebookId { get; set; }
        public string? GoogleId { get; set; }

        // Přihlášení heslem
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(255)]
        public string PasswordSalt { get; set; } = string.Empty;

        // Přátelé
        public List<User> Friends { get; set; } = new List<User>();

        // Události vytvořené tímto uživatelem
        public List<Event> SubmittedEvents { get; set; } = new List<Event>();

        // Události, na které byl uživatel pozván
        public List<Event> InvitedToEvents { get; set; } = new List<Event>();
    }

}
