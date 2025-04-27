namespace Healio.Models.DTO
{
    public class DoctorScheduleDTO
    {
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
