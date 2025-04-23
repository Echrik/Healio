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
            UserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            UserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value == "patient" ? "Páciens" : "Orvos";
        }
    }
}