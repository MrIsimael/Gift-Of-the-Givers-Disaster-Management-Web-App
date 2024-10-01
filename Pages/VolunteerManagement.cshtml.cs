using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GiftOfTheGiversFoundation.Models;
using GiftOfTheGiversFoundation.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiftOfTheGiversFoundation.Pages
{
    public class VolunteerManagementModel : PageModel
    {
        private readonly ILogger<VolunteerManagementModel> _logger;
        private readonly ApplicationDbContext _context;

        public VolunteerManagementModel(ILogger<VolunteerManagementModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string Skills { get; set; } = string.Empty;
            public string Availability { get; set; } = string.Empty;
            public List<string> PreferredTasks { get; set; } = new List<string>();
        }

        public void OnGet()
        {
            // Called when the page is first loaded (GET request)
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors in the form.";
                return Page();
            }

            var volunteer = new Volunteer
            {
                Name = Input.Name,
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber,
                Skills = Input.Skills,
                Availability = Input.Availability,
                PreferredTasks = string.Join(", ", Input.PreferredTasks)
            };

            try
            {
                _context.Volunteers.Add(volunteer);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Volunteer registered successfully.";
                return RedirectToPage("/Success"); // Redirect to a success page
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving the volunteer registration.");
                
                TempData["ErrorMessage"] = "Failed to register the volunteer. Please try again.";
                return Page();
            }
        }
    }
}
