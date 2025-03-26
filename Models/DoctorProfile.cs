namespace Healio.Models
{
    public class DoctorProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string? ClinicAddress { get; set; }
        public User User { get; set; }
    }
}
