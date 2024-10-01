using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversFoundation.Models
{
    public class ResourceDonation
    {
        public int Id { get; set; }

        [Required]
        public string? DonorName { get; set; }

        [Required]
        public string? DonorContact { get; set; }

        [Required]
        public string? DonationType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? DonationMethod { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}