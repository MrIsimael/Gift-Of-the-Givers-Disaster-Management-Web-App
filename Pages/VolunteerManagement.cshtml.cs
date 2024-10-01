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
        private readonly ILogger<VolunteerManagementModel> _logger;
        private readonly ApplicationDbContext _context;

        public VolunteerManagementModel(ILogger<VolunteerManagementModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();
        public List<Volunteer>? Volunteers { get; set; }

        public class InputModel
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string Skills { get; set; } = string.Empty;
            public string Availability { get; set; } = string.Empty;
            public string PreferredTasks { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            LoadVolunteers();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Please correct the following errors: " + string.Join(", ", errors);
                return Page();
            }

            var volunteer = new Volunteer
            {
                Name = Input.Name,
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber,
                Skills = Input.Skills,
                Availability = Input.Availability,
                PreferredTasks = Input.PreferredTasks,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                _context.Volunteers.Add(volunteer);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Volunteer registered successfully.";
                return RedirectToPage("/Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving the volunteer registration.");
                TempData["ErrorMessage"] = $"Failed to register the volunteer. Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    TempData["ErrorMessage"] += $" Inner exception: {ex.InnerException.Message}";
                }
                return Page();
            }
        }

        private void LoadVolunteers()
        {
            Volunteers = _context.Volunteers
                .OrderByDescending(v => v.CreatedAt)
                .Take(10)
                .ToList();
        }
    }
}
