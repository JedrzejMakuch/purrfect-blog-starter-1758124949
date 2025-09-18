using Microsoft.AspNet.Identity.EntityFramework;

namespace Purrfect_Blog_Starter.Models
{
    public class ApplicationUser : IdentityUser
    {}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection") { }
        public static ApplicationDbContext Create() => new ApplicationDbContext();
    }
}