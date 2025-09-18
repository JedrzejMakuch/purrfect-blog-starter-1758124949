using Microsoft.AspNet.Identity.EntityFramework;
using Purrfect_Blog_Starter.Dtos;
using Purrfect_Blog_Starter.Models;
using System.Data.Entity;

namespace Purrfect_Blog_Starter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection") { }
        public static ApplicationDbContext Create() => new ApplicationDbContext();

        public DbSet<SavedFactDto> SavedFacts { get; set; }
    }
}