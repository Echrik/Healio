using System.ComponentModel.DataAnnotations;

namespace Healio.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; } = "pending"; // "pending", "confirmed", "cancelled"
        [Required]
        public PatientProfile Patient { get; set; }
        [Required]
        public DoctorProfile Doctor { get; set; }
    }
}
