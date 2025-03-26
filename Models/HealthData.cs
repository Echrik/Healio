using System.ComponentModel.DataAnnotations;

namespace Healio.Models
{
    public class HealthData
    {
        public int Id { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        [StringLength(100)]
        public string DataType { get; set; }
        [Required]
        [StringLength(500)]
        public string Value { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.Now;
        [Required]
        public PatientProfile Patient { get; set; }
    }
}
