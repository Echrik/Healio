using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Healio.Pages
{
    [Authorize(Roles = "patient")]
    public class BookAppointmentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
