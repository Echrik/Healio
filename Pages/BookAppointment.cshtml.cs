using Healio.Models;
using Healio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Healio.Pages
{
    [Authorize(Roles = "patient")]
    public class BookAppointmentModel : PageModel
    {
        private readonly AppointmentService _appointmentService;
        private readonly UserService _userService;

        public BookAppointmentModel(AppointmentService appointmentService, UserService userService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
        }

        public List<User> Doctors { get; set; }
        public List<TimeSpan> AvailableTimes { get; set; }

        [BindProperty]
        public int SelectedDoctorId { get; set; }
        [BindProperty]
        public DateTime SelectedDate { get; set; }

        [BindProperty]
        public DateTime SelectedTime { get; set; }

        public void OnGet()
        {
            Doctors = _appointmentService.GetDoctors();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            PatientProfile patient = _userService.GetPatientByUserId(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DoctorProfile doctor = _userService.GetDoctorByUserId(SelectedDoctorId);
            DateTime appointmentDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second);
            _appointmentService.BookAppointment(new Appointment { AppointmentDate = appointmentDate, DoctorId = SelectedDoctorId, PatientId = patient.Id, Patient = patient, Doctor = doctor });
            return RedirectToPage("/Index");
        }
    }
}
