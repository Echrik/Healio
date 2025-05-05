using Healio.Models;
using Healio.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Healio.Pages
{
    public class HealthDataCheckoutModel : PageModel
    {
        private readonly AppointmentService _appointmentService;

        public HealthDataCheckoutModel(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [BindProperty]
        public string DataType { get; set; }

        [BindProperty]
        public string Value { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Appointment = _appointmentService.GetAppointmentById(AppointmentId);

            if (Appointment == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var appointment = _appointmentService.GetAppointmentById(AppointmentId);

            if (appointment == null)
            {
                return NotFound();
            }

            _appointmentService.RecordHealthData(new HealthData
            {
                PatientId = appointment.PatientId,
                DataType = DataType,
                Value = Value,
                Patient = appointment.Patient
            });

            _appointmentService.CloseAppointment(appointment.Id);

            return RedirectToPage("/Reservation");
        }
    }
}
