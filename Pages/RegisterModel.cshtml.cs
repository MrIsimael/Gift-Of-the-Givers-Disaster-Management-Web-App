using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GiftOfTheGiversFoundation.Models;
using System.Threading.Tasks;

namespace GiftOfTheGiversFoundation.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<UserReg> _userManager; // Add UserManager for user management
        private readonly SignInManager<UserReg> _signInManager; // Add SignInManager for automatic login

        public RegisterModel(UserManager<UserReg> userManager, SignInManager<UserReg> signInManager) // Add SignInManager to the constructor
        {
            _userManager = userManager; // Initialize UserManager
            _signInManager = signInManager; // Initialize SignInManager
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel(); // Initialize to a new instance

        public class InputModel // Define InputModel class inside RegisterModel class
        {
            public string FullName { get; set; } = string.Empty; // Set default value to avoid null
            public string Password { get; set; } = string.Empty; // Set default value to avoid null
            public string Email { get; set; } = string.Empty; // Set default value to avoid null
        }
 
        public void OnGet() { } // No need to return a view for GET request

        public async Task<IActionResult> OnPostAsync() // Change return type to IActionResult to handle redirection
        {
            if (!ModelState.IsValid) // Check if the model state is valid before processing the form submission
            {
                return Page(); // Return page if the model state is invalid
            }

            var user = new UserReg // Create a new UserReg object
            {
                FullName = Input.FullName, // Use Input.FullName instead of FullName
                Email = Input.Email, // Use Input.Email instead of Email
                UserName = Input.Email // Use Input.Email as UserName
            };

            // Create user in the database
            var result = await _userManager.CreateAsync(user, Input.Password); // Use Input.Password instead of Password
            
            if (result.Succeeded) // Check if user creation was successful
            {

                // Redirect to Login page after successful registration
                return RedirectToPage("/Login");
            }

            // Display errors if user creation failed
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description); // Add errors to the model state
            }

            return Page(); // Return the same page if there's an error
        }
    }
}
