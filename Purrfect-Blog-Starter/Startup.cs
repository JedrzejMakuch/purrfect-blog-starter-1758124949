using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

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
        }
    }
}