using Healio.Models;
using Healio.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Healio.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public RegisterModel(UserService userService)
        {
            _userService = userService;
            User = new User();
            Password = "";
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _userService.RegisterUserAsync(User, Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "User with this email already exists.");
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
