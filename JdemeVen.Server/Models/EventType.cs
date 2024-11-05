using System.ComponentModel.DataAnnotations;

namespace JdemeVen.Server.Models
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}