using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GiftOfTheGiversFoundation.Models;
using System.Threading.Tasks;

namespace GiftOfTheGiversFoundation.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<UserReg> _userManager;
        private readonly SignInManager<UserReg> _signInManager; // Add SignInManager for automatic login

        public RegisterModel(UserManager<UserReg> userManager, SignInManager<UserReg> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel(); // Initialize to a new instance

        public class InputModel
        {
            public string FullName { get; set; } = string.Empty; // Set default value to avoid null
            public string Password { get; set; } = string.Empty; // Set default value to avoid null
            public string Email { get; set; } = string.Empty; // Set default value to avoid null
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return page if the model state is invalid
            }

            var user = new UserReg
            {
                FullName = Input.FullName,
                Email = Input.Email,
                UserName = Input.Email
            };

            // Create user in the database
            var result = await _userManager.CreateAsync(user, Input.Password);
            
            if (result.Succeeded)
            {
                // Optional: Sign in the user after registration
                // await _signInManager.SignInAsync(user, isPersistent: false);

                // Redirect to Login page after successful registration
                return RedirectToPage("/Login");
            }

            // Display errors if user creation failed
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page(); // Return the same page if there's an error
        }
    }
}
