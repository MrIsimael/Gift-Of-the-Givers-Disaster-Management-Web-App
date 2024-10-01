using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GiftOfTheGiversFoundation.Models;
using GiftOfTheGiversFoundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftOfTheGiversFoundation.Pages
{
    public class IncidentReportModel : PageModel
    {
        private readonly ILogger<IncidentReportModel> _logger; // Added ILogger to log errors or information
        private readonly ApplicationDbContext _context; // Added ApplicationDbContext to access the database

        public IncidentReportModel(ILogger<IncidentReportModel> logger, ApplicationDbContext context) // Constructor injection of ILogger and ApplicationDbContext
        {
            _logger = logger; // Injected ILogger to log errors or information
            _context = context; // Injected ApplicationDbContext to access the database
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel(); // Added Input property to bind the form fields

        public List<IncidentReport>? Incidents { get; set; } // Added Incidents property to display the list of incidents

        public class InputModel // This is the model for the form fields
        {
            public string IncidentTitle { get; set; } = string.Empty; // Added IncidentTitle property
            public DateTime IncidentDateTime { get; set; } // Added IncidentDateTime property
            public string Location { get; set; } = string.Empty; // Added Location property
            public string DisasterType { get; set; } = string.Empty; // Added DisasterType property
            public string Description { get; set; } = string.Empty; // Added Description property
        }

        public void OnGet() 
        {
            LoadIncidents(); 
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors in the form.";
                LoadIncidents();
                return Page();
            }

            var incidentReport = new IncidentReport
            {
                IncidentTitle = Input.IncidentTitle,
                IncidentDateTime = Input.IncidentDateTime,
                Location = Input.Location,
                DisasterType = Input.DisasterType,
                Description = Input.Description,
                CreatedAt = DateTime.Now
            };

            try
            {
                _context.IncidentReports.Add(incidentReport);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Incident report submitted successfully.";
                return RedirectToPage("/IncidentReport");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving the incident report.");
                
                TempData["ErrorMessage"] = "Failed to submit the incident report. Please try again.";
                LoadIncidents();
                return Page();
            }
        }

        private void LoadIncidents()
        {
            Incidents = _context.IncidentReports
                .OrderByDescending(i => i.IncidentDateTime)
                .Take(10)
                .ToList();
        }
    }
}