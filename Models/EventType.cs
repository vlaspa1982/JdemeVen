using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;  // Např. "Kultura", "Sport", atd.

        // Kolekce událostí tohoto typu, pokud je relace potřeba
        public ICollection<Event>? Events { get; set; }
    }
}
