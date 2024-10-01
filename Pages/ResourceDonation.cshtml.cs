using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GiftOfTheGiversFoundation.Models;
using GiftOfTheGiversFoundation.Data;
using System;
using System.Threading.Tasks;

namespace GiftOfTheGiversFoundation.Pages
{
    public class ResourceDonationModel : PageModel
{
    private readonly ILogger<ResourceDonationModel> _logger;
    private readonly ApplicationDbContext _context;

    public ResourceDonationModel(ILogger<ResourceDonationModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new InputModel();

    public List<ResourceDonation>? Donations { get; set; } // To hold existing donations

    public class InputModel
    {
        public string DonorName { get; set; } = string.Empty;
        public string DonorContact { get; set; } = string.Empty;
        public string DonationType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public string DonationMethod { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public void OnGet()
    {
        LoadDonations();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please correct the errors in the form.";
            LoadDonations();
            return Page();
        }

        var resourceDonation = new ResourceDonation
        {
            DonorName = Input.DonorName,
            DonorContact = Input.DonorContact,
            DonationType = Input.DonationType,
            Quantity = Input.Quantity,
            Description = Input.Description,
            DonationMethod = Input.DonationMethod,
            Address = Input.Address,
            CreatedAt = DateTime.Now
        };

        try
        {
            _context.ResourceDonations.Add(resourceDonation);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Resource donation submitted successfully.";
            return RedirectToPage("/Success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while saving the resource donation.");
            TempData["ErrorMessage"] = "Failed to submit the resource donation. Please try again.";
            LoadDonations();
            return Page();
        }
    }

    private void LoadDonations()
    {
        Donations = _context.ResourceDonations
            .OrderByDescending(d => d.CreatedAt)
            .Take(10)
            .ToList();
    }
}

}