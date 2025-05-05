using Healio.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Buffers.Text;
using System.Security.Claims;

namespace Healio.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userService.AuthenticateUserAsync(Email, Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty,"Érvénytelen email vagy jelszó!");
                return Page();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            AuthenticationProperties authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);


            return RedirectToPage("/Index");
        }

        public void OnGet()
        {
        }
    }
}
