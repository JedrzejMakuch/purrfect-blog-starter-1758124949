using Microsoft.AspNet.Identity.EntityFramework;
using Purrfect_Blog_Starter.Models;

namespace Purrfect_Blog_Starter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection") { }
        public static ApplicationDbContext Create() => new ApplicationDbContext();
    }
}