using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healio.Models
{
    public class PatientProfile
    {
        public int Id { get; set; }
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(1000)]
        [Column("medical_history")]
        public string? MedicalHistory { get; set; }
        [Required]
        public User User { get; set; }
    }
}
