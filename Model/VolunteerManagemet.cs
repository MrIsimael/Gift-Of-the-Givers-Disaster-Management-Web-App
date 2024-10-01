using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversFoundation.Models
{
    public class Volunteer
    {
        public int Id { get; set; } // Primary key for the Volunteer entity

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Skills are required.")]
        public string Skills { get; set; } = string.Empty;

        [Required(ErrorMessage = "Availability is required.")]
        public string Availability { get; set; } = string.Empty;

        public string PreferredTasks { get; set; } = string.Empty; // Store as a single string
    }
}
