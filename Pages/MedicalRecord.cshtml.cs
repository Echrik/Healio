using Healio.Models;
using Healio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Healio.Pages
{
    [Authorize(Roles = "patient")]
    public class MedicalRecordModel : PageModel
    {
        private readonly HealthDataService _healthDataService;

        public MedicalRecordModel(HealthDataService healthDataService)
        {
            _healthDataService = healthDataService;
        }

        public List<HealthData> HealthDataList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            HealthDataList = _healthDataService.GetHealthData(userId);

            return Page();
        }
    }
}
