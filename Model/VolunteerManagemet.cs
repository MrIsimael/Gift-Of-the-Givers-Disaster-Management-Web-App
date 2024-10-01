using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversFoundation.Models
{
    public class Volunteer
    {
        public int Id { get; set; } // Primary key for the Volunteer entity

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty; // Name of the volunteer

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty; // Email address of the volunteer

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty; // 10-digit phone number of the volunteer

        [Required(ErrorMessage = "Skills are required.")]
        public string Skills { get; set; } = string.Empty; // Store as a single string, you can

        [Required(ErrorMessage = "Availability is required.")]
        public string Availability { get; set; } = string.Empty; // Store as a single string, you can

        public string PreferredTasks { get; set; } = string.Empty; // Store as a single string

        public DateTime CreatedAt { get; set; } // Timestamp when the volunteer record was created
    }
}
