using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversFoundation.Models
{
    public class IncidentReport
    {
        [Key]
        public int IncidentID { get; set; } // Primary key

        [Required]
        [StringLength(255)]
        public string? IncidentTitle { get; set; } // Title of the incident

        [Required]
        public DateTime IncidentDateTime { get; set; } // Date and time of the incident

        [Required]
        [StringLength(255)]
        public string? Location { get; set; } // Location of the incident

        [Required]
        [StringLength(50)]
        public string? DisasterType { get; set; } // Type of disaster (Flood, Earthquake, etc.)

        [Required]
        public string? Description { get; set; } // Detailed description of the incident

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Automatically set the creation date
    }
}
