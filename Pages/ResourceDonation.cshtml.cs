using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages; // Add this line for Razor Pages
using Microsoft.Extensions.Logging;
using GiftOfTheGiversFoundation.Models; // Add this line for the ResourceDonation model
using GiftOfTheGiversFoundation.Data; // Add this line for the database context
using System;
using System.Threading.Tasks;

namespace GiftOfTheGiversFoundation.Pages
{
    public class ResourceDonationModel : PageModel // Inherit from PageModel
{
    private readonly ILogger<ResourceDonationModel> _logger; // Add this line for logging
    private readonly ApplicationDbContext _context; // Add this line for database context

    public ResourceDonationModel(ILogger<ResourceDonationModel> logger, ApplicationDbContext context) // Constructor injection
    {
        _logger = logger; // Initialize the logger
        _context = context; // Initialize the database context
    }

    [BindProperty]
    public InputModel Input { get; set; } = new InputModel(); // Model for form input

    public List<ResourceDonation>? Donations { get; set; } // To hold existing donations

    public class InputModel // Model for form input
    {
        public string DonorName { get; set; } = string.Empty; // Donor's name
        public string DonorContact { get; set; } = string.Empty; // Donor's contact information
        public string DonationType { get; set; } = string.Empty; // Type of donation (e.g., food, clothing, medical supplies)
        public int Quantity { get; set; } // Quantity of the donation
        public string Description { get; set; } = string.Empty; // Description of the donation
        public string DonationMethod { get; set; } = string.Empty;  // Method of donation (e.g., pick-up, drop-off, delivery)
        public string Address { get; set; } = string.Empty; // Donor's address (if applicable)
    }

    public void OnGet() // Handle the GET request to display the form
    {
        LoadDonations(); // Load existing donations
    }

    public async Task<IActionResult> OnPostAsync() // Handle the form submission
    {
        if (!ModelState.IsValid) // Check if the model state is valid
        {
            TempData["ErrorMessage"] = "Please correct the errors in the form."; // Set error message
            LoadDonations(); // Reload the donations
            return Page(); // Return to the page    
        }

        var resourceDonation = new ResourceDonation // Create a new resource donation object
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

        try // Try to save the resource donation
        {
            _context.ResourceDonations.Add(resourceDonation); // Add the resource donation to the context
            await _context.SaveChangesAsync(); // Save the resource donation to the database

            TempData["SuccessMessage"] = "Resource donation submitted successfully."; // Set success message
            return RedirectToPage("/Success"); // Redirect to a success page
        }
        catch (Exception ex) // Catch any exceptions that occur during the save operation
        {
            _logger.LogError(ex, "Error occurred while saving the resource donation."); // Log the error
            TempData["ErrorMessage"] = "Failed to submit the resource donation. Please try again."; // Set error message
            LoadDonations(); // Reload the donations
            return Page(); // Return to the page
        }
    }

    private void LoadDonations() // Load the last 10 donations
    {
        Donations = _context.ResourceDonations // Get all resource donations
            .OrderByDescending(d => d.CreatedAt) // Order by the latest donations
            .Take(10) // Limit to the last 10 donations
            .ToList(); // Convert to a list
    }
}

}