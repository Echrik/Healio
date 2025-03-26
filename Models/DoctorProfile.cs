using System.ComponentModel.DataAnnotations;

namespace Healio.Models
{
    public class DoctorProfile
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Specialization { get; set; }
        [StringLength(200)]
        public string? ClinicAddress { get; set; }
        [Required]
        public User User { get; set; }
    }
}

