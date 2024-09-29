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

    }
}
