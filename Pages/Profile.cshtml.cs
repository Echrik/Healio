using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Healio.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public string UserEmail { get; set; }
        public string UserRole { get; set; }

        public void OnGet()
        {
            UserEmail = User.Identity.Name; // Email a bejelentkezett felhasználótól
            UserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        }
    }
}