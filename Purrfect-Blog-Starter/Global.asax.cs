using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Purrfect_Blog_Starter.Models;
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
            SeedUsers();
        }

        private void SeedUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (userManager.FindByName("Test1") == null)
                {
                    var user = new ApplicationUser { UserName = "Test1"};
                    userManager.Create(user, "Test1#123");
                }

                if (userManager.FindByName("Test2") == null)
                {
                    var user = new ApplicationUser { UserName = "Test2"};
                    userManager.Create(user, "Test2#123");
                }

                if (userManager.FindByName("Test3") == null)
                {
                    var user = new ApplicationUser { UserName = "Test3"};
                    userManager.Create(user, "Test3#123");
                }
            }
        }
    }
}
