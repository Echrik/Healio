namespace Healio.Models.DTO
{
    public class PublicPatientDTO
    {
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string MedicalHistory { get; set; } = "";

        public void Deconstruct(out DateTime dateOfBirth, out string medicalHistory)
        {
            dateOfBirth = DateOfBirth;
            medicalHistory = MedicalHistory;
        }
    }
}
