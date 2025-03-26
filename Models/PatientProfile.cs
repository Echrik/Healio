namespace Healio.Models
{
    public class PatientProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? MedicalHistory { get; set; }
        public User User { get; set; }
    }
}
