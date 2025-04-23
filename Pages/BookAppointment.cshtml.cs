using Healio.Models;
using Healio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Healio.Pages
{
    [Authorize(Roles = "patient")]
    public class BookAppointmentModel : PageModel
    {
        private readonly AppointmentService _appointmentService;

        public BookAppointmentModel(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public List<DoctorProfile> Doctors { get; set; }
        public List<DateTime> AvailableTimes { get; set; }

        [BindProperty]
        public int SelectedDoctorId { get; set; }

        [BindProperty]
        public DateTime SelectedDate { get; set; }

        [BindProperty]
        public DateTime SelectedTime { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch the list of doctors
            Doctors = await _appointmentService.GetDoctorsAsync();
        }
    }
}
