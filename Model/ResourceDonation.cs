using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversFoundation.Models
{
    public class ResourceDonation
    {
        public int Id { get; set; } // Primary key

        [Required]
        public string? DonorName { get; set; } // Name of the donor

        [Required]
        public string? DonorContact { get; set; } // Contact information of the donor

        [Required]
        public string? DonationType { get; set; } // Type of resource donated (e.g., food, clothing, medical supplies)

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; } // Quantity of the resource donated

        [Required]
        public string? Description { get; set; } // Description of the resource donated

        [Required]
        public string? DonationMethod { get; set; } // Method of donation (e.g., pick-up, drop-off, delivery)

        public string? Address { get; set; } // Address for pick-up or delivery (optional)

        public DateTime CreatedAt { get; set; } // Timestamp when the donation was created
    }
}