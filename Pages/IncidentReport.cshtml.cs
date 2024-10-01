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
        private readonly ILogger<IncidentReportModel> _logger;
        private readonly ApplicationDbContext _context;

        public IncidentReportModel(ILogger<IncidentReportModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public List<IncidentReport>? Incidents { get; set; }

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