using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Purrfect_Blog_Starter.Data;
using Purrfect_Blog_Starter.Dtos;
using Purrfect_Blog_Starter.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Purrfect_Blog_Starter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SeedUsersAndFacts();
        }

        private void SeedUsersAndFacts()
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.Create(new ApplicationUser { UserName = "Test1" }, "Test1#123");
                userManager.Create(new ApplicationUser { UserName = "Test2" }, "Test2#123");
                userManager.Create(new ApplicationUser { UserName = "Test3" }, "Test3#123");

                var u1 = userManager.FindByName("Test1");
                var u2 = userManager.FindByName("Test2");
                var u3 = userManager.FindByName("Test3");

                AddFactIfMissing(context, u1?.Id, "Cats have 3 eyelids.");
                AddFactIfMissing(context, u1?.Id, "Cats walk on their toes.");
                AddFactIfMissing(context, u2?.Id, "Most cats adore sardines.");
                AddFactIfMissing(context, u3?.Id, "Cats dislike citrus scent.");
                AddFactIfMissing(context, u3?.Id, "Approximately 40,000 people are bitten by cats in the U.S. annually.");

                context.SaveChanges();
            }
        }

        private static void AddFactIfMissing(ApplicationDbContext ctx, string userId, string text)
        {
            var exists = ctx.SavedFacts.Any(f => f.UserId == userId && f.Text == text);
            if (!exists)
            {
                ctx.SavedFacts.Add(new SavedFactDto
                {
                    UserId = userId,
                    Text = text,
                    CreatedAtUtc = DateTime.UtcNow
                });
            }
        }
    }
}
