using System.ComponentModel.DataAnnotations;

namespace Healio.Models
{
    public class Log
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Action { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public User? User { get; set; }
    }
}
