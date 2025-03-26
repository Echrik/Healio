namespace Healio.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "pending"; // "pending", "confirmed", "cancelled"
        public PatientProfile Patient { get; set; } 
        public DoctorProfile Doctor { get; set; }
    }
}
