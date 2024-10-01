using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GiftOfTheGiversFoundation.Models;
using GiftOfTheGiversFoundation.Data;

namespace GiftOfTheGiversFoundation.Pages
{
    public class VolunteerManagementModel : PageModel
    {
        private readonly ILogger<VolunteerManagementModel> _logger; // Logger for logging errors or information
        private readonly ApplicationDbContext _context; // Database context for accessing the database

        public VolunteerManagementModel(ILogger<VolunteerManagementModel> logger, ApplicationDbContext context) // Constructor for the class, injecting the logger and database context
        {
            _logger = logger; // Assigning the injected logger to the private field\_logger
            _context = context; // Assigning the injected database context to the private field\_context
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel(); // BindProperty attribute for binding the form data to the Input property of the class
        public List<Volunteer>? Volunteers { get; set; } // List of volunteers to be displayed on the page

        public class InputModel // Input model for the form data
        {
            public string Name { get; set; } = string.Empty; // Name of the volunteer
            public string Email { get; set; } = string.Empty; // Email of the volunteer
            public string PhoneNumber { get; set; } = string.Empty; // Phone number of the volunteer
            public string Skills { get; set; } = string.Empty; // Skills of the volunteer
            public string Availability { get; set; } = string.Empty; // Availability of the volunteer
            public string PreferredTasks { get; set; } = string.Empty; // Preferred tasks of the volunteer
        }

        public void OnGet() // Handler for GET requests to the page, loads the list of volunteers from the database
        {
            LoadVolunteers(); // Calling the LoadVolunteers method to load the list of volunteers from the database
        }

        public async Task<IActionResult> OnPostAsync() // Handler for POST requests to the page, saves the volunteer registration data to the database
        {
            if (!ModelState.IsValid) // Checking if the form data is valid, if not, returning the page with the validation errors
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage); // Getting the validation errors from the ModelState dictionary
                TempData["ErrorMessage"] = "Please correct the following errors: " + string.Join(", ", errors); // Adding the validation errors to the TempData dictionary to be displayed on the page
                return Page(); // Returning the page with the validation errors
            }

            var volunteer = new Volunteer // Creating a new Volunteer object with the form data
            {
                Name = Input.Name, // Assigning the Name property of the Volunteer object with the Name property of the Input object
                Email = Input.Email, // Assigning the Email property of the Volunteer object with the Email property of the Input object
                PhoneNumber = Input.PhoneNumber, // Assigning the PhoneNumber property of the Volunteer object with the PhoneNumber property of the Input object
                Skills = Input.Skills, // Assigning the Skills property of the Volunteer object with the Skills property of the Input object
                Availability = Input.Availability, // Assigning the Availability property of the Volunteer object with the Availability property of the Input object
                PreferredTasks = Input.PreferredTasks, // Assigning the PreferredTasks property of the Volunteer object with the PreferredTasks property of the Input object
                CreatedAt = DateTime.UtcNow // Assigning the CreatedAt property of the Volunteer object with the current UTC time
            };

            try // Try-catch block for handling any exceptions that may occur while saving the volunteer registration data to the database
            {
                _context.Volunteers.Add(volunteer); // Adding the Volunteer object to the Volunteers DbSet of the database context
                await _context.SaveChangesAsync(); // Saving the changes to the database asynchronously

                TempData["SuccessMessage"] = "Volunteer registered successfully."; // Adding a success message to the TempData dictionary to be displayed on the page
                return RedirectToPage("/Success"); // Redirecting the user to the Success page
            }
            catch (Exception ex) // Catch block for handling any exceptions that may occur while saving the volunteer registration data to the database
            {
                _logger.LogError(ex, "Error occurred while saving the volunteer registration."); // Logging the error message and the exception stack trace to the logger
                TempData["ErrorMessage"] = $"Failed to register the volunteer. Error: {ex.Message}"; // Adding an error message to the TempData dictionary to be displayed on the page
                if (ex.InnerException != null) // Checking if there is an inner exception, if yes, adding it to the error message
                {
                    TempData["ErrorMessage"] += $" Inner exception: {ex.InnerException.Message}"; // Adding the inner exception message to the error message
                }
                return Page(); // Returning the page with the error message
            }
        }

        private void LoadVolunteers() // Method for loading the list of volunteers from the database
        {
            Volunteers = _context.Volunteers // Assigning the Volunteers property of the PageModel with the Volunteers DbSet of the database context
                .OrderByDescending(v => v.CreatedAt) // Ordering the volunteers by the CreatedAt property in descending order, so that the latest volunteers are displayed first
                .Take(10) // Taking only the first 10 volunteers, to a Taking only the first 10 volunteers, to avoid loading too many records at once
                .ToList(); // Converting the IQueryable to a List of Volunteer objects
        }
    }
}
