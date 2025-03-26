using System.ComponentModel.DataAnnotations;

namespace Healio.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        [StringLength(10)]
        public string DayOfWeek { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public DoctorProfile Doctor { get; set; }
    }
}

