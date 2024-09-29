using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using GiftOfTheGiversFoundation.Models;

namespace GiftOfTheGiversFoundation.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<UserReg> _signInManager;

        public LoginModel(SignInManager<UserReg> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel(); // Initialize to a new instance

        public class InputModel
        {
            public string Username { get; set; } = string.Empty; // Set default value to avoid null
            public string Password { get; set; } = string.Empty; // Set default value to avoid null
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validate that the username and password are not empty
            if (string.IsNullOrEmpty(Input.Username) || string.IsNullOrEmpty(Input.Password))
            {
                ModelState.AddModelError(string.Empty, "Username and Password are required.");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToPage("/Index"); // Redirect to home or desired page
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
