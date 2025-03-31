using System.ComponentModel.DataAnnotations;

namespace Healio.Models
{
    public class PatientProfile
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(1000)]
        public string? MedicalHistory { get; set; }
        [Required]
        public User User { get; set; }
    }
}
