using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGiversFoundation.Models;

namespace GiftOfTheGiversFoundation.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserReg>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IncidentReport> IncidentReports { get; set; } // Incident reports table

        public DbSet<ResourceDonation> ResourceDonations { get; set; } // Resource donations table

        public DbSet<Volunteer> Volunteers { get; set; }


    }
}
