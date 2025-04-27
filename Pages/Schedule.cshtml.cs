using AutoMapper;
using Healio.Models;
using Healio.Models.DTO;
using Healio.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;
using System.Security.Claims;

namespace Healio.Pages
{
    public class ScheduleModel : PageModel
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public ScheduleModel(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [BindProperty]
        public List<DoctorScheduleDTO> Schedules { get; set; }
        [BindProperty]
        public List<string> DaysOfWeek { get; set; } =
            new List<string> {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"
        };

        [BindProperty]
        public string DayOfWeek { get; set; }

        [BindProperty]
        public TimeSpan StartTime { get; set; }

        [BindProperty]
        public TimeSpan EndTime { get; set; }

        public void OnGet()
        {
            var doctor = _userService.GetDoctorByUserId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (doctor == null)
            {
                RedirectToPage("/Index");
            }
            Schedules = _userService.GetSchedulesByDoctorId(doctor.Id).Select(s => _mapper.Map<DoctorScheduleDTO>(s)).ToList();
            var Days = Schedules.Select(s => s.DayOfWeek).ToList();
            DaysOfWeek = DaysOfWeek.Except(Days).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var doctor = _userService.GetDoctorByUserId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (doctor == null)
            {
                return Unauthorized();
            }

            var schedule = new DoctorSchedule
            {
                DoctorId = doctor.Id,
                DayOfWeek = DayOfWeek,
                StartTime = StartTime,
                EndTime = EndTime
            };

            _userService.AddSchedule(schedule);
            return RedirectToPage("/Schedule");
        }

        public async Task<IActionResult> OnGetDeleteSchedule(string day)
        {
            var doctor = _userService.GetDoctorByUserId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (doctor == null)
            {
                return Unauthorized();
            }
            _userService.DeleteScheduleByDay(day);
            return RedirectToPage("/Schedule");
        }
    }
}
