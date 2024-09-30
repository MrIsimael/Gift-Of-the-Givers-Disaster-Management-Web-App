using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GiftOfTheGiversFoundation.Models;
using GiftOfTheGiversFoundation.Data; // Add this line to import ApplicationDbContext
using System.Threading.Tasks;

namespace GiftOfTheGiversFoundation.Pages
{
    public class IncidentReportModel : PageModel
    {
        private readonly ILogger<IncidentReportModel> _logger;
        private readonly ApplicationDbContext _context; // Database context

        public IncidentReportModel(ILogger<IncidentReportModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Bind the form input to this property
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            public string IncidentTitle { get; set; } = string.Empty;
            public DateTime IncidentDateTime { get; set; }
            public string Location { get; set; } = string.Empty;
            public string DisasterType { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            // Called when the page is first loaded (GET request)
        }

        public async Task<IActionResult> OnPostAsync()
{
    // Validate the form input
    if (!ModelState.IsValid)
    {
        TempData["ErrorMessage"] = "Please correct the errors in the form.";
        return Page(); // Return the page if the form is invalid
    }

    // Create a new IncidentReport entity
    var incidentReport = new IncidentReport
    {
        IncidentTitle = Input.IncidentTitle,
        IncidentDateTime = Input.IncidentDateTime,
        Location = Input.Location,
        DisasterType = Input.DisasterType,
        Description = Input.Description,
        CreatedAt = DateTime.Now // Automatically set the created date
    };

    try
    {
        // Save the report to the database
        _context.IncidentReports.Add(incidentReport);
        await _context.SaveChangesAsync();

        // Set success message
        TempData["SuccessMessage"] = "Incident report submitted successfully.";
        return RedirectToPage("/Success"); // Redirect to a success page
    }
    catch (Exception ex)
    {
        // Log the error if needed
        _logger.LogError(ex, "Error occurred while saving the incident report.");
        
        // Set failure message
        TempData["ErrorMessage"] = "Failed to submit the incident report. Please try again.";
        return Page(); // Return to the same page
    }
}

    }
}
