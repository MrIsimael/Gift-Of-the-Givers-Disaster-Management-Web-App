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

        public void OnGet()  // This method is called when the page is loaded for the first time
        {
            LoadIncidents(); // Called LoadIncidents method to load the list of incidents
        }

        public async Task<IActionResult> OnPostAsync() // This method is called when the form is submitted
        {
            if (!ModelState.IsValid) // Check if the form is valid or not
            {
                TempData["ErrorMessage"] = "Please correct the errors in the form."; // If the form is not valid, then show the error message
                LoadIncidents(); // Called LoadIncidents method to load the list of incidents
                return Page(); // Return the page with the error message
            }

            var incidentReport = new IncidentReport // Create a new instance of IncidentReport model
            {
                IncidentTitle = Input.IncidentTitle, // Set the IncidentTitle property of IncidentReport model with the value of IncidentTitle property of Input model
                IncidentDateTime = Input.IncidentDateTime, // Set the IncidentDateTime property of IncidentReport model with the value of IncidentDateTime property of Input model
                Location = Input.Location, // Set the Location property of IncidentReport model with the value of Location property of Input model
                DisasterType = Input.DisasterType, // Set the DisasterType property of IncidentReport model with the value of DisasterType property of Input model
                Description = Input.Description, // Set the Description property of IncidentReport model with the value of Description property of Input model
                CreatedAt = DateTime.Now // Set the CreatedAt property of IncidentReport model with the current date and time
            };

            try
            {
                _context.IncidentReports.Add(incidentReport); // Add the incident report to the database
                await _context.SaveChangesAsync(); // Save the changes to the database

                TempData["SuccessMessage"] = "Incident report submitted successfully."; // Show the success message
                return RedirectToPage("/IncidentReport"); // Redirect to the IncidentReport page
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving the incident report."); // Log the error message
                
                TempData["ErrorMessage"] = "Failed to submit the incident report. Please try again."; // Show the error message
                LoadIncidents(); // Called LoadIncidents method to load the list of incidents
                return Page(); // Return the page with the error message
            }
        }

        private void LoadIncidents() // This method is used to load the list of incidents
        {
            Incidents = _context.IncidentReports // Get the list of incidents from the database
                .OrderByDescending(i => i.IncidentDateTime) // Order the list of incidents by IncidentDateTime property in descending order
                .Take(10) // Take only the first 10 incidents
                .ToList(); // Convert the list of incidents to a list of IncidentReport model
        }
    }
}