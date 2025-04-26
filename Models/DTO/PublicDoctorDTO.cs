namespace Healio.Models.DTO
{
    public class PublicDoctorDTO
    {
        public string Specialization { get; set; } = "";
        public string ClinicAddress { get; set; } = "";
        public void Deconstruct(out string specialization, out string clinicAddress)
        {
            specialization = Specialization;
            clinicAddress = ClinicAddress;
        }
    }
}
