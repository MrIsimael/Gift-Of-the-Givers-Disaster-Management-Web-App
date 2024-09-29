using System.ComponentModel.DataAnnotations;
using GiftOfTheGiversFoundation.Models;
using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGiversFoundation.Models
{
    public class UserReg : IdentityUser
    {
        [Required]
        public string? FullName { get; set; } // Additional field if needed
    }
}
