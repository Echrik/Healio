using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healio.Models
{
    public class DoctorProfile
    {
        public int Id { get; set; }
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Specialization { get; set; }
        [StringLength(200)]
        [Column("clinic_address")]
        public string? ClinicAddress { get; set; }
        [Required]
        public User User { get; set; }
    }
}

