using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using GiftOfTheGiversFoundation.Models;

namespace GiftOfTheGiversFoundation.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<UserReg> _signInManager; // Declare the SignInManager field

        public LoginModel(SignInManager<UserReg> signInManager) // Inject the SignInManager through the constructor
        {
            _signInManager = signInManager; // Initialize the SignInManager
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel(); // Initialize to a new instance

        public class InputModel // Define a nested class for the input model
        {
            public string Email { get; set; } = string.Empty; // Set default value to avoid null
            public string Password { get; set; } = string.Empty; // Set default value to avoid null
        }

        public void OnGet() { } // No need to return anything for GET requests

        public async Task<IActionResult> OnPostAsync() // Change the return type to Task<IActionResult> to handle asynchronous operations
        {
            if (!ModelState.IsValid) // Check if the model state is valid before processing the form submission
            {
                return Page(); // Return the page with validation errors
            }

            // Validating that the username and password are not empty
            if (string.IsNullOrEmpty(Input.Email) || string.IsNullOrEmpty(Input.Password))
            {
                ModelState.AddModelError(string.Empty, "Username and Password are required."); // Add error message if fields are empty
                return Page(); // Return the page with the error message
            }

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, isPersistent: false, lockoutOnFailure: false); // Attempt to sign in the user
            if (result.Succeeded) // Check if the sign-in was successful
            {
                return RedirectToPage("/Dashboard"); // Redirect to Dashboard
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Add error message if login fails
            return Page(); // Return the page with the error message
        }
    }
}
