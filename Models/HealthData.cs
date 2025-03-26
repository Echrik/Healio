namespace Healio.Models
{
    public class HealthData
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string DataType { get; set; } // Pl. "Blood Pressure", "Blood Sugar"
        public string Value { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.Now;
        public PatientProfile Patient { get; set; }
    }
}
