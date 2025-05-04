using Healio.Services;
using Microsoft.AspNetCore.Mvc;

namespace Healio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Route("Times")]
        public IActionResult GetDoctorTimes([FromBody] TimesRequestDTO TimesRequest)
        {
            var response = _appointmentService.GetAvailableTimes(TimesRequest.DoctorId, TimesRequest.Date.DayOfWeek.ToString());
            Console.WriteLine(response.Count);
            Console.WriteLine(TimesRequest.Date.DayOfWeek.ToString());
            Console.WriteLine(TimesRequest.DoctorId);
            return Ok(response);
        }

        public class TimesRequestDTO
        {
            public int DoctorId { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
