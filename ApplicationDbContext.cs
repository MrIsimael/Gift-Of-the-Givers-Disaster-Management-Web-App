using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGiversFoundation.Models;

namespace GiftOfTheGiversFoundation.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserReg> // Inherit from IdentityDbContext with UserReg as the user type
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) // Constructor
            : base(options) // Call the base constructor with the provided options
        {
        }
        public DbSet<IncidentReport> IncidentReports { get; set; } // Incident reports table

        public DbSet<ResourceDonation> ResourceDonations { get; set; } // Resource donations table

        public DbSet<Volunteer> Volunteers { get; set; } // Volunteers table


    }
}
