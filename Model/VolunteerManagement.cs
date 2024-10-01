using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversFoundation.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Skills are required.")]
        public string? Skills { get; set; }

        [Required(ErrorMessage = "Availability is required.")]
        public string? Availability { get; set; }

        public string? PreferredTasks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
