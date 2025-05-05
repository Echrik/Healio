using Healio.Models;
using Healio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Healio.Pages
{
    [Authorize(Roles = "doctor")]
    public class ReservationModel : PageModel
    {
        private readonly AppointmentService _appointmentService;

        public ReservationModel(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [BindProperty(SupportsGet = true)]
        public int PatientId { get; set; }

        public List<PatientProfile> Patients { get; set; }
        public List<Appointment> Reservations { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var doctorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Patients = _appointmentService.GetPatientsForDoctor(doctorId) ?? new List<PatientProfile>();

            if (PatientId > 0)
            {
                Reservations = _appointmentService.GetReservationsForDoctorAndPatient(doctorId, PatientId);
            }

            return Page();
        }
    }
}