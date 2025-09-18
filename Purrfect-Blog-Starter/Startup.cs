using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Purrfect_Blog_Starter.Data;
using Purrfect_Blog_Starter.Repositories;
using Purrfect_Blog_Starter.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(Purrfect_Blog_Starter.Startup))]
namespace Purrfect_Blog_Starter
{
    public class Startup
    {
        public void Configuration(IAppBuilder application)
        {
            application.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            });

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IFactsRepository, FactsRepository>(Lifestyle.Scoped);
            container.Register<IFactsService, FactsService>(Lifestyle.Scoped);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}