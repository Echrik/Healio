using Healio.Models;
using Healio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
            Console.WriteLine(SelectedDate.DayOfWeek.ToString());
            return Page();
        }
    }
}
